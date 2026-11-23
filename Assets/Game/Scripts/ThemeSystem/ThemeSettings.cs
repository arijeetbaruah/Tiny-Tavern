using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.ThemeSystem
{
    [CreateAssetMenu(fileName = "ThemeSettings", menuName = "Game/Theme/ThemeSettings")]
    public class ThemeSettings : ScriptableObject
    {
        public Color TextColor => _textColor;
        
        [ColorPalette, SerializeField]
        private Color _textColor;
    }
}
