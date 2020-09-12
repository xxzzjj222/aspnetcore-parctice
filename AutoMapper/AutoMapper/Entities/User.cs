using System;
using System.Collections.Generic;
using System.Text;
using AutoMapperSample.Entities;

namespace AutoMapperSample.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
