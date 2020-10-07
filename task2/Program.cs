using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace task2
{
    class Student 
    {
        public string name;
        public string state;

        public Student(string name1)
        {
            name = name1;
            state = "";
        }
        public void Read()
        {
            state += "Read "; 
        }
        public void Write()
        {
            state += "Write ";
        }

        public void Relax()
        {
            state += "Relax ";
        }

        public virtual void Study()
        {
        }

    }
    
    class BadStudent : Student
    {
        public BadStudent(string name) : base(name) 
        {
            state += "Bad ";
        }

        public override void Study()
        {
            Relax();
            Relax();
            Relax();
            Relax();
            Read();
        }
    }

    class GoodStudent : Student
    {
        public GoodStudent(string name) : base(name)
        {
            state += "Good ";
        }

        public override void Study()
        {
            Read();
            Write();
            Read();
            Write();
            Relax();
        }
    }
    
    class Group
    {
        public string number;
        List<Student> students;

        public Group(string name1)
        {
            number = name1;
            students = new List<Student>() { };
        }

        public void AddStudent(Student person)
        {
            students.Add(person);
        }

        public void GetInfo()
        {
            Console.WriteLine("Group - " + number);
            students.ForEach(delegate (Student person)
            {
                Console.WriteLine(person.name);
            });
        }

        public void GetFullInfo()
        {
            Console.WriteLine("Group - " + number);
            students.ForEach(delegate (Student person)
            {
                Console.WriteLine(number + "     " + person.name + "     " + person.state);
            });
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Group testgroup = new Group("K-00");
            Student teststudent1 = new BadStudent("TestStudent1");
            Student teststudent2 = new GoodStudent("TestStudent2");
            Student teststudent3 = new BadStudent("TestStudent3");
            testgroup.AddStudent(teststudent1);
            testgroup.AddStudent(teststudent2);
            testgroup.AddStudent(teststudent3);
            teststudent1.Study();
            teststudent2.Study();
            teststudent3.Study();
            testgroup.GetInfo();
            testgroup.GetFullInfo();
        }
    }
}
