using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;

namespace ApiCore.DB
{
    public class LinqToDbSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "PostgreSQL";
        public string DefaultDataProvider => "PostgreSQL";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "PostgreSQL",
                        ProviderName = "PostgreSQL",
                        ConnectionString = @"Server=postgress;Host=localhost;Port=5432;Database=peer;User Id=postgres;Password=123"
                    };
            }
        }
    }
}
