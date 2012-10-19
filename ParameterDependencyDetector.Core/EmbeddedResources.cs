using System.IO;
using System.Reflection;
using System.Text;

namespace ParameterDependencyDetector.Core
{
    public class EmbeddedResources
    {
        public StringBuilder GetResource(string resourceName)
        {
            var result = new StringBuilder();

            try
            {
                using (var resourceStream = GetResourceStream(resourceName))
                {
                    if (resourceStream != null)
                    {
                        using (var textStreamReader = new StreamReader(resourceStream))
                        {
                            result.Append(textStreamReader.ReadToEnd());
                        }
                    }
                }
            }
            catch
            {
                result.Clear();
            }

            return result;
        }

        public static Stream GetResourceStream(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream("ParameterDependencyDetector.Core." + path);
        }
    }
}