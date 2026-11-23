using System;
using Game.PanelSystem;
using UnityEngine;
using Zenject;

namespace Game.OptionsMenu
{
    public class OptionsMenuUI : BasePanel
    {
        private OptionMenuDataService _optionMenuDataService;

        private OptionMenuSaveData Data => _optionMenuDataService.SaveData;
        
        public void Initalize(OptionMenuDataService optionMenuDataService)
        {
            _optionMenuDataService = optionMenuDataService;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
