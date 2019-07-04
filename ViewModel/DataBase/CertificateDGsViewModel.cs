using Model.Data.PatternMVVM.DataBase;
using Model.DataBase.Context;
using Model.DataBase.Model;
using System.Data.Entity;

namespace ViewModel.DataBase
{
    public class CertificateDGsViewModel
    {
        public CertificateDGsModel certifications { get; set; }
        DataBaseContext DataBaseContext;
        RelayCommand certifictationEdit;
        RelayCommand certifictationDelete;
        RelayCommand certifictationLoad;

        public CertificateDGsViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.CertificateDGs.Load();

            certifications = new CertificateDGsModel()
            {
                Certifications = DataBaseContext.CertificateDGs.Local.ToBindingList()
            };
        }

        // команда редактирования
        public RelayCommand CertifictationEdit
        {
            get
            {
                return certifictationEdit ??
                  (certifictationEdit = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      CertificateDGs certification = selectedItem as CertificateDGs;

                      CertificateDGs certificateEdit = new CertificateDGs()
                      {
                          id = certification.id,
                          party = certification.party,
                          issueDate = certification.issueDate
                      };

                      // получаем измененный объект
                      certification = DataBaseContext.CertificateDGs.Find(certificateEdit.id);
                      if (certification != null)
                      {
                          certification.party = certificateEdit.party;
                          certification.issueDate = certificateEdit.issueDate;
                          DataBaseContext.Entry(certification).State = EntityState.Modified;
                          DataBaseContext.SaveChanges();
                      }

                  }));
            }
        }
        // команда удаления
        public RelayCommand CertifictationDelete
        {
            get
            {
                return certifictationDelete ??
                  (certifictationDelete = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      CertificateDGs certification = selectedItem as CertificateDGs;
                      DataBaseContext.CertificateDGs.Remove(certification);
                      DataBaseContext.SaveChanges();
                  }));
            }
        }
        /// <summary>
        /// Загрузка Сертификата
        /// </summary>
        public RelayCommand CertifictationLoad
        {
            get
            {
                return certifictationLoad ??
                  (certifictationLoad = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      CertificateDGs certification = selectedItem as CertificateDGs;
                      certification.LoadCertification(certification);
                  }));
            }
        }
    }
}
