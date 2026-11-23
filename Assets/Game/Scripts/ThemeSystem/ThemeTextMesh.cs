using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.ThemeSystem
{
    public class ThemeTextMesh : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        private ThemeSettings _themeSettings;
        
        [Inject]
        public void Construct(ThemeSettings themeSettings)
        {
            _themeSettings = themeSettings;
        }

        private void Start()
        {
            ApplyTheme();
        }

        public void SetText(string text)
        {
            _textMesh.SetText(text);
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            _textMesh.color = _themeSettings.TextColor;
        }
    }
}
