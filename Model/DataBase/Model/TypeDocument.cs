namespace Model.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeDocument")]
    public partial class TypeDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeDocument()
        {
            ProgramDGs = new HashSet<ProgramDGs>();
            Programs = new HashSet<Programs>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgramDGs> ProgramDGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programs> Programs { get; set; }

        public int ConvertNameToId(string type)
        {
            int idType = -1;
            if (type.Equals("�������������"))
            {
                idType = 0;
            }
            if (type.Equals("������������� (��������)"))
            {
                idType = 1;
            }
            if (type.Equals("������������� (��������)"))
            {
                idType = 2;
            }
            if (type.Equals("���������� ��"))
            {
                idType = 3;
            }
            if (type.Equals("���������"))
            {
                idType = 4;
            }
            return idType;
        }
    }
}
