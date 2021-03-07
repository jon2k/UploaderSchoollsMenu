using System;
using System.Collections.Generic;
using System.Text;

namespace FtpUploadMksh
{
    public interface IUploader
    {
        DataFromFtp Upload(string path);
        DateTime GetTimeModify();
    }
}
