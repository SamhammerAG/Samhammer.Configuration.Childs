using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Samhammer.Configuration.Childs
{
    /// This class is inspired from Microsoft.Extensions.Configuration.ChainedConfigurationProvider
    public class ChainedSubstitutedConfigurationProvider: ConfigurationProvider
    {
        private readonly IConfiguration _config;
        private readonly string _substituteSection;

        /// <summary>
        /// Initialize a new instance from the configuration.
        /// </summary>
        public ChainedSubstitutedConfigurationProvider(IConfiguration config, string substituteSection)
        {
            _config = config;
            _substituteSection = substituteSection;
        }

        /// <summary>
        /// Loads configuration values from the source represented by this <see cref="IConfigurationProvider"/>.
        /// </summary>
        public override void Load()
        {
            ApplyOverwrites(_config.GetChildren());
        }

        /// <summary>
        /// Applies configuration values from substitute section, to parent configuration section / configuration root.
        /// </summary>
        /// <param name="sections"></param>
        private void ApplyOverwrites(IEnumerable<IConfigurationSection> sections)
        {
            foreach (var item in sections)
            {
                if (item.Path.Contains($"{_substituteSection}:"))
                {
                    var overwritePath = item.Path.Replace($"{_substituteSection}:", string.Empty);
                    Set(overwritePath, item.Value);
                }

                ApplyOverwrites(item.GetChildren());
            }
        }
    }
}
