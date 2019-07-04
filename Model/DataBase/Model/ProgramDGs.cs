namespace Model.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProgramDGs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProgramDGs()
        {
            CertificateDGs = new HashSet<CertificateDGs>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(1200)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string dateNumberApproved { get; set; }

        public int typeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertificateDGs> CertificateDGs { get; set; }

        public virtual TypeDocument TypeDocument { get; set; }
    }
}
