using System.Collections.Generic;
using System.Threading.Tasks;

namespace Whiskey_Tycoon.lib.PlatformAbstractions
{
    public interface IFileSystem
    {
        Task<bool> WriteFileAsync<T>(string fileName, object obj);

        Task<T> GetFileAsync<T>(string fileName);

        Task<List<T>> GetFilesAsync<T>(string extension);

        Task<bool> DeleteFileAsync(string fileName);
    }
}