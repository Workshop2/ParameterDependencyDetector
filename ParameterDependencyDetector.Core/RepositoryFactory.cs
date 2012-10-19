namespace ParameterDependencyDetector.Core
{
    public static class RepositoryFactory
    {
         public static IRepository GetRepository(string connectionString)
         {
             return new Repository(connectionString);
         }
    }
}