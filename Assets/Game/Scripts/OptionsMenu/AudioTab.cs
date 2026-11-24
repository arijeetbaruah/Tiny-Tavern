using UnityEngine;

namespace Game.OptionsMenu
{
    public class AudioTab : MonoBehaviour, IOptionTab
    {
        private OptionMenuDataService _optionMenuDataService;

        private OptionMenuSaveData Data => _optionMenuDataService.SaveData;
        
        public void Initalize(OptionMenuDataService optionMenuDataService)
        {
            _optionMenuDataService = optionMenuDataService;
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
