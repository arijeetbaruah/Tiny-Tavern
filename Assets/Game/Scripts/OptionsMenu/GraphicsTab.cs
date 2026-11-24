using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.OptionsMenu
{
    public class GraphicsTab : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _resolutionDropdown;
        [SerializeField] private Toggle _fullscreenToggle;
        
        private OptionMenuDataService _optionMenuDataService;

        private OptionMenuSaveData Data => _optionMenuDataService.SaveData;
        
        public void Initalize(OptionMenuDataService optionMenuDataService)
        {
            _optionMenuDataService = optionMenuDataService;

            InitializeResolution();
            InitializeFullscreenToggle();
        }

        private void InitializeFullscreenToggle()
        {
            _fullscreenToggle.isOn = _optionMenuDataService.SaveData.GraphicsTab.Fullscreen;
        }
        
        private void InitializeResolution()
        {
            _resolutionDropdown.ClearOptions();
            _resolutionDropdown.AddOptions(_optionMenuDataService.Resolutions.Select(resolution => new TMP_Dropdown.OptionData($"{resolution.width} x {resolution.height}")).ToList());
            _resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);

            int selectedIndex = -1;
            int defaultIndex = 0;
            for (int index = 0; index < _resolutionDropdown.options.Count; index++)
            {
                var resolution = _optionMenuDataService.Resolutions[index];
                if (resolution.width == _optionMenuDataService.SaveData.GraphicsTab.ResolutionWidth &&
                    resolution.height == _optionMenuDataService.SaveData.GraphicsTab.ResolutionHeight)
                {
                    selectedIndex = index;
                    break;
                }
                
                if (resolution.width == Screen.width && resolution.height == Screen.height)
                {
                    defaultIndex = index;
                }
            }

            if (selectedIndex == -1)
            {
                selectedIndex = defaultIndex;
            }
            
            _resolutionDropdown.SetValueWithoutNotify(selectedIndex);
        }

        private void OnResolutionChange(int value)
        {
            var resolution = _optionMenuDataService.Resolutions[value];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        }
    }
}
