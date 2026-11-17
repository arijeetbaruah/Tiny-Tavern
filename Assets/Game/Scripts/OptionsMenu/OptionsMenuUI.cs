using System;
using UnityEngine;
using Zenject;

namespace Game.OptionsMenu
{
    public class OptionsMenuUI : MonoBehaviour
    {
        private OptionMenuDataService _optionMenuDataService;
        
        [Inject]
        public void Construct(OptionMenuDataService optionMenuDataService)
        {
            _optionMenuDataService = optionMenuDataService;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public class OptionsMenuFactory : Factory<OptionsMenuUI>
        {
        }
    }
}
