namespace Model.DataBase.Model
{
    using global::Model.Data.PatternMVVM.TrainingProgramm;
    using global::Model.DataBase.Context;
    using global::Model.Message;
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

        public void AddProgramm(Programm3Model programm3Model)
        {
            if (programm3Model == null)
            {
                MessageBug.AddMessage("Модель не заполнена");
                return;
            }
            if (programm3Model.Name == null)
            {
                MessageBug.AddMessage("Название программы не заполнено");
                return;
            }
            if (programm3Model.DateNumberApproved == null)
            {
                MessageBug.AddMessage("Кем и когда одобрена не заполнены");
                return;
            }
            
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                ProgramDGs programs = new ProgramDGs
                {
                    name = programm3Model.Name,
                    dateNumberApproved = programm3Model.DateNumberApproved,
                    typeId = 3
                };
                dataBaseContext.ProgramDGs.Add(programs);
                dataBaseContext.SaveChanges();
            }
        }
    }
}
