using AutoMapper;
using AutoMapperSample.CustomAutoMapper;
using AutoMapperSample.Entities;
using AutoMapperSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperSample
{
    class Class4
    {
        public static void Run()
        {
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<Customer, CustomerViewModel>()
                      .ForMember(model => model.FullName, expression => expression.MapFrom<CustomerResolver>());
              });

            var mapper = config.CreateMapper();

            var customer = new Customer
            {
                Id = 1,
                FirstName = "Klein",
                LastName = "Moretti"
            };

            var result = mapper.Map<CustomerViewModel>(customer);

            Console.WriteLine(result.FullName);

        }
    }
}
