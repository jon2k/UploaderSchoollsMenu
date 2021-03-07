using System;
using System.Configuration;
using UploaderMenu.Services;

namespace UploaderMenu.Model
{
    public class DataFtpConnection : IDataFtpConnection
    {
        public FtpCfg GetFtpCfg()
        {
            FtpCfg ftpCfg;
            try
            {
                ftpCfg = new FtpCfg()
                {
                    Ftp = ConfigurationManager.AppSettings.Get("ftp"),
                    Folder = ConfigurationManager.AppSettings.Get("folder"),
                    Login = ConfigurationManager.AppSettings.Get("login"),
                    Pasword = ConfigurationManager.AppSettings.Get("password")
                };
                if (string.IsNullOrEmpty(ftpCfg.Ftp))
                {
                    throw new Exception(message: $"Не удалось извлечь адрес ftp сервера из настроек.");
                }
                else if(string.IsNullOrEmpty(ftpCfg.Folder))
                {
                    throw new Exception(message: $"Не удалось извлечь название папки из настроек ftp сервера.");
                }
                else if (string.IsNullOrEmpty(ftpCfg.Login))
                {
                    throw new Exception(message: $"Не удалось извлечь логин из настроек ftp сервера.");
                }
                else if (string.IsNullOrEmpty(ftpCfg.Pasword))
                {
                    throw new Exception(message: $"Не удалось извлечь пароль из настроек ftp сервера.");
                }

                return ftpCfg;
            }
            catch
            {
                throw;
            }
        }
    }
}
