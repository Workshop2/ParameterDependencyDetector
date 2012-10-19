using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParameterDependencyDetector.Core
{
    public class StoredProcedureParser
    {
        private IRepository Repository { get; set; }
        private string ProcedureToParse { get; set; }
        private ParameterDetector ParameterDetector { get; set; }

        public StoredProcedureParser(IRepository repository, string procedureToParse, ParameterDetector parameterDetector)
        {
            this.Repository = repository;
            this.ProcedureToParse = procedureToParse;
            this.ParameterDetector = parameterDetector;
        }

        public async Task<List<Dictionary<string, string>>> DetectUsages(string procedureName)
        {
            var result = new List<Dictionary<string, string>>();
            var procedureToParse = (await Repository.GetProcedure(ProcedureToParse)).Trim();
            procedureToParse = RemoveAllComments(procedureToParse);
            var procedureAsLines = procedureToParse.Split(Environment.NewLine.ToCharArray()).Select(x => x.Trim()).Where(x => string.IsNullOrEmpty(x) == false).ToArray();

            if (string.IsNullOrEmpty(procedureToParse))
                return result;

            var instances = GetAllInstancesOfProcedure(procedureAsLines, procedureName);

            result.AddRange(instances.Select(instance => GetParametersForInstance(instance, procedureAsLines, procedureName)));

            return result;
        }

        private static string RemoveAllComments(string procedureToParse)
        {
            const string blockComments = @"/\*(.*?)\*/";
            const string lineComments = @"--(.*?)\r?\n";
            const string strings = @"""((\\[^\n]|[^""\n])*)""";
            const string verbatimStrings = @"@(""[^""]*"")+";
            const string prints = @"print (.*?)\n";

            var noComments = Regex.Replace(procedureToParse, blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings + "|" + prints,
                me =>
                {
                    var m = me.Value.Trim();
                    if (m.StartsWith("/*") || m.StartsWith("--") || m.StartsWith("PRINT", StringComparison.InvariantCultureIgnoreCase))
                        return m.StartsWith("--") ? Environment.NewLine : string.Empty;
                    // Keep the literal strings
                    return m;
                },
                RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            return noComments;
        }

        private Dictionary<string, string> GetParametersForInstance(int instance, IList<string> procedureToParse, string procedureName)
        {
            var hasFoundEnd = false;

            var result = ParameterDetector.GetBlankObject();
            result[ParameterDetector.UsageConst] = this.ProcedureToParse;

            var currentLine = procedureToParse[instance];
            var whereToStart = currentLine.IndexOf(procedureName, StringComparison.InvariantCultureIgnoreCase) + procedureName.Length;

            if (whereToStart < currentLine.Length)
                currentLine = currentLine.Substring(whereToStart);
            else
            {
                instance++;
                currentLine = procedureToParse[instance];
            }

            while (!hasFoundEnd)
            {
                var matches = currentLine.Split(',').Select(x => x.Trim()).ToList();

                foreach (var match in matches)
                {
                    var parameterParts = match.Split('=').Select(x => x.Trim()).ToArray();

                    //TODO: If we want to increase precision, then we need to support the format of: usp_SP @Param1, @Param2 @Param3

                    if (parameterParts.Count() < 2 || string.IsNullOrEmpty(parameterParts[0]))
                        continue;

                    var isLast = matches.IndexOf(match) == (matches.Count - 1);
                    if (isLast)
                        hasFoundEnd = true;

                    if (!result.ContainsKey(parameterParts[0]))
                    {
                        hasFoundEnd = true; // throw new Exception("Found un-expected paramter: " + parameterParts[0]);
                        continue;
                    }

                    result[parameterParts[0]] = parameterParts[1];
                }

                if (!hasFoundEnd)
                {
                    instance++;
                    if (instance < (procedureToParse.Count() - 1))
                        currentLine = procedureToParse[instance];
                    else
                        hasFoundEnd = true;
                }
            }

            return result;
        }

        private static IEnumerable<int> GetAllInstancesOfProcedure(IList<string> procedureToParse, string procedureName)
        {
            var result = new List<int>();

            for (var i = 0; i < procedureToParse.Count(); i++)
            {
                var line = procedureToParse[i];
                var indexOf = line.IndexOf(procedureName, StringComparison.InvariantCultureIgnoreCase);
                if (indexOf <= -1)
                    continue;

                //No we need to make sure the string we have found doesn't contain anything after, e.g. ProcedureName_Test
                var remainingCharactersCount = line.Length - (indexOf + procedureName.Length);
                if (remainingCharactersCount > 0)
                {
                    var trailingCharacter = line.Substring(indexOf + procedureName.Length, 1);
                    trailingCharacter = trailingCharacter.Trim();

                    if (string.IsNullOrEmpty(trailingCharacter) || trailingCharacter == "]")
                        result.Add(i);
                }
                else
                    result.Add(i);
            }

            return result;
        }
    }
}