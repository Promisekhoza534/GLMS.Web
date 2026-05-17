namespace GLMS.Web.Services
{
    public class FileValidationService
    {
        public bool IsPdfFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            return Path.GetExtension(fileName).ToLower() == ".pdf";
        }
    }
}