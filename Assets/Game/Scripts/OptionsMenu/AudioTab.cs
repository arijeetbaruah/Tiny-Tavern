using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.OptionsMenu
{
    public class AudioTab : MonoBehaviour, IOptionTab
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        
        private OptionMenuDataService _optionMenuDataService;

        private OptionMenuSaveData Data => _optionMenuDataService.SaveData;
        
        public void Initalize(OptionMenuDataService optionMenuDataService)
        {
            _optionMenuDataService = optionMenuDataService;
            
            InitializeVolume(_masterVolumeSlider, optionMenuDataService.SaveData.VolumeTab.MasterVolume, OnMasterChange);
            InitializeVolume(_musicVolumeSlider, optionMenuDataService.SaveData.VolumeTab.MusicVolume, OnMusicChange);
            InitializeVolume(_sfxVolumeSlider, optionMenuDataService.SaveData.VolumeTab.SFXVolume, OnSFXChange);
        }

        private static void InitializeVolume(Slider slider, float value, UnityAction<float> OnSliderValue)
        {
            slider.value = value;
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(OnSliderValue);
        }

        private void OnMasterChange(float value)
        {
            _optionMenuDataService.CloneData.VolumeTab.MasterVolume = value;
            _optionMenuDataService.OnValueChanged.Invoke();
        }
        
        private void OnMusicChange(float value)
        {
            _optionMenuDataService.CloneData.VolumeTab.MusicVolume = value;
            _optionMenuDataService.OnValueChanged.Invoke();
        }
        
        private void OnSFXChange(float value)
        {
            _optionMenuDataService.CloneData.VolumeTab.SFXVolume = value;
            _optionMenuDataService.OnValueChanged.Invoke();
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
