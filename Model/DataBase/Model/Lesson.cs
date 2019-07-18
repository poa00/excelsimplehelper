using global::Model.DataBase.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Linq;


namespace Model.DataBase.Model
{
    [Table("Lesson")]
    public partial class Lesson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lesson()
        {
            Programs = new HashSet<Programs>();
        }

        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programs> Programs { get; set; }

        private string lessonCorrect(string lesson)
        {
            string lessonCorrect = " ";
            lessonCorrect = lesson.Replace('.', (char)13);
            return lessonCorrect;
        }

        /// <summary>
        /// Поиск сертификата по номеру
        /// </summary>
        /// <param name="context">доступ к базе данных</param>
        /// <param name="group">номер</param>
        /// <returns></returns>
        public int FindIdLesson(DataBaseContext context, string lesson)
        {
            int idLesson = -1;
            idLesson = context.Lesson
                        .Where(c => c.Name == lesson).First().id;
            return idLesson;
        }

        private bool isLesson(DataBaseContext dataBaseContext, string lesson)
        {
            var Lesson = dataBaseContext.Lesson
                                .Where(c => c.Name == lesson);
            if (Lesson.Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Добавляю уроки и получаю id это записи
        /// </summary>
        /// <param name="lesson">уроки</param>
        /// <returns>id уроков в БД</returns>
        public int AddLesson(DataBaseContext dataBaseContext, string lesson)
        {
            int id = -1;
            //using (DataBaseContext dataBaseContext = new DataBaseContext())
            //{
                Lesson Lessons = new Lesson();
                Lessons.Name = lessonCorrect(lesson);
                if (!isLesson(dataBaseContext, Lessons.Name))
                {
                    dataBaseContext.Lesson.Add(Lessons);
                    dataBaseContext.SaveChanges();
                    id = FindIdLesson(dataBaseContext, Lessons.Name);
                    return id;
                }
            id = FindIdLesson(dataBaseContext, Lessons.Name);
            return id;
            //}
        }
    }
}
