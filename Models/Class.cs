using System;
using System.Collections.Generic;

#nullable disable

namespace ElemSchool
{
    public partial class Class
    {
        public Class()
        {
            Pupils = new HashSet<Pupil>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public int SpecializationId { get; set; }

        public virtual ElementarySchool School { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Pupil> Pupils { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
