using System;
using Game.PanelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.MainMenu
{
    public class MainMenuPanel : BasePanel
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _quitButton;
        
        private MainMenuHandler _mainMenuHandler;

        public void SetHandlers(MainMenuHandler mainMenuHandler)
        {
            _mainMenuHandler = mainMenuHandler;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayPressed);
            _optionsButton.onClick.AddListener(OnOptionsPressed);
            _quitButton.onClick.AddListener(OnQuitPressed);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayPressed);
            _optionsButton.onClick.RemoveListener(OnOptionsPressed);
            _quitButton.onClick.RemoveListener(OnQuitPressed);
        }

        public void OnPlayPressed()
        {
            _mainMenuHandler.OnPlayPressed();
        }

        public void OnOptionsPressed()
        {
            _mainMenuHandler.OnOptionsPressed();
        }

        public void OnQuitPressed()
        {
            _mainMenuHandler.OnQuitPressed();
        }
    }
}
