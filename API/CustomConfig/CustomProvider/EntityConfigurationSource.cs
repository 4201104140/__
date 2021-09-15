using Microsoft.Extensions.Configuration;

namespace CustomConfig.CustomProvider
{
    public class EntityConfigurationSource : IConfigurationSource
    {
        private readonly string _connectionString;
        private readonly bool _reloadOnChange;

        public EntityConfigurationSource(string connectionString, bool reloadOnChange)
        {
            _connectionString = connectionString;
            _reloadOnChange = reloadOnChange;
        }
            

        public IConfigurationProvider Build(IConfigurationBuilder builder) =>
            new EntityConfigurationProvider(_connectionString, _reloadOnChange);
    }
}
