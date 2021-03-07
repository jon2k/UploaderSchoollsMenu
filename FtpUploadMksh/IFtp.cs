using System;

namespace FtpUploadMksh
{
    public interface IFtp
    {
        bool CheckExistFileInServer();
        DateTime GetTimeFileInServer();
        bool DeleteFileInServer();
        string UploadFileInServer(string pathFile);
    }
}
