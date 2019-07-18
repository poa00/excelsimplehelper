using Model.Data.PatternMVVM.TrainingProgramm;
using Model.DataBase.Context;
using System.Data.Entity;
using System.Windows.Input;

namespace ViewModel.Programs
{
    public class Program3ViewModel
    {
        public Programm3Model Program3 { get; set; }
        DataBaseContext dataBaseContext { get; set; }

        public Program3ViewModel()
        {
            Program3 = new Programm3Model();
            dataBaseContext = new DataBaseContext();
            dataBaseContext.ProgramDGs.Load();

            AddProgramm = new RelayCommand(arg => addProgramm());
            Program3.Programs = dataBaseContext.ProgramDGs.Local.ToBindingList();
        }

        public ICommand AddProgramm { get; set; }
        public void addProgramm()
        {
            Model.DataBase.Model.ProgramDGs programs = new Model.DataBase.Model.ProgramDGs();
            programs.AddProgramm(Program3);
        }

        RelayCommand certifictateProgrammDelete;
        // команда удаления
        public RelayCommand CertifictateProgrammDelete
        {
            get
            {
                return certifictateProgrammDelete ??
                  (certifictateProgrammDelete = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Model.DataBase.Model.ProgramDGs programs = selectedItem as Model.DataBase.Model.ProgramDGs;
                      dataBaseContext.ProgramDGs.Remove(programs);
                      dataBaseContext.SaveChanges();
                  }));
            }
        }
    }
}
