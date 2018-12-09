﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Search;

using Newtonsoft.Json;

using Whiskey_Tycoon.lib.PlatformAbstractions;

namespace Whiskey_Tycoon.UWP.PlatformImplementations
{
    public class FileSystem : IFileSystem
    {
        private readonly Windows.Storage.StorageFolder _storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public bool WriteFile<T>(string fileName, object obj)
        {
            throw new System.NotImplementedException();
        }

        public T GetFile<T>(string fileName)
        {
            throw new System.NotImplementedException();
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
                    // TODO Log
                    continue;
                }
            }

            return objects;
        }
    }
}