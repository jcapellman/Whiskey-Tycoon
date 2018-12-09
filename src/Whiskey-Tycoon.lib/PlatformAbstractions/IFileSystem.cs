using System.Collections.Generic;
using System.Threading.Tasks;

namespace Whiskey_Tycoon.lib.PlatformAbstractions
{
    public interface IFileSystem
    {
        bool WriteFile<T>(string fileName, object obj);

        T GetFile<T>(string fileName);

        Task<List<T>> GetFilesAsync<T>(string extension);
    }
}