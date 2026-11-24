using Game.OptionsMenu;
using Game.PanelSystem;
using UnityEngine;
using Zenject;

namespace Game.MainMenu
{
    public class MainMenuHandler : IInitializable
    {
        private MainMenuPanel _mainMenuPanelInstance;
        private PanelManager _panelManager;
        private OptionsMenuUI _optionsMenuUI;
        
        public MainMenuHandler(MainMenuPanel mainMenuPanel, OptionsMenuUI optionsMenuUI, PanelManager panelManager)
        {
            _mainMenuPanelInstance =  mainMenuPanel;
            _panelManager = panelManager;
            _optionsMenuUI = optionsMenuUI;
        }
        
        public void Initialize()
        {
            _mainMenuPanelInstance.SetHandlers(this);
        }

        public void OnPlayPressed()
        {
            Debug.Log("Play");
        }

        public void OnOptionsPressed()
        {
            _panelManager.OpenPanel(_optionsMenuUI);
        }
        
        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}
