using AutoMapper;
using AutoMapperSample.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperSample
{
    class Class6
    {
        public static void Run()
        {
            var config = new MapperConfiguration(cfg =>
              {

              });

            var mapper = config.CreateMapper();

            var customer = new Customer
            {
                Id = 1,
                FirstName = "Klein",
                LastName = "Moretti"
            };

            var result = mapper.Map<dynamic>(customer);
            Console.WriteLine(result.FirstName);

            customer = mapper.Map<Customer>(result);
            Console.WriteLine(customer.LastName);

            var dic = new Dictionary<string, object>() { { "FirstName", "aaa" }, { "LastName", "bbb" } };
            customer = mapper.Map<Customer>(dic);
            Console.WriteLine(customer.FirstName);
        }
    }
}
