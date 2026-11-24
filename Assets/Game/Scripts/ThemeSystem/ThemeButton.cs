using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.ThemeSystem
{
    public class ThemeButton : BaseThemeElement
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Image _background;

        [SerializeField, ValueDropdown(nameof(GetButtonTypes))] private string _buttonType;
        
        public void SetText(string text)
        {
            _textMesh.SetText(text);
            ApplyTheme();
        }

        protected override void ApplyTheme()
        {
            var btnColors = _themeSettings.ButtonTypes.FirstOrDefault(btn => btn.Name == _buttonType);
            _textMesh.color = btnColors.Text;
            _background.color = btnColors.BG;
        }

        protected string[] GetButtonTypes()
        {
            #if UNITY_EDITOR
            var guids = UnityEditor.AssetDatabase.FindAssets($"t:{nameof(ThemeSettings)}");
            return guids.Select(guid =>
                UnityEditor.AssetDatabase.LoadAssetAtPath<ThemeSettings>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid)))
                .SelectMany(settings => settings.ButtonTypes).Select(buttonTypes => buttonTypes.Name).ToArray();
            #endif
            
            return new string[0];
        }
    }
}
