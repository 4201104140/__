using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomConfig.CustomProvider
{
    public class EntityConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connectionString;
        private readonly bool _reloadOnChange;

        public EntityConfigurationProvider(string connectionString, bool reloadOnChange)
        {
            _connectionString = connectionString;
            _reloadOnChange = reloadOnChange;
            if (_reloadOnChange)
            {
                //ChangeToken.OnChange(
                //    () => GetReloadToken(),
                //    () =>
                //    {
                //        Thread.Sleep(250);
                //        this.Load();
                //    });
            }
        } 

        public override void Load()
        {
            using var dbContext = new EntityConfigurationContext(_connectionString);

            dbContext.Database.EnsureCreated();

            Data = dbContext.Settings.Any()
                ? dbContext.Settings.ToDictionary(c => c.Id, c => c.Value)
                : CreateAndSaveDefaultValues(dbContext);
            //OnReload();
        }

        static IDictionary<string, string> CreateAndSaveDefaultValues(
            EntityConfigurationContext context)
        {
            var settings = new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase)
            {
                ["WidgetOptions:EndpointId"] = "b3da3c4c-9c4e-4411-bc4d-609e2dcc5c67",
                ["WidgetOptions:DisplayLabel"] = "Widgets Incorporated, LLC.",
                ["WidgetOptions:WidgetRoute"] = "api/widgets"
            };

            context.Settings.AddRange(
                settings.Select(kvp => new Settings(kvp.Key, kvp.Value))
                        .ToArray());

            context.SaveChanges();

            return settings;
        }
    }
}
