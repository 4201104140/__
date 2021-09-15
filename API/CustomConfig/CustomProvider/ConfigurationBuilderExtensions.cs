using Microsoft.Extensions.Configuration;

namespace CustomConfig.CustomProvider
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddEntityConfiguration(
            this IConfigurationBuilder builder, bool reloadOnChange)
        {
            var tempConfig = builder.Build();
            var connectionString =
                tempConfig.GetConnectionString("WidgetConnectionString");

            return builder.Add(new EntityConfigurationSource(connectionString, reloadOnChange));
        }
    }
}
