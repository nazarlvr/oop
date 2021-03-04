using System;
using System.Collections.Generic;

#nullable disable

namespace ElemSchool
{
    public partial class MedicalCard
    {
        public MedicalCard()
        {
            Pupils = new HashSet<Pupil>();
        }

        public int Id { get; set; }
        public int Heigth { get; set; }
        public int Weigth { get; set; }
        public int Term { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
    }
}
