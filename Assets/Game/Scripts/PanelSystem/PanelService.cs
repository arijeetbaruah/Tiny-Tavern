using UnityEngine;

namespace Game.PanelSystem
{
    public class PanelService
    {
        private PanelManager _manager;
        
        public PanelService(PanelManager manager)
        {
            _manager = manager;
        }
    }
}
