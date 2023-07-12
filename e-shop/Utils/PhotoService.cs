using ConsoleApp1;
using System.Web;
namespace e_shop.Utils
{
    public class PhotoService
    {
        private static string _baseFilePath = $@"{Environment.CurrentDirectory}\wwwroot\images\uploaded\";

        private IFormFile _formFile;

        private string _fullFilePath;

        public string GetFileName() => Path.GetFileName(_fullFilePath);

        public PhotoService(IFormFile formFile)
        {
            this._formFile = formFile;

            this._fullFilePath = GetFilePath();
        }

        private string GetFilePath(int number = 0)
        {
            string path = $"{_baseFilePath}photo_{number}.jpg";
            
            return File.Exists(path) ? GetFilePath(++number) : path;
        }

        public void Update(Photo photo)
        {
            if (_formFile == null || _formFile.Length <= 0)
            {
                return;
            }

            Save($"{_baseFilePath}{photo.Name}");
        }

        private void Save(string path)
        {
            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                _formFile.CopyTo(fileStream);
            };
        }

        public void Save()
        {
            Save(_fullFilePath);
        }

        public static void Remove(string name)
        {
            string number = name.Split('_')[1];

            File.Delete($"{_baseFilePath}_{number}");
        }

        public static void Remove(Photo photo)
        {
            PhotoService.Remove(photo.Name);
        }
    }
}
