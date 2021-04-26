using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment8.BL
{
    public class StudentRepository
    {
        List<Student> studentList = new List<Student>() 
        { 
            new Student
            { 
                Id=1,
                Name="Heta",
                City="Surat",
                State="Gujarat",
                Gender="Female",
                Age=21
            },
            new Student
            {
                Id=2,
                Name="Lav",
                City="Pune",
                State="Maharastra",
                Gender="Male",
                Age=24
            },
            new Student
            {
                Id=3,
                Name="Payal",
                City="Surat",
                State="Gujarat",
                Gender="Female",
                Age=21
            },
            new Student
            {
                Id=4,
                Name="Krish",
                City="Ahmedabad",
                State="Gujarat",
                Gender="Male",
                Age=18
            }
        };

        public List<Student> GetStudents()
        {
            return studentList;
        }

        
        
    }
}
