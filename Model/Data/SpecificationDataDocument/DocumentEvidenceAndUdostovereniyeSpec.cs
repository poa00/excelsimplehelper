using Model.DataBase.Model;
using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Data.SpecificationDataDocument
{
    public class DocumentEvidenceAndUdostovereniyeSpec : SpecFunction
    {
        private Record[] InlideRecords; // Массив записей с изначальным набором данных
        private Record[] CorrectRecords;// Массив записей oткоректированый который возвращается
        
        private Dictionary<int, string> date;

        private string NumberCard;
        private string StartEducation;
        private string EndEducation;
        private string IssueDocumentDate;
        private Programs Programm;

        public DocumentEvidenceAndUdostovereniyeSpec(Record[] records, string startEducation, string endEducation, string issueDocumentDate, string numberCard, Programs programm)
        {
            InlideRecords = records;
            CorrectRecords = new Record[InlideRecords.Length];

            NumberCard = numberCard;
            StartEducation = startEducation;
            EndEducation = endEducation;
            IssueDocumentDate = issueDocumentDate;
            Programm = programm;
            
            date = new Dictionary<int, string>
            { 
                { 1, "Д" },
                { 2, "М" },
                { 3, "Г" }
            };
        }

        public Record[] GetRecords()
        {
            return CorrectRecords;
        }

        /// <summary>
        /// Корректирует данные под удостоверение/свидетельство
        /// </summary>
        public void Correction()
        {
            for (int i = 0; i < InlideRecords.Length; i++)
            {
                InlideRecords[i] = CorrectFIO(InlideRecords[i]);
                InlideRecords[i] = CorrectNumberSertificate(InlideRecords[i], i);
                InlideRecords[i] = CorrectDate(InlideRecords[i]);
                InlideRecords[i] = CorrectMark(InlideRecords[i], i);
                InlideRecords[i] = CorrectProgram(InlideRecords[i]);
                CorrectRecords[i] = InlideRecords[i];
            }            
        }

        public void CorrectionLoad()
        {
            for (int i = 0; i < InlideRecords.Length; i++)
            {
                InlideRecords[i] = CorrectDate(InlideRecords[i]);
                InlideRecords[i] = CorrectProgram(InlideRecords[i]);
                CorrectRecords[i] = InlideRecords[i];
            }
        }

        private Record CorrectProgram(Record dataStudent)
        {
            dataStudent.AddPropertyRecord("Программа", Programm.name);
            dataStudent.AddPropertyRecord("Уроки", Programm.Lesson.Name.ToString());
            dataStudent.AddPropertyRecord("ПовышенияКвалификации", Programm.training);
            dataStudent.AddPropertyRecord("Часы", Programm.clock);
            dataStudent.AddPropertyRecord("Тип", Programm.TypeDocument.Name);

            return dataStudent;
        }

        /// <summary>
        /// Создаёт id в группе и добавляет 0 к цифрам (01).
        /// </summary>
        /// <param name="student"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Record CorrectNumberSertificate(Record dataStudent, int id)
        {
            id = id + 1;
            if (id < 10)
            {
                dataStudent.AddPropertyRecord("Номер", (NumberCard + "-0" + id).Trim());
            }
            else
            {
                dataStudent.AddPropertyRecord("Номер", (NumberCard + "-" + id).Trim());
            }
            return dataStudent;
        }

        /// <summary>
        /// Корректировка дат под сертификат
        /// </summary>
        /// <param name="idStudent"> Номер студента</param>
        private Record CorrectDate(Record dataStudent)
        {
            for (int i = 1; i < 4; i++)
            {
                if (i == 2)
                {
                    dataStudent.AddPropertyRecord("Н" + date[i], ConvertingNumberPerMonth(GetNumberData(StartEducation, i)).Trim());
                    dataStudent.AddPropertyRecord("К" + date[i], ConvertingNumberPerMonth(GetNumberData(EndEducation, i)).Trim());
                    dataStudent.AddPropertyRecord("П" + date[i], ConvertingNumberPerMonth(GetNumberData(IssueDocumentDate, i)).Trim());
                }
                else
                {
                    dataStudent.AddPropertyRecord("Н" + date[i], GetNumberData(StartEducation, i).Trim());
                    dataStudent.AddPropertyRecord("К" + date[i], GetNumberData(EndEducation, i).Trim());
                    dataStudent.AddPropertyRecord("П" + date[i], GetNumberData(IssueDocumentDate, i).Trim());
                }
            }
            return dataStudent;
        }

        /// <summary>
        /// Коррекция оценок
        /// </summary>
        /// <param name="student"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Record CorrectMark(Record dataStudent, int id)
        {
            string mark = dataStudent.GetOneStudent()["Оценка"];
            if (mark.Equals("5"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "пять");
                return dataStudent;
            }
            else
            {
                if (mark.Equals("пять"))
                {
                    dataStudent.RemoveRecord("Оценка");
                    dataStudent.AddPropertyRecord("Оценка", mark);
                    return dataStudent;
                }                    
            }
            if (mark.Equals("4"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "четыре");
                return dataStudent;
            }
            else
            {
                if (mark.Equals("четыре"))
                {
                    dataStudent.RemoveRecord("Оценка");
                    dataStudent.AddPropertyRecord("Оценка", mark);
                    return dataStudent;
                }
            }
            if (mark.Equals("3"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "три");
                return dataStudent;
            }
            else
            {
                if (mark.Equals("три"))
                {
                    dataStudent.RemoveRecord("Оценка");
                    dataStudent.AddPropertyRecord("Оценка", mark);
                    return dataStudent;
                }
            }
            if (mark.Equals("2"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "два");
                return dataStudent;
            }
            else
            {
                if (mark.Equals("два"))
                {
                    dataStudent.RemoveRecord("Оценка");
                    dataStudent.AddPropertyRecord("Оценка", mark);
                    return dataStudent;
                }
            }
            if (mark.Equals("1"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "единица");
                return dataStudent;
            }
            else
            {
                if (mark.Equals("единица"))
                {
                    dataStudent.RemoveRecord("Оценка");
                    dataStudent.AddPropertyRecord("Оценка", mark);
                    return dataStudent;
                }
            }
            
            MessageBug.AddMessage("Оценка неверно записана по индексу: " + (id + 1));
            return dataStudent;
        }
        
    }
}
