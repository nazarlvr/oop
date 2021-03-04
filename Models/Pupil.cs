using System;
using System.Collections.Generic;

#nullable disable

namespace ElemSchool
{
    public partial class Pupil
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public int MedicalCardId { get; set; }

        public virtual Class Class { get; set; }
        public virtual MedicalCard MedicalCard { get; set; }
    }
}
