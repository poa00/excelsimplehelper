using Model.Data.PatternMVVM.TrainingProgramm;
using Model.DataBase.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.Programs
{
    public class Program012ViewModel
    {
        public Programm012Model Program012 { get; set; }
        DataBaseContext dataBaseContext { get; set; }

        public Program012ViewModel()
        {
            dataBaseContext = new DataBaseContext();
            dataBaseContext.Programs.Load();

            Program012 = new Programm012Model();
            Program012.Type = new List<string>(3);
            Program012.Type.Add("Свидетельство");
            Program012.Type.Add("Удостоверения (Лицензия)");
            Program012.Type.Add("Удостоверение (Реквизит)");
            Program012.Programs = dataBaseContext.Programs.Local.ToBindingList();
        }
        
        RelayCommand addProgramm;
        // команда Добавления программы
        public RelayCommand AddProgramm
        {
            get
            {
                return addProgramm ??
                  (addProgramm = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      string type = selectedItem as string;
                      Model.DataBase.Model.Programs programs = new Model.DataBase.Model.Programs();
                      programs.AddProgramm(Program012, type);
                    }));
            }
        }

        RelayCommand programmDeleteDelete;
        // команда удаления
        public RelayCommand ProgrammDeleteDelete
        {
            get
            {
                return programmDeleteDelete ??
                  (programmDeleteDelete = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Model.DataBase.Model.Programs programs = selectedItem as Model.DataBase.Model.Programs;
                      dataBaseContext.Programs.Remove(programs);
                      dataBaseContext.SaveChanges();
                  }));
            }
        }
    }
}
