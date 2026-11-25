using Game.InputSystem;
using Game.OptionsMenu;
using Game.PanelSystem;
using Game.SaveLoad;
using Game.ThemeSystem;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private OptionsMenuUI _optionsMenuUI;
        [SerializeField] private ThemeSettings _themeSettings;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputSettingService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveLoadSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ThemeSettings>().FromInstance(_themeSettings).AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<PanelManager>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<OptionsMenuUI>()
                .FromComponentInNewPrefab(_optionsMenuUI)
                .AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<OptionMenuDataService>()
                .AsSingle()
                .NonLazy();
        }
    }
}
