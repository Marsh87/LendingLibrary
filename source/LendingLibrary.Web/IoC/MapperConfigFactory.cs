using System;
using System.Linq;
using AutoMapper;
using LendingLibrary.Web.AutoMappingProfiles;
using PeanutButter.Utils;

namespace LendingLibrary.Web.IoC
{
    public interface IMapperConfigFactory
    {
        MapperConfiguration GetConfiguration();
    }

    public class MapperConfigFactory : IMapperConfigFactory
    {
        public MapperConfiguration GetConfiguration()
        {
            var profileBaseType = typeof (Profile);
            var profileTypes = typeof (PersonMappings).Assembly
                .GetTypes()
                .Where(t => profileBaseType.IsAssignableFrom(t));
            var profiles = profileTypes.Select(Activator.CreateInstance)
                .Select(o => o as Profile);

            return new MapperConfiguration(cfg =>
            {
                profiles.ForEach(cfg.AddProfile);
            });
        }        
    }
}