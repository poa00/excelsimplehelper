using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using Model.Data.PatternMVVM;
using Model.Data.SpecificationDataDocument;
using Model.DataBase.Model;
using Model.File;
using Model.Message;
using System;
using System.Collections.Generic;
using System.Text;
using Xceed.Words.NET;

namespace Model.Write.Word
{
    public class Statement
    {
        //Количество колонок в таблице
        private const int COLUMN_REGISTER = 5;
        // Название программы обучения
        private Programs Program;

        Table TableRegister;
        // Размер отступа в таблице
        private int SizeIndentationHanging;
        private string[] BookamrkStatement;
        private StatementSpec statementSpec;
        private StatementModel StatementModel;
        private FileExcel DateFromFile;

        public Statement(Programs program, StatementModel statementModel)
        {
            Program = program;
            StatementModel = statementModel;
            BookamrkStatement = new string[2] { "группа", "НаименованиеПрограммыУтверждения" };
            SizeIndentationHanging = 300;
            DateFromFile = new FileExcel(Properties.Settings.Default.TextPathFileExcelDataStudentsUdostovereniye, 1);
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
            for (int row = 1; row <= statementSpec.GetRecords().Length; row++)
            {
                TableRegister.Rows[row].Cells[0].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[0].Width = 25;
                TableRegister.Rows[row].Cells[0].Paragraphs[0].Append(row.ToString()).FontSize(11);

                TableRegister.Rows[row].Cells[1].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[1].Width = 220;


                TableRegister.Rows[row].Cells[1].Paragraphs[0].Append(statementSpec.GetRecords()[row - 1].GetOneStudent()["ФИО"]).FontSize(11);

                TableRegister.Rows[row].Cells[2].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[2].Paragraphs[0].Append(statementSpec.GetRecords()[row - 1].GetOneStudent()["ДатаРождения"]).FontSize(11);

                TableRegister.Rows[row].Cells[3].Paragraphs[0].IndentationHanging = SizeIndentationHanging;
                TableRegister.Rows[row].Cells[3].Paragraphs[0].Append(statementSpec.GetRecords()[row - 1].GetOneStudent()["группа"]).FontSize(11);

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
            if (MessageBug.GetMessages().Count > 0)
            {
                return MessageBug.GetMessages();
            }
            else
            {
                DateFromFile.ReadFile();
                statementSpec = new StatementSpec(DateFromFile.GetRecords(), StatementModel.Group, Program);
                statementSpec.Correction();
                using (var document = DocX.Load(Properties.Settings.Default.TextPathFileWordStatementTemplate))
                {
                    TableRegister = document.AddTable(statementSpec.GetRecords().Length + 1, COLUMN_REGISTER);
                    SettingStatement();
                    var NameProgrammBookmark = document.Bookmarks["НаименованиеПрограммыУтверждения"];
                    NameProgrammBookmark.SetText(statementSpec.GetRecords()[0].GetOneStudent()["НаименованиеПрограммыУтверждения"]);

                    var IdGroupBookmark = document.Bookmarks["группа"];
                    IdGroupBookmark.SetText(statementSpec.GetRecords()[0].GetOneStudent()["группа"]);

                    document.InsertTable(CreateTable());

                    document.InsertParagraph("Методист АУЦ					Ю.А. Нестеренко ").FontSize(14).Bold();
                    document.SaveAs(Properties.Settings.Default.TextPathFolderResult + "\\Ведомость_" + StatementModel.Group + "-группы.doc");
                }
                return MessageBug.GetMessages();
            }
        }
    }
}
