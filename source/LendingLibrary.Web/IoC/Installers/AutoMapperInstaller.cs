using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace LendingLibrary.Web.IoC.Installers
{
/*
    public class AutoMapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                     .BasedOn<Profile>()
                     .WithServiceBase());
            container.Register(Classes.FromAssembly(typeof(LendingLibrary .Mapping.AutoMappingProfiles.PersonMappings).Assembly)
                                .BasedOn<Profile>()
                                .WithServiceBase());

            container.Register(Component.For<IMappingEngine>().UsingFactoryMethod(k =>
            {
                Profile[] profiles = k.ResolveAll<Profile>();

                Mapper.Initialize(cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });

                CollectionExtensions.ForEach(profiles, k.ReleaseComponent);


                return Mapper.Engine;
            }));
        }
    }
*/
}