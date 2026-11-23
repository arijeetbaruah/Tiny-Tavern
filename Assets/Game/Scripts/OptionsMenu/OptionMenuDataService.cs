using System;
using System.Threading.Tasks;
using Game.SaveLoad;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Game.OptionsMenu
{
    public class OptionMenuDataService : IInitializable
    {
        private OptionMenuSaveData _saveData;
        private SaveLoadSystem _saveLoadSystem;
        public OptionsMenuUI OptionsMenuUI { get; private set; }
        
        public OptionMenuSaveData SaveData => _saveData;

        public OptionMenuDataService(SaveLoadSystem saveLoadSystem, OptionsMenuUI optionsMenuUI)
        {
            _saveLoadSystem = saveLoadSystem;
            OptionsMenuUI = optionsMenuUI;
        }

        public async void Initialize()
        {
            await LoadOptions();
            OptionsMenuUI.Initalize(this);
        }

        private async Task LoadOptions()
        {
            Debug.Log("Loading Options");
            if (_saveLoadSystem.HasOptions())
            {
                var data = await _saveLoadSystem.LoadOptions();
                _saveData = JsonConvert.DeserializeObject<OptionMenuSaveData>(data);
            }
            else
            {
                _saveData = new OptionMenuSaveData();
                SaveOptions();
            }
            Debug.Log("Options loaded");
        }

        public async void SaveOptions()
        {
            var data = JsonConvert.SerializeObject(_saveData);
            await _saveLoadSystem.SaveOptions(data);
        }
    }
}
