using System;
using System.Linq;
using Game.PanelSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.OptionsMenu
{
    public class OptionsMenuUI : BasePanel
    {
        [SerializeField] private GraphicsTab _graphicsTab;
        
        private OptionMenuDataService _optionMenuDataService;
        
        public void Initalize(OptionMenuDataService optionMenuDataService)
        {
            _optionMenuDataService = optionMenuDataService;

            _graphicsTab.Initalize(optionMenuDataService);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        
    }
}
