using AutoMapperSample.Entities;
using AutoMapperSample.Entities.Context;
using AutoMapperSample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper
{
    class Class1
    {
        public static void Run()
        {
            var result = GetStudents();

            var students = new List<StudentViewModel>();

            foreach (var item in result)
            {
                var student = new StudentViewModel
                {
                    Id = item.Id,
                    Age = item.Age,
                    Name = item.Name,
                    Gender = item.Gender
                };
                students.Add(student);
            }
            Print(students);
        }

        public static  List<Student> GetStudents()
        {
            var context = new SampleContext();
            return context.Students;
        }

        public static void Print(IList<StudentViewModel> studentViewModels)
        {
            foreach (var student in studentViewModels)
            {
                Console.WriteLine($"Id={student.Id};Name={student.Name};");
            }
        }

    }
}
