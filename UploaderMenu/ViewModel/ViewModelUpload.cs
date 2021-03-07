using FtpUploadMksh;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;
using UploaderMenu.Model;

namespace UploaderMenu
{
    public class ViewModelUpload : ViewModelBase
    {
        private ICommand _openFileDialog;
        private ICommand _uploadFile;
        private IUploader _uploader;
        private string _path = string.Empty;

        public ViewModelUpload(FtpCfg ftpCfg, string name, string nameFile, string extension)
        {
            _uploader = new Uploader(new Ftp(ftpCfg.Ftp, ftpCfg.Folder, nameFile, extension, ftpCfg.Login, ftpCfg.Pasword));
            Name = name;
        }

        public string Name { get; set; }
        public string PathMenu { get => _path; set => _path = value; } 
        public DateTime TimeUpdate { get; set; }
        public ICommand OpenFileDialog => _openFileDialog ?? (_openFileDialog = new RelayCommand(() => OpenFileDialogNow()));
        public ICommand UploadFile => _uploadFile ?? (_uploadFile = new RelayCommand(() => UploadFileNow()));


        public void OpenFileDialogNow()
        {
            try
            {
                _path = FileDialog.GetPath();
            }
            catch (Exception e)
            {
                ShowMessegaBox(e.Message);
            }
            RaisePropertyChanged("");
        }
        public void UploadFileNow()
        {
            if (_path == string.Empty)
            {
                ShowMessegaBox("Не выбран файл для загрузки");
            }
            else
            {
                try
                {
                    var res = _uploader.Upload(_path);
                    TimeUpdate = res.TimeUpdate;
                    RaisePropertyChanged("");
                    ShowMessegaBox(res.Message);
                }
                catch (Exception e)
                {
                    ShowMessegaBox(e.Message);
                }
            }
            RaisePropertyChanged("");
        }
        public void UpdateTimeNow()
        {
            try
            {
                TimeUpdate = _uploader.GetTimeModify();
                RaisePropertyChanged("");
            }
            catch(Exception e)
            {
                ShowMessegaBox(e.Message);
            }
        }

        public void ShowMessegaBox(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
