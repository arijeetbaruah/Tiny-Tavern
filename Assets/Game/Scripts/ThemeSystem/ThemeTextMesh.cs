using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.ThemeSystem
{
    public class ThemeTextMesh : BaseThemeElement
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        public void SetText(string text)
        {
            _textMesh.SetText(text);
            ApplyTheme();
        }

        protected override void ApplyTheme()
        {
            _textMesh.color = _themeSettings.TextColor;
        }
    }
}
