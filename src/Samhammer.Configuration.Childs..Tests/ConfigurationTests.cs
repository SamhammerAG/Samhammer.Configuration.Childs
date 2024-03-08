using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Samhammer.Configuration.Childs.Test
{
    public class ConfigurationTests
    {
        [Fact]
        public void Should_get_value_original()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"ConnectionString", "con1"}
                })
                .EnableChildSubstitutions("{substitute}");

            var configuration = configurationBuilder.Build();

            // Act
            var substituted = configuration["ConnectionString"];

            substituted.Should().Be("con1");
        }

        [Fact]
        public void Should_get_value_from_root_substitute_section()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"ConnectionString", "con1"},
                    {"{substitute}:ConnectionString", "con2"}
                })
                .EnableChildSubstitutions("{substitute}");

            var configuration = configurationBuilder.Build();

            // Act
            var substituted = configuration["ConnectionString"];

            substituted.Should().Be("con2");
        }

        [Fact]
        public void Should_get_section_value_from_root_substitute_section()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"connection:ConnectionString", "con1"},
                    {"{substitute}:connection:ConnectionString", "con2"}
                })
                .EnableChildSubstitutions("{substitute}");

            var configuration = configurationBuilder.Build();

            // Act
            var substituted = configuration["connection:ConnectionString"];
            substituted.Should().Be("con2");
        }

        [Fact]
        public void Should_get_section_value_from_parent_substitute_section()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"connection:ConnectionString", "con1"},
                    {"connection:{substitute}:ConnectionString", "con2"}
                })
                .EnableChildSubstitutions("{substitute}");

            var configuration = configurationBuilder.Build();

            // Act
            var substituted = configuration["connection:ConnectionString"];
            substituted.Should().Be("con2");
        }

        [Fact]
        public void Should_get_section_childs_including_substitute_child()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    {"apis:abc", "abc.de"},
                    {"apis:def", "def.de"},
                    {"{substitute}:apis:xyz", "xyz.de"}
                })
                .EnableChildSubstitutions("{substitute}");

            var configuration = configurationBuilder.Build();

            // Act
            var children = configuration.GetSection("apis").GetChildren().Select(c => c.Key).ToList();
            children.Should().HaveCount(3);
            children.Should().ContainInOrder("abc", "def", "xyz");
        }
    }
}
