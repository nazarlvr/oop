using System;
using System.Collections.Generic;

#nullable disable

namespace ElemSchool
{
    public partial class ElementarySchool
    {
        public ElementarySchool()
        {
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string SchoolTypeId { get; set; }
        public string Adress { get; set; }
        public int SchoolNumber { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
