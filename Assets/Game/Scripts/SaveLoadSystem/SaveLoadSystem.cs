using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.SaveLoad
{
    /// <summary>
    /// Save Load Handler
    /// </summary>
    public class SaveLoadSystem
    {
        public readonly string SaveFileLocation = Path.Combine(Application.persistentDataPath, "saves");
        public readonly string optionsFileLocation = Application.persistentDataPath;
        public const string optionsFileName = "options.json";

        /// <summary>
        /// Save game to location
        /// </summary>
        /// <param name="saveFile">game file name</param>
        /// <param name="gameData">game data</param>
        public async Task SaveGameData(string saveFile, string gameData)
        {
            await SaveFile(SaveFileLocation, $"{saveFile}.sav", gameData);
        }

        /// <summary>
        /// Load game from location
        /// </summary>
        /// <param name="saveFile">game file name</param>
        /// <returns>game data</returns>
        public async Task<string> LoadGameData(string saveFile)
        {
            return await LoadFile(SaveFileLocation, $"{saveFile}.sav");
        }
        
        /// <summary>
        /// Save OptionData
        /// </summary>
        /// <param name="optionsData">Option Data</param>
        public async Task SaveOptions(string optionsData)
        {
            await SaveFile(optionsFileName, optionsFileLocation, optionsData);
        }

        /// <summary>
        /// Has Options files
        /// </summary>
        /// <returns>true when options file exist</returns>
        public bool HasOptions()
        {
            if (!File.Exists(optionsFileLocation))
            {
                return false;
            }
            string filePath = Path.Combine(optionsFileLocation, optionsFileName);
            
            return File.Exists(filePath);
        }

        /// <summary>
        /// Load OptionData
        /// </summary>
        /// <returns>Option Data</returns>
        public async Task<string> LoadOptions()
        {
            return await LoadFile(optionsFileName, optionsFileLocation);
        }
        
        /// <summary>
        /// Save Files to location
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <param name="fileLocation">file location</param>
        /// <param name="data">data to be saved</param>
        public async Task SaveFile(string fileName,string fileLocation, string data)
        {
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }
            
            string filePath = Path.Combine(fileLocation, fileName);
            await File.WriteAllTextAsync(filePath, data);
        }

        /// <summary>
        /// Load Files from location
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <param name="fileLocation">file location</param>
        /// <returns>data saved</returns>
        public async Task<string> LoadFile(string fileName, string fileLocation)
        {
            if (!File.Exists(fileLocation))
            {
                await Task.Delay(1);
                return "";
            }
            
            string filePath = Path.Combine(fileLocation, fileName);
            return await File.ReadAllTextAsync(filePath);
        }
    }
}
