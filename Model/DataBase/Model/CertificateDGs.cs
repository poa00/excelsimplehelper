using Model.Data;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Context;
using Model.Write.Word.Document;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Model.DataBase.Model
{
    public partial class CertificateDGs
    {
        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string issueDate { get; set; }

        [Required]
        [StringLength(10)]
        public string party { get; set; }

        public int idProgramDG { get; set; }

        public int idStudent { get; set; }

        public virtual ProgramDGs ProgramDGs { get; set; }

        public virtual Students Students { get; set; }

        public void LoadCertification(CertificateDGs certification)
        {
            ManagerDocument managerDocument = new ManagerDocument();
            managerDocument.DocumentLoad(certification);
        }

        public void SaveSertification(Record DataForDocuments)
        {
            ManagerDocument managerDocument = new ManagerDocument();
            managerDocument.DocumentSaveCertificateDG(DataForDocuments);
        }
    }
}
