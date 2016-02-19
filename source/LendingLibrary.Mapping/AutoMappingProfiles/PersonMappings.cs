﻿using AutoMapper;
using LendingLibrary.Domain.Models;
using LendingLibrary.Web.Models;

namespace LendingLibrary.Mapping.AutoMappingProfiles
{
    public class DELETE_ME_PersonMappings:Profile
    {
        protected override void Configure()
        {
            CreateMap<PersonViewModel, Person>();
            CreateMap<Person, PersonViewModel>();
        }
    }
}