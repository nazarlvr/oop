using System;
using System.Collections.Generic;

#nullable disable

namespace ElemSchool
{
    public partial class Teacher
    {
        public string Id { get; set; }
        public int CategoryId { get; set; }
        public int ClassId { get; set; }
        public decimal Salary { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }

        public virtual Category Category { get; set; }
        public virtual Class Class { get; set; }
    }
}
