using System;
using System.Collections.Generic;

#nullable disable

namespace ElemSchool
{
    public partial class Specialization
    {
        public Specialization()
        {
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
