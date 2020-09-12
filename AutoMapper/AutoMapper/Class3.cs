using AutoMapper;
using AutoMapperSample.CustomAutoMapper;
using AutoMapperSample.Entities;
using AutoMapperSample.Entities.Context;
using AutoMapperSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperSample
{
    class Class3
    {

        public static void Run()
        {
            var config = new MapperConfiguration(cfg =>
              {
                  //cfg.CreateMap<User, UserViewModel>()
                  //    .ForMember(
                  //    model => model.City, 
                  //    expression => expression.MapFrom(user => user.Address.City))
                  //    .ForMember(
                  //    model => model.Country, 
                  //    expression => expression.MapFrom(user => user.Address.Country))
                  //    .ForMember(
                  //    model => model.State, expression => 
                  //    expression.MapFrom(user => user.Address.State));

                  cfg.AddProfile<UserProfile>();
              });

            var mapper = config.CreateMapper();
            var users = GetUsers();
            Print(mapper.Map<List<UserViewModel>>(users));
        }
        public static List<User> GetUsers()
        {
            var context = new SampleContext();
            return context.Users;
        }

        public static void Print(IList<UserViewModel> userViewModels)
        {
            foreach (var user in userViewModels)
            {
                Console.WriteLine($"Id={user.Id};Name={user.Name};City={user.City}");
            }
        }
    }
}
