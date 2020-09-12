using AutoMapper;
using AutoMapperSample.Entities;
using AutoMapperSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperSample.CustomAutoMapper
{
    class CustomerResolver : IValueResolver<Customer, CustomerViewModel, string>
    {
        public string Resolve(Customer source, CustomerViewModel destination, string destMember, ResolutionContext context)
        {
            return source.FirstName + " " + source.LastName;
        }
    }
}
