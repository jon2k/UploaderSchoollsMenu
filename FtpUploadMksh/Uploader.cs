using System;

namespace FtpUploadMksh
{
    public class DataFromFtp
    {
        public string Message { get; set; }
        public DateTime TimeUpdate { get; set; }
    }

    public class Uploader : IUploader
    {
        private IFtp _ftpFile;
        public Uploader(IFtp ftp)
        {
            _ftpFile = ftp;
        }
        public DataFromFtp Upload(string path)
        {
            try
            {
                var exist = _ftpFile.CheckExistFileInServer();
                if (exist)
                {
                    _ftpFile.DeleteFileInServer();
                }
                return new DataFromFtp()
                {
                    Message = _ftpFile.UploadFileInServer(path),
                    TimeUpdate = _ftpFile.GetTimeFileInServer()
                };
            }
            catch
            {
                throw;
            }
        }
        public DateTime GetTimeModify()
        {
            try
            {
                return _ftpFile.GetTimeFileInServer();
            }
            catch
            {
                throw;
            }
        }
    }
}
