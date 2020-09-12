using AutoMapper;
using AutoMapperSample.CustomAutoMapper;
using AutoMapperSample.Entities;
using AutoMapperSample.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AutoMapperSample
{
    class Class5
    {
        public static void Run()
        {
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<int, string>().ConvertUsing<StringTypeConverter>();
                  cfg.CreateMap<Person, PersonViewModel>();
              });

            var mapper = config.CreateMapper();

            var person = new Person()
            {
                Age = 12,
                Id = 1,
                Name = "asd"
            };

            var result = mapper.Map<PersonViewModel>(person);
            Console.WriteLine($"{result.Age.GetType()}:Age={result.Age})");
        }
    }
}
