using System;
using System.IO;
using System.Net;

namespace FtpUploadMksh
{
    public class Ftp : IFtp
    {
        private string _ftp;
        private string _login;
        private string _password;
        private string _nameFolders;
        private string _nameFile;
        private string _extension;
        private bool _ssl;
        public Ftp(string ftp, string nameFolders, string nameFile, string extension, string login, string password, bool NeedSsl=false)
        {
            _ftp = ftp;
            _login = login;
            _password = password;
            _extension = extension;
            _nameFolders = nameFolders;
            _nameFile = nameFile;
            _ssl = NeedSsl;
        }
        public bool CheckExistFileInServer()
        {
            try
            {
                var _request = (FtpWebRequest)WebRequest.Create(_ftp + "//" + _nameFolders + "/" + _nameFile + "." + _extension);
                _request.Credentials = new NetworkCredential(_login, _password);
                _request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                FtpWebResponse response = (FtpWebResponse)_request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                var data = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                response.Close();
                return data.Equals(string.Empty) ? false : true;
            }
            catch (Exception e)
            {
                throw new Exception(message: $"Невозможно проверить существование файла на сервере. {e.Message}");
            }
        }
        public DateTime GetTimeFileInServer()
        {
            try
            {
                var _request = (FtpWebRequest)WebRequest.Create(_ftp + "//" + _nameFolders + "/" + _nameFile + "." + _extension);
                _request.Credentials = new NetworkCredential(_login, _password);
                _request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                FtpWebResponse response = (FtpWebResponse)_request.GetResponse();
                var res= response.LastModified;
                         
                response.Close();
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(message: $"Невозможно узнать время обновления файла на сервере. {e.Message}");
            }
        }
        public bool DeleteFileInServer()
        {
            try
            {
                var _request = (FtpWebRequest)WebRequest.Create(_ftp + "//" + _nameFolders + "/" + _nameFile + "." + _extension);
                _request.Credentials = new NetworkCredential(_login, _password);
                _request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)_request.GetResponse();
      
                response.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(message: $"Невозможно удалить файл на сервере. {e.Message}");
            }
        }
        public string UploadFileInServer(string pathFile)
        {
            var ext = pathFile.Split('.');
            if (ext[ext.Length - 1].ToUpper() == _extension.ToUpper())
            {
                try
                {
                    var _request = (FtpWebRequest)WebRequest.Create(_ftp + "//" + _nameFolders + "/" + _nameFile + "." + _extension);
                    _request.Credentials = new NetworkCredential(_login, _password);
                    // Устанавливаем метод на загрузку файлов.
                    _request.Method = WebRequestMethods.Ftp.UploadFile;
                    // Создаем поток для загрузки файла.
                    FileStream fs = new FileStream(pathFile, FileMode.Open);
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, fileContents.Length);
                    fs.Close();
                    _request.ContentLength = fileContents.Length;

                    // Пишем считанный в массив байтов файл в выходной поток.
                    Stream requestStream = _request.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();

                    // Получаем ответ от сервера в виде объекта FtpWebResponse.
                    FtpWebResponse response = (FtpWebResponse)_request.GetResponse();

                    var answer = $"Загрузка файлов завершена. Статус: {response.StatusDescription}";

                    response.Close();
                    return answer;
                }
                catch (Exception e)
                {
                    throw new Exception(message: $"Не удалось загрузить файл. {e.Message}");
                }

            }
            else
                throw new Exception(message: $"Расширение файла должно быть: {_extension}");

        }
    }
}
