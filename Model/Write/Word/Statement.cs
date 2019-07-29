using Model.Data;
using Model.Data.PatternMVVM;
using Model.Data.SpecificationDataDocument;
using Model.File;
using Model.Message;
using System.Collections.Generic;
using Xceed.Words.NET;

namespace Model.Write.Word
{
    /// <summary>
    /// Ведомость
    /// </summary>
    public class Statement
    {
        //Количество колонок в таблице
        private const int COLUMN_REGISTER = 5;
        // Название программы обучения
        private string ProgramName;

        Table TableRegister;
        // Размер отступа в таблице
        private int SizeIndentationHanging;
        private string[] BookamrkStatement;
        // Номер студента в ведомости
        public string Number;
        // Группа обучения
        public string Group;
        private StudentRecord[] records;

        public Statement(StudentRecord[] record, string programName, string group)
        {
            ProgramName = programName;
            Group = group;
            BookamrkStatement = new string[2] { "группа", "Программа" };
            SizeIndentationHanging = 300;
            records = record;
        }
        public Statement(StudentRecord[] record, string programName, string group, string number)
        {
            ProgramName = programName;
            Number = number;
            Group = group;
            BookamrkStatement = new string[2] { "группа", "Программа" };
            SizeIndentationHanging = 300;
            records = record;
        }

        /// <summary>
        /// Настройка колонок таблицы
        /// </summary>
        private void SettingStatement()
        {
            TableRegister.Design = TableDesign.TableGrid;

            TableRegister.Rows[0].Cells[0].Paragraphs[0].Append("№ п/п").IndentationHanging = SizeIndentationHanging;
            TableRegister.Rows[0].Cells[0].Width = 18;
            TableRegister.Rows[0].Cells[1].Paragraphs[0].Append("ФИО слушателя").IndentationHanging = SizeIndentationHanging;
            TableRegister.Rows[0].Cells[1].Width = 220;
            TableRegister.Rows[0].Cells[2].Paragraphs[0].Append("Дата рождения слушателя").IndentationHanging = SizeIndentationHanging;
            TableRegister.Rows[0].Cells[3].Paragraphs[0].Append("Номер сертификата").IndentationHanging = SizeIndentationHanging;
            TableRegister.Rows[0].Cells[4].Paragraphs[0].Append("Роспись лица, получившего удостоверение").IndentationHanging = SizeIndentationHanging;
        }

        /// <summary>
        /// Создание таблицы со списком студентов
        /// </summary>
        /// <returns></returns>
        public Table CreateTable()
        {
            for (int row = 1; row <= records.Length; row++)
            {
                TableRegister.Rows[row].Cells[0].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[0].Width = 25;
                TableRegister.Rows[row].Cells[0].Paragraphs[0].Append(row.ToString()).FontSize(11);

                TableRegister.Rows[row].Cells[1].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[1].Width = 220;

                TableRegister.Rows[row].Cells[1].Paragraphs[0].Append(records[row - 1].GetOneStudent()["Фамилия"] + " " + " " + records[row - 1].GetOneStudent()["Имя"] + " " + records[row - 1].GetOneStudent()["Отчество"]).FontSize(11);

                TableRegister.Rows[row].Cells[2].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[2].Paragraphs[0].Append(records[row - 1].GetOneStudent()["ДатаРождения"]).FontSize(11);

                TableRegister.Rows[row].Cells[3].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[3].Paragraphs[0].Append(records[row - 1].GetOneStudent()["Номер"]).FontSize(11);

                TableRegister.Rows[row].Cells[4].Paragraphs[0].Append(" ").FontSize(11);
            }
            return TableRegister;
        }


        /// <summary>
        /// Создание документа ведомость
        /// </summary>
        /// <param name="records"></param>
        public List<string> DocumentCreate()
        {
            using (var document = DocX.Load(Properties.Settings.Default.PathFileWordStatementTemplate))
            {
                TableRegister = document.AddTable(records.Length + 1, COLUMN_REGISTER);
                SettingStatement();
                var NameProgrammBookmark = document.Bookmarks["Программа"];
                NameProgrammBookmark.SetText(records[0].GetOneStudent()["Программа"]);

                var IdGroupBookmark = document.Bookmarks["группа"];
                IdGroupBookmark.SetText(Group);

                document.InsertTable(CreateTable());

                document.InsertParagraph("Методист АУЦ					Ю.А. Нестеренко ").FontSize(14).Bold();
                document.SaveAs(Properties.Settings.Default.PathFolderResult + "\\Ведомость_" + Group + "-группы.doc");
            }
            return MessageBug.GetMessages();
        }
    }
}