using System;
using System.Collections.Generic;

#nullable disable

namespace ElemSchool
{
    public partial class Category
    {
        public Category()
        {
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Info { get; set; }
        public string Subjects { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
