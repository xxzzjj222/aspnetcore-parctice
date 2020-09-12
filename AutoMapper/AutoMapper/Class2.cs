using AutoMapperSample.Entities;
using AutoMapperSample.Entities.Context;
using AutoMapperSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper
{
    class Class2
    {
        public static void Run()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentViewModel>());
            var mapper = config.CreateMapper();

            var result = GetStudents();
            var students = mapper.Map<List<StudentViewModel>>(result);

            Print(students);
        }

        public static List<Student> GetStudents()
        {
            var context = new SampleContext();
            return context.Students;
        }

        public static void Print(IList<StudentViewModel> studentViewModels)
        {
            foreach (var student in studentViewModels)
            {
                Console.WriteLine($"Id={student.Id};Name={student.Name};Age={student.Age};Gender={student.Gender}");
            }
        }
    }
}
