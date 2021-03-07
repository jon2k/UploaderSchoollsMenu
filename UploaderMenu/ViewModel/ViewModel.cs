using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UploaderMenu.Model;

namespace UploaderMenu
{

    public class ViewModelMain : ViewModelBase
    {
        private ICommand _updateTime;

        public ICommand UpdateTime => _updateTime ?? (_updateTime = new RelayCommand(() => UpdateTimeNow()));
       
        public ObservableCollection<ViewModelUpload> AllMenu { get; set; } = new ObservableCollection<ViewModelUpload>();

        public ViewModelMain(FtpCfg ftpCfg)
        {
            AllMenu.Add( new ViewModelUpload(ftpCfg, "Детский сад", "menu1", "pdf"));
            AllMenu.Add(new ViewModelUpload(ftpCfg, "Начальня школа", "menu2", "pdf"));
            AllMenu.Add(new ViewModelUpload(ftpCfg, "Основная школа", "menu3", "pdf"));         
        }

        public void UpdateTimeNow()
        {          
            foreach (var item in AllMenu)
            {
                item.UpdateTimeNow();
            }
        }
    }
}
