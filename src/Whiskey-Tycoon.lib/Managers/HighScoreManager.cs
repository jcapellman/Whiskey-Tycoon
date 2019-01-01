using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Whiskey_Tycoon.lib.Common;
using Whiskey_Tycoon.lib.JSONObjects;
using Whiskey_Tycoon.lib.PlatformAbstractions;

namespace Whiskey_Tycoon.lib.Managers
{
    public static class HighScoreManager
    {
        public static async Task<List<HighScoresObject>> GetHighScoresAsync(IFileSystem fileSystem)
        {
            var highScoresResult = await fileSystem.GetFileAsync<List<HighScoresObject>>(Constants.FILENAME_HIGHSCORES);

            return highScoresResult?.OrderByDescending(a => a.BottlesSold).ToList() ?? new List<HighScoresObject>();
        }

        public static async void AddHighScore(HighScoresObject highScoreObject, IFileSystem fileSystem)
        {
            var highScores = await GetHighScoresAsync(fileSystem);

            highScores.Add(highScoreObject);

            await fileSystem.WriteFileAsync<List<HighScoresObject>>(Constants.FILENAME_HIGHSCORES, highScores);
        }
    }
}