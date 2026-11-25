using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.OptionsMenu
{
    public class GraphicsTab : MonoBehaviour, IOptionTab
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

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void InitializeFullscreenToggle()
        {
            _fullscreenToggle.isOn = Data.GraphicsTab.Fullscreen;
            _fullscreenToggle.onValueChanged.RemoveAllListeners();
            _fullscreenToggle.onValueChanged.AddListener(OnFullScreenUpdate);
        }

        private void OnFullScreenUpdate(bool val)
        {
            Screen.fullScreen = val;
            _optionMenuDataService.CloneData.GraphicsTab.Fullscreen = val;
            _optionMenuDataService.OnValueChanged.Invoke();
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
                if (resolution.width == Data.GraphicsTab.ResolutionWidth &&
                    resolution.height == Data.GraphicsTab.ResolutionHeight)
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
            _optionMenuDataService.CloneData.GraphicsTab.ResolutionWidth = resolution.width;
            _optionMenuDataService.CloneData.GraphicsTab.ResolutionHeight = resolution.height;
            
            _optionMenuDataService.OnValueChanged.Invoke();
        }
    }
}
