namespace FinalExam.Utilities.Image
{
    public static class FileUploadExtension
    {
        public static string SaveImage(this IFormFile file,IWebHostEnvironment env,string folder)
        {
            string path=Path.Combine(env.WebRootPath,folder);
            string filename=Guid.NewGuid().ToString()+"_"+file.FileName;
            string fullpath=Path.Combine(path,filename);
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using(FileStream stream=new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }
    }
}
