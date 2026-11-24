using System;
using System.Collections.Generic;
using System.Linq;
using Game.PanelSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.OptionsMenu
{
    public interface IOptionTab
    {
        void Initalize(OptionMenuDataService optionMenuDataService);
        void Open();
        void Close();
    }
    
    public class OptionsMenuUI : BasePanel
    {
        [OdinSerialize, ShowInInspector]
        private List<OptionsMenuTabData> _tabs;
        
        private OptionsMenuTabData? _currentTab;
        
        public void Initalize(OptionMenuDataService optionMenuDataService)
        {
            foreach (var tab in _tabs)
            {
                var tabData = tab;
                
                tab.Tab.Initalize(optionMenuDataService);
                tab.Btn.onClick.AddListener(() => OpenTab(tabData));
            }
            
            OpenTab(_tabs[0]);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OpenTab(OptionsMenuTabData data)
        {
            if (_currentTab.HasValue)
            {
                if (_currentTab.Value.Tab == data.Tab)
                {
                    return;
                }
                else
                {
                    _currentTab.Value.Tab.Close();
                    _currentTab.Value.Btn.interactable = true;
                }
            }
            
            foreach (var tab in _tabs)
            {
                if (tab.Tab == data.Tab)
                {
                    data.Tab.Open();
                    data.Btn.interactable = false;
                }
            }
            _currentTab = data;
        }

        [System.Serializable]
        public struct OptionsMenuTabData
        {
            public Button Btn;
            [OdinSerialize, ShowInInspector]
            public IOptionTab Tab;
        }
    }
}
