using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Model.Write.Word.Document
{
    interface IPropertiesDocument
    {
        Run GetPropertiesEvidenceAndUdostovereniye(string bookmark, Text textElement);
        Run GetPropertiesCertificateDangerous(string bookmark, Text textElement);
    }
}
