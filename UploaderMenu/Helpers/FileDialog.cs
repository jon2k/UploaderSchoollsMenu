using Microsoft.Win32;

namespace UploaderMenu
{
    public class FileDialog
    {

        public static string GetPath()
        {
            try
            {
                // Get path CSV file.
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF files (*.pdf;)|*.PDF";
                if (openFileDialog.ShowDialog() == true)
                {
                    return openFileDialog.InitialDirectory + openFileDialog.FileName;                  
                }
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }
    }
}
