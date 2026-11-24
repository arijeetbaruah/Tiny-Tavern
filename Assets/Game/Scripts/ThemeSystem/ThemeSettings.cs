using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.ThemeSystem
{
    [CreateAssetMenu(fileName = "ThemeSettings", menuName = "Game/Theme/ThemeSettings")]
    public class ThemeSettings : ScriptableObject
    {
        public Color TextColor => _textColor;
        public IReadOnlyList<ButtonTypesDatum> ButtonTypes => _buttonTypes;
        
        [ColorPalette, SerializeField]
        private Color _textColor;
        [SerializeField]
        private List<ButtonTypesDatum> _buttonTypes;

        [System.Serializable]
        public struct ButtonTypesDatum
        {
            public string Name;
            [ColorPalette]
            public Color Text;
            [ColorPalette]
            public Color BG;
        }
    }
}
