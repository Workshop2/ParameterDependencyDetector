using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParameterDependencyDetector.Core
{
    public class ParameterDetector
    {
        private IRepository repository { get; set; }
        public List<string> Parameters { get; set; } 

        public ParameterDetector(IRepository repository)
        {
            this.repository = repository;
            this.Parameters = new List<string> { UsageConst };
        }

        public async void DetectParametersForProcedure(string procedureName)
        {
            Parameters = await repository.ListParametersForStoredProcedure(procedureName);
            Parameters.Insert(0, UsageConst);
        }

        public Dictionary<string,string> GetBlankObject()
        {
            return Parameters.ToDictionary(parameter => parameter, parameter => string.Empty);
        }

        public static string UsageConst = "Usage";
    }
}