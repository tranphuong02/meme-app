using System.IO;
using System.Web.Hosting;

namespace Core.Utils
{
    public static class FileUtils
    {
        public static string MapPath(string fileName)
        {
            return HostingEnvironment.MapPath(fileName);
        }
        public static bool IsExist(string fileName)
        {
            var filePath = MapPath(fileName);
            return filePath != null && File.Exists(filePath);
        }

        public static void Delete(string fileName)
        {
            if(IsExist(fileName))
                File.Delete(MapPath(fileName));
        }
    }
}
