using ParameterDependencyDetector.Core;

namespace ParameterDependencyDetector.UI.Database
{
    public class DatabaseSingleton
    {
        private static readonly IRepository instance = RepositoryFactory.GetRepository(GetConnectionString());

        private DatabaseSingleton() { }

        public static IRepository Instance
        {
            get
            {
                return instance;
            }
        }

        private static string GetConnectionString()
        {
            return Properties.Settings.Default.ConnectionString;
        }
    }
}