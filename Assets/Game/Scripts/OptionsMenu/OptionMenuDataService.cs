using System;
using System.Collections.Generic;
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
        private OptionMenuSaveData _cloneData;
        private SaveLoadSystem _saveLoadSystem;
        public OptionsMenuUI OptionsMenuUI { get; private set; }
        
        public OptionMenuSaveData SaveData => _saveData;
        public OptionMenuSaveData CloneData => _cloneData;
        
        private readonly Resolution[] _availableResolutions = null;
        private List<Resolution> _filteredResolutions = new();
        private int _currentResolutionIndex = 0;
        
        public IReadOnlyList<Resolution> Resolutions => _filteredResolutions;

        public delegate void OnValueChangedDelegate();
        public OnValueChangedDelegate OnValueChanged = delegate { };

        public OptionMenuDataService(SaveLoadSystem saveLoadSystem, OptionsMenuUI optionsMenuUI)
        {
            _saveLoadSystem = saveLoadSystem;
            OptionsMenuUI = optionsMenuUI;
            _availableResolutions = Screen.resolutions;
        }

        public async void Initialize()
        {
            await LoadOptions();
            
            UpdateResolutions();

            OptionsMenuUI.Initalize(this);
        }

        private void UpdateResolutions()
        {
            _filteredResolutions.Clear();
            List<string> options = new List<string>();
            for (int i = 0; i < _availableResolutions.Length; i++)
            {
                string option = _availableResolutions[i].width + " x " + _availableResolutions[i].height;
                if (!options.Contains(option))
                {
                    options.Add(option);
                    _filteredResolutions.Add(_availableResolutions[i]);
                }
            }
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
                //SaveOptions();
            }
            
            _cloneData = _saveData.Clone();
            Debug.Log("Options loaded");
        }

        public async void SaveOptions()
        {
            var data = JsonConvert.SerializeObject(_saveData);
            await _saveLoadSystem.SaveOptions(data);
        }

        public void Apply()
        {
            _saveData = _cloneData.Clone();
            SaveOptions();
        }

        public void Reset()
        {
            _cloneData = _saveData.Clone();
        }
    }
}
