using Game.Global.Config;
using Game.InputSystem;
using Game.OptionsMenu;
using Game.SaveLoad;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GlobalConfig _globalConfig;
        [SerializeField] private OptionsMenuUI _optionsMenuUI;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GlobalConfig>().FromInstance(_globalConfig).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputSettingService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveLoadSystem>().AsSingle().NonLazy();
            Container.BindFactory<OptionsMenuUI, OptionsMenuUI.OptionsMenuFactory>()
                .FromComponentInNewPrefab(_optionsMenuUI)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<OptionMenuDataService>()
                .AsSingle()
                .NonLazy();
        }
    }
}
