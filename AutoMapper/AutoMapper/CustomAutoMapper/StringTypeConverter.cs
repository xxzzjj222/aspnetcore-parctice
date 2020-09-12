using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperSample.CustomAutoMapper
{
    class StringTypeConverter : ITypeConverter<int, string>
    {
        public string Convert(int source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
}
