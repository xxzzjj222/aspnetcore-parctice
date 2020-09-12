using System.Collections.Generic;

namespace AutoMapperSample.Entities.Context
{
    public class SampleContext
    {
        public List<Student> Students { get; set; }

        public List<User> Users { get; set; }

        public SampleContext()
        {
            Students = new List<Student>();
            for (var i = 0; i < 5; i++)
            {
                var student = new Student
                {
                    Id = i,
                    Name = $"空军{i}号",
                    Age = 18 + i,
                    Gender = "男"
                };
                Students.Add(student);
            }

            Users = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                var user = new User
                {
                    Id = i,
                    Name = $"用户{i}",
                    Address = new Address
                    {
                        City = "廷根市",
                        Country = "鲁恩",
                        State = "0-08"
                    }
                };
                Users.Add(user);
            }
            
        }
    }
}
