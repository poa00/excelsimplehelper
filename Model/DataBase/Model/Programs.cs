using Model.Data.PatternMVVM.TrainingProgramm;
using Model.DataBase.Context;
using Model.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Data.Entity.Validation;

namespace Model.DataBase.Model
{
    [Table("Programs")]
    public partial class Programs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Programs()
        {
            Certificate = new HashSet<Certificate>();
        }
        
        public int id { get; set; }

        [Required]
        public string training { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string clock { get; set; }

        public int lessonId { get; set; }

        public int typeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Certificate> Certificate { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual TypeDocument TypeDocument { get; set; }

        public void AddProgramm(Programm012Model programm012Model, string type)
        {
            string tmp = "для Свидетельства/Удостоверения";
            if (programm012Model == null)
            {
                MessageBug.AddMessage(MessageBug.message.Модель_не_заполнена, tmp);
                return;
            }
            if (programm012Model.Name == null || programm012Model.Name == " " || programm012Model.Name == "")
            {
                MessageBug.AddMessage(MessageBug.message.Название_программы_не_заполнено, tmp);
                return;
            }
            if (programm012Model.Lesson == null || programm012Model.Lesson == " " || programm012Model.Lesson == "")
            {
                MessageBug.AddMessage(MessageBug.message.Уроки_не_заполнены, tmp);
                return;
            }
            if (programm012Model.Type == null)
            {
                string bug = (MessageBug.message.Тип_не_выбран).ToString() + " для Свидетельства/Удостоверения";
                MessageBug.AddMessage(MessageBug.message.Тип_не_выбран, tmp);
                return;
            }
            if (type.Equals("Свидетельство") && programm012Model.Training == null || programm012Model.Training == " " || programm012Model.Training == "")
            {
                MessageBug.AddMessage(MessageBug.message.Повышение_квалификации_не_заполнено, "для Свидетельства");
                return;
            }
            if(!type.Equals("Свидетельство"))
            {
                programm012Model.Training = "Тут ничего нет";
            }
            if (programm012Model.Clock == null || programm012Model.Clock == " " || programm012Model.Clock == "")
            {
                MessageBug.AddMessage(MessageBug.message.Часы_не_заполнены, tmp);
                return;
            }

            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Lesson lesson = new Lesson();
                TypeDocument typeDocument = new TypeDocument();
                int idTypeDocument = typeDocument.ConvertNameToId(type);
                int idLesson = lesson.AddLesson(dataBaseContext, programm012Model.Lesson);
                if (idLesson == -1 || idTypeDocument == -1)
                {
                    return;
                }
                Programs programs = new Programs
                {
                    name = programm012Model.Name,
                    training = programm012Model.Training,
                    clock = programm012Model.Clock,
                    lessonId = idLesson,
                    typeId = idTypeDocument
                };
                dataBaseContext.Programs.Add(programs);
                dataBaseContext.SaveChanges();
            }
        }
    }
}
