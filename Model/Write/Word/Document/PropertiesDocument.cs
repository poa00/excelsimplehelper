using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Model.Write.Word.Document
{
    /// <summary>
    /// Настройки документа,
    /// возможно лучше это считывать из файла
    /// </summary>
    internal class PropertiesDocument : IPropertiesDocument
    {
        public Run GetPropertiesEvidenceAndUdostovereniye(string bookmark, Text textElement)
        {
            var run = new Run(textElement);
            RunProperties runProperties = new RunProperties(); // Create run properties.
            RunFonts runFont = new RunFonts();           // Create font
            runFont.Ascii = "Times New Roman";           // Specify font family
            FontSize fontSize = new FontSize() { Val = "22" };
            run.PrependChild<RunProperties>(runProperties);
            if (bookmark == "Фамилия" || bookmark == "Имя" || bookmark == "Отчество" ||
                bookmark == "ДатаРождения" || bookmark == "Номер")
            {
                fontSize = new FontSize() { Val = "28" };
            }
            if (bookmark == "Номер")
            {
                fontSize = new FontSize() { Val = "28" };
                Bold bold = new Bold();
                runProperties.Append(bold);
            }

            if (bookmark == "Оценка")
            {
                fontSize = new FontSize() { Val = "24" };
                Bold bold = new Bold();
                runProperties.Append(bold);
            }

            if (bookmark == "Уроки")
            {
                fontSize = new FontSize() { Val = "22" };
            }

            runProperties.Append(fontSize);
            runProperties.Append(runFont);

            return run;
        }

        public Run GetPropertiesCertificateDangerous(string bookmark, Text textElement)
        {
            var run = new Run(textElement);
            RunProperties runProperties = new RunProperties(); // Create run properties.
            RunFonts runFont = new RunFonts();           // Create font
            runFont.Ascii = "Times New Roman";           // Specify font family
            FontSize fontSize = new FontSize() { Val = "22" };
            Bold bold = new Bold();

            if (bookmark == "Фамилия" || bookmark == "Имя" || bookmark == "Отчество")
            {
                fontSize = new FontSize() { Val = "22" };
            }
            if (bookmark == "КогдаКемУтверждена")
            {
                fontSize = new FontSize() { Val = "20" };
            }
            if (bookmark == "Программа" || bookmark == "ДатаВыдачи")
            {
                fontSize = new FontSize() { Val = "22" };
                Underline underline = new Underline();
                underline.Val = UnderlineValues.Single;
                runProperties.Append(underline);
            }
            if (bookmark == "Номер")
            {
                fontSize = new FontSize() { Val = "24" };
            }

            runProperties.Append(runFont);
            runProperties.Append(bold);
            runProperties.Append(fontSize);
            run.PrependChild<RunProperties>(runProperties);
            return run;
        }

        public Run GetProperties(string typeDocument, string bookmark, Text textElement)
        {
            var run = new Run();
            if (typeDocument == "Сертификат ОГ")
            {
                run = GetPropertiesCertificateDangerous(bookmark, textElement);
            }
            if (typeDocument == "Свидетельство" || typeDocument == "Удостоверения (Лицензия)" || typeDocument == "Удостоверение (Реквизит)")
            {
                run = GetPropertiesEvidenceAndUdostovereniye(bookmark, textElement);
            }

            return run;
        }
    }
}
