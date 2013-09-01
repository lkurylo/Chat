using System;
using System.IO;
using System.Web;

namespace FPChat.Domain.Utilities
{
    /// <summary>
    /// Contains a utilities for files.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Returns a specific text-based file content. 
        /// </summary>
        /// <param name="fileName">Relative path to file.</param>
        /// <returns>Empty if file doesn't exists.</returns>
        public static string GetFileContent(string fileName)
        {
            string file = HttpRuntime.AppDomainAppPath + fileName;
            if (File.Exists(file))
            {
                return File.ReadAllText(file);
            }

            return String.Empty;
        }

        /// <summary>
        /// The enum type containg a specific file extensions.
        /// </summary>
        public enum FileExtension{
            JS,
            CSS,
            OTHER
        }

        /// <summary>
        /// Returns the file extension as a enum field.
        /// </summary>
        /// <param name="fileName">File name to check the extension.</param>
        /// <returns>The enum field specific for the appropriate extension.</returns>
        public static FileExtension GetFileEntension(string fileName)
        {
            FileExtension result;
            string extension = Path.GetExtension(fileName);
            switch (extension.ToLower())
            {
                case ".css":
                    result = FileExtension.CSS;
                    break;
                case ".js":
                    result = FileExtension.JS;
                    break;
                default:
                    result = FileExtension.OTHER;
                    break;
            }

            return result;
        }
    }
}
