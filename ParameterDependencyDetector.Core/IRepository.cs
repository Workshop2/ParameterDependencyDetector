using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParameterDependencyDetector.Core
{
    public interface IRepository
    {
        Task<List<string>> ListSystemStoredProcedures();
        Task<List<string>> ListUsages(string toFind);
        Task<List<string>> ListParametersForStoredProcedure(string procedureName);
        Task<string> GetProcedure(string procedureName);
    }
}