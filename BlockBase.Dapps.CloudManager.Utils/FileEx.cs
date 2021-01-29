using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Utils
{
    public static class FileEx
    {

        private const int DefaultBufferSize = 4096;

        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static Task<string> ReadFileAsync(string path)
        {
            return ReadFileAsync(path, Encoding.UTF8);
        }

        public static async Task<string> ReadFileAsync(string path, Encoding encoding)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
                using (var reader = new StreamReader(stream, encoding))
                {
                    return await reader.ReadToEndAsync();   
                }  
        }
    }
}
