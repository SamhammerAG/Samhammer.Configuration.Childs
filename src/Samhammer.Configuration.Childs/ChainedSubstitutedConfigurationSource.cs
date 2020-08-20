﻿using Microsoft.Extensions.Configuration;

namespace Samhammer.Configuration.Childs
{
    /// <summary>
    /// Represents a chained <see cref="IConfiguration"/> as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class ChainedSubstitutedConfigurationSource : IConfigurationSource
    {
        private readonly IConfiguration _configuration;
        private readonly string _substituteSection;

        public ChainedSubstitutedConfigurationSource(IConfiguration configuration, string substituteSection)
        {
            _configuration = configuration;
            _substituteSection = substituteSection;
        }

        /// <summary>
        /// Builds the <see cref="ChainedSubstitutedConfigurationSource"/> using IConfigurationBuilder as the source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="ChainedSubstitutedConfigurationProvider"/></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new ChainedSubstitutedConfigurationProvider(_configuration, _substituteSection);
    }
}
