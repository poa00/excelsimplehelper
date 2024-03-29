﻿using Model.DataBase.Model;
using Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Data.SpecificationDataDocument
{
    /// <summary>
    /// Данные для свидетельства и удостоверений
    /// </summary>
    public class EvidenceAndUdostovereniye
    {
        private StudentRecord[] CorrectRecords;// Массив откоректированных записей который возвращается

        private Dictionary<int, string> date;

        private string NumberCard;
        private string StartEducation;
        private string EndEducation;
        private string IssueDocumentDate;
        private Programs Programm;
        private SpecFunction SpecFunction_;
        const byte DEGGERENCE_BETWEEN_BEGINNING_ARRAY_AND_FILE = 1;

        public EvidenceAndUdostovereniye(StudentRecord[] records, string startEducation, string endEducation, string issueDocumentDate, string numberCard, Programs programm)
        {
            CorrectRecords = records;

            NumberCard = numberCard;
            StartEducation = startEducation;
            EndEducation = endEducation;
            IssueDocumentDate = issueDocumentDate;
            Programm = programm;
            SpecFunction_ = new SpecFunction();
            date = new Dictionary<int, string>
            { 
                { 1, "Д" },// день
                { 2, "М" },// месяц
                { 3, "Г" } // год
            };
        }

        public StudentRecord[] GetRecords()
        {
            return CorrectRecords;
        }

        /// <summary>
        /// Корректирует данные под удостоверение/свидетельство
        /// </summary>
        public void Correction()
        {
            for (byte i = 0; i < CorrectRecords.Length; i++)
            {
                CorrectRecords[i] = SpecFunction_.CorrectFIO(CorrectRecords[i], i);
                CorrectRecords[i] = CorrectNumberSertificate(CorrectRecords[i], i);
                CorrectRecords[i] = CorrectDate(CorrectRecords[i]);
                CorrectRecords[i] = CorrectMark(CorrectRecords[i], i);
                CorrectRecords[i] = CorrectProgram(CorrectRecords[i]);
                CorrectRecords[i] = CorrectGroup(CorrectRecords[i]);
            }            
        }

        /// <summary>
        /// Коррекитровка данных для загрузки сертификата данными из базы данных
        /// </summary>
        public void CorrectionLoad()
        {
            for (int i = 0; i < CorrectRecords.Length; i++)
            {
                CorrectRecords[i] = CorrectDate(CorrectRecords[i]);
                CorrectRecords[i] = CorrectProgram(CorrectRecords[i]);
            }
        }

        /// <summary>
        /// Добавляет программу и данные о ней
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <returns></returns>
        private StudentRecord CorrectProgram(StudentRecord dataStudent)
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
        private StudentRecord CorrectNumberSertificate(StudentRecord dataStudent, int id)
        {
            id = id + DEGGERENCE_BETWEEN_BEGINNING_ARRAY_AND_FILE;
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
        /// Добавляет название группы
        /// </summary>
        /// <param name="dataStudent"></param>
        /// <returns></returns>
        private StudentRecord CorrectGroup(StudentRecord dataStudent)
        {
            dataStudent.AddPropertyRecord("Группа", NumberCard.Trim());
            
            return dataStudent;
        }


        /// <summary>
        /// Корректировка дат под сертификат (Н - начало обучения, К - конец обучения, П - Получение)
        /// </summary>
        /// <param name="idStudent"> Номер студента</param>
        private StudentRecord CorrectDate(StudentRecord dataStudent)
        {
            for (byte i = 1; i < 4; i++)
            {
                if (i == 2)
                {
                    dataStudent.AddPropertyRecord("Н" + date[i], SpecFunction_.ConvertingNumberPerMonth(SpecFunction_.GetNumberData(StartEducation, i)).Trim());
                    dataStudent.AddPropertyRecord("К" + date[i], SpecFunction_.ConvertingNumberPerMonth(SpecFunction_.GetNumberData(EndEducation, i)).Trim());
                    dataStudent.AddPropertyRecord("П" + date[i], SpecFunction_.ConvertingNumberPerMonth(SpecFunction_.GetNumberData(IssueDocumentDate, i)).Trim());
                }
                else
                {
                    dataStudent.AddPropertyRecord("Н" + date[i], SpecFunction_.GetNumberData(StartEducation, i).Trim());
                    dataStudent.AddPropertyRecord("К" + date[i], SpecFunction_.GetNumberData(EndEducation, i).Trim());
                    dataStudent.AddPropertyRecord("П" + date[i], SpecFunction_.GetNumberData(IssueDocumentDate, i).Trim());
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
        private StudentRecord CorrectMark(StudentRecord dataStudent, int id)
        {
            string mark = dataStudent.GetOneStudent()["Оценка"];
            if (mark.Equals("5"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "пять");
                return dataStudent;
            }
            if (mark.Equals("4"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "четыре");
                return dataStudent;
            }
            if (mark.Equals("3"))
            {
                dataStudent.RemoveRecord("Оценка");
                dataStudent.AddPropertyRecord("Оценка", "три");
                return dataStudent;
            }
            
            return dataStudent;
        }
        
    }
}
