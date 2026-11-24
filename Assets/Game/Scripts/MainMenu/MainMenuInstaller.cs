using UnityEngine;
using Zenject;

namespace Game.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuPanel _mainMenuPanel;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuPanel>()
                .FromComponentInNewPrefab(_mainMenuPanel)
                .AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<MainMenuHandler>().AsSingle().NonLazy();
        }
    }
}