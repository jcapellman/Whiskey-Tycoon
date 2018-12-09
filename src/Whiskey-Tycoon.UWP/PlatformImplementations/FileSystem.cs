using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Search;

using Microsoft.Toolkit.Uwp.Helpers;

using Newtonsoft.Json;

using NLog;

using Whiskey_Tycoon.lib.PlatformAbstractions;

namespace Whiskey_Tycoon.UWP.PlatformImplementations
{
    public class FileSystem : IFileSystem
    {
        private Logger log = NLog.LogManager.GetCurrentClassLogger();

        private readonly Windows.Storage.StorageFolder _storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public async Task<bool> WriteFileAsync<T>(string fileName, object obj)
        {
            try
            {
                var result = await _storageFolder.WriteTextToFileAsync(JsonConvert.SerializeObject(obj), fileName);

                return result != null;
            }
            catch (Exception ex)
            {
                log.Error(ex, $"Error writing to {fileName}, object: {obj}");

                return false;
            }
        }

        public async Task<T> GetFileAsync<T>(string fileName)
        {
            try
            {
                var textResult = await _storageFolder.ReadTextFromFileAsync(fileName);

                return JsonConvert.DeserializeObject<T>(textResult);
            }
            catch (Exception ex)
            {
                log.Error(ex, $"Error reading from {fileName}");

                return default(T);
            }
        }

        public async Task<List<T>> GetFilesAsync<T>(string extension)
        {
            var objects = new List<T>();

            var options = new QueryOptions();

            options.FileTypeFilter.Add($".{extension}");
            options.FolderDepth = FolderDepth.Deep;

            var query = _storageFolder.CreateFileQueryWithOptions(options);

            var saveGames = await query.GetFilesAsync();

            foreach (var saveGame in saveGames)
            {
                var gameObjectString = await FileIO.ReadTextAsync(saveGame);

                if (string.IsNullOrEmpty(gameObjectString))
                {
                    continue;
                }

                try
                {
                    var gameObject = JsonConvert.DeserializeObject<T>(gameObjectString);

                    objects.Add(gameObject);
                }
                catch (JsonException)
                {
                    log.Error($"Error parsing {saveGame.Path} from JSON to Object");
                }
            }

            return objects;
        }
    }
}