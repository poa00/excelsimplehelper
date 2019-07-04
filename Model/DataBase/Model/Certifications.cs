namespace Model.DataBase.Model
{
    using global::Model.Data;
    using global::Model.Write.Word.Document;
    using System.ComponentModel.DataAnnotations;

    public partial class Certifications
    {
        public int id { get; set; }

        [Required]
        [StringLength(18)]
        public string startEducation { get; set; }

        [Required]
        [StringLength(18)]
        public string endEducation { get; set; }

        [Required]
        [StringLength(18)]
        public string issueDate { get; set; }

        [Required]
        [StringLength(10)]
        public string party { get; set; }

        [Required]
        [StringLength(7)]
        public string mark { get; set; }

        public int idStudent { get; set; }

        public int idProgramm { get; set; }

        public virtual Programs Programs { get; set; }

        public virtual Students Students { get; set; }

        public void LoadCertification(Certifications certification)
        {
            ManagerDocument managerDocument = new ManagerDocument();
            managerDocument.DocumentLoad(certification);
        }

        public void SaveCertification(Record DataForDocuments)
        {
            ManagerDocument managerDocument = new ManagerDocument();
            managerDocument.DocumentSaveCertificate(DataForDocuments);
        }
    }
}
