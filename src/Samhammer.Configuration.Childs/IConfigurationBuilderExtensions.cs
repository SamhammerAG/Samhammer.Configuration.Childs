using Microsoft.Extensions.Configuration;

namespace Samhammer.Configuration.Childs
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder EnableChildSubstitutions(this IConfigurationBuilder builder, string childSectionName)
        {
            return builder.Add(new ChainedSubstitutedConfigurationSource(builder.Build(), childSectionName));
        }
    }
}
