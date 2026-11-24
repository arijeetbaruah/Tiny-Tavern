using UnityEngine;
using Zenject;

namespace Game.ThemeSystem
{
    public abstract class BaseThemeElement : MonoBehaviour
    {
        protected ThemeSettings _themeSettings;
        
        [Inject]
        public virtual void Construct(ThemeSettings themeSettings)
        {
            _themeSettings = themeSettings;
        }
        
        protected virtual void Start()
        {
            ApplyTheme();
        }

        protected abstract void ApplyTheme();
    }
}
