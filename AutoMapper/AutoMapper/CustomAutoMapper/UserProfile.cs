using AutoMapper;
using AutoMapperSample.Entities;
using AutoMapperSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperSample.CustomAutoMapper
{
    class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
               .ForMember(
                model => model.City,
                expression => expression.MapFrom(user => user.Address.City))
               .ForMember(
                model => model.Country,
                expression => expression.MapFrom(user => user.Address.Country))
                .ForMember(
                model => model.State, expression =>
                expression.MapFrom(user => user.Address.State));
        }
    }
}
