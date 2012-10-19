using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ParameterDependencyDetector.Core
{
    public class Repository : IRepository
    {
        private SqlConnection Connection { get; set; }


        public Repository(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }
        
        public async Task<List<string>> ListSystemStoredProcedures()
        {
            var sql = new EmbeddedResources().GetResource("Resources.ListStoredProcedures.sql");
            var foundStoredProcedures = new List<string>();

            using (var command = new SqlCommand(sql.ToString(), Connection))
            {
                using(var reader = command.ExecuteReader())
                {
                    while(await reader.ReadAsync())
                    {
                        foundStoredProcedures.Add(reader.GetString(0));
                    }
                }
            }

            return foundStoredProcedures;
        }

        public async Task<List<string>> ListUsages(string toFind)
        {
            var sql = new EmbeddedResources().GetResource("Resources.SearchDatabaseScript.sql");
            var foundUsages = new List<string>();

            using (var command = new SqlCommand(sql.ToString(), Connection))
            {
                command.Parameters.Add(new SqlParameter("@ToFind", SqlDbType.NVarChar) {Value = "%" + toFind + "%"});

                using (var reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        foundUsages.Add(reader.GetString(0));
                    }
                }
            }

            return foundUsages;
        }

        public async Task<List<string>> ListParametersForStoredProcedure(string procedureName)
        {
            var sql = new EmbeddedResources().GetResource("Resources.DetectParameters.sql");
            var detectedParameters = new List<string>();

            using (var command = new SqlCommand(sql.ToString(), Connection))
            {
                command.Parameters.Add(new SqlParameter("@ProcedureName", SqlDbType.NVarChar) { Value = procedureName });

                using (var reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        detectedParameters.Add(reader.GetString(0));
                    }
                }
            }

            return detectedParameters;
        }

        public async Task<string> GetProcedure(string procedureName)
        {
            var sql = new EmbeddedResources().GetResource("Resources.GetProcedure.sql");
            var result = string.Empty;

            using (var command = new SqlCommand(sql.ToString(), Connection))
            {
                command.Parameters.Add(new SqlParameter("@ProcedureName", SqlDbType.NVarChar) { Value = procedureName });

                using (var reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        result = reader.GetString(0);
                        break;
                    }
                }
            }

            return result;
        }
    }
}