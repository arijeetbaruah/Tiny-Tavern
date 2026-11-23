using System.Collections.Generic;
using UnityEngine;

namespace Game.PanelSystem
{
    [CreateAssetMenu(fileName = "ScenePanelConfig", menuName = "Game/Panels/Scene Panel Config")]
    public class ScenePanelConfig : ScriptableObject
    {
        public string SceneId => sceneId;
        public IReadOnlyList<PanelConfig> Panels => _panels;
        
        [SerializeField] private string sceneId;
        [SerializeField] private List<PanelConfig> _panels;
    }

    [System.Serializable]
    public class PanelConfig
    {
        [field:SerializeField]
        public BasePanel Panel { get; private set; }
        [field:SerializeField] 
        public bool IsOpen { get; private set; }
    }
}
