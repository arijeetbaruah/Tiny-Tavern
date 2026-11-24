using System;
using System.Collections.Generic;
using Game.InputSystem;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.PanelSystem
{
    /// <summary>
    /// Manages a stack of open panels and controls which of them are visible.
    /// </summary>
    /// <remarks>
    /// Panels are kept in open order, with the most recently opened panel on top.
    /// Visibility is recomputed after every change: all panels below the topmost
    /// non-stackable panel are hidden, and everything else is shown.
    /// The manager subscribes to the static <see cref="BasePanel.OnPanelClosed"/> event,
    /// so call <see cref="Dispose"/> when the owner is destroyed.
    /// </remarks>
    public class PanelManager : IDisposable, IInitializable
    {
        /// <summary>
        /// Open panels ordered from bottom (index 0) to top (last index).
        /// </summary>
        private readonly List<IPanel> _panels = new List<IPanel>();

        /// <summary>
        /// Gets the topmost open panel, or <c>null</c> if no panels are open.
        /// </summary>
        public IPanel Top => _panels.Count > 0 ? _panels[_panels.Count - 1] : null;

        private InputSettingService _inputSettingService;

        public PanelManager(InputSettingService inputSettingService)
        {
            _inputSettingService = inputSettingService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelManager"/> class
        /// and subscribes to panel close events.
        /// </summary>
        public void Initialize()
        {
            BasePanel.OnPanelClosed += HandlePanelClosed;
            _inputSettingService.InputSettings.PanelController.Back.performed += OnBackPressed;
            _inputSettingService.InputSettings.PanelController.Enable();
            _inputSettingService.InputSettings.PanelController.Back.Enable();
        }

        /// <summary>
        /// Unsubscribes from panel close events.
        /// </summary>
        /// <remarks>
        /// Required because the events are static and would otherwise keep this
        /// instance alive and invoke stale handlers.
        /// </remarks>
        public void Dispose()
        {
            _inputSettingService.InputSettings.PanelController.Back.Disable();
            _inputSettingService.InputSettings.PanelController.Disable();
            _inputSettingService.InputSettings.PanelController.Back.performed -= OnBackPressed;
            BasePanel.OnPanelClosed -= HandlePanelClosed;
        }

        private void OnBackPressed(InputAction.CallbackContext obj)
        {
            OnBackPressed();
        }

        /// <summary>
        /// Opens a panel and places it on top of the stack.
        /// </summary>
        /// <param name="panel">The panel to open. Ignored if <c>null</c>.</param>
        /// <remarks>
        /// If the panel is already open, it is moved to the top without being
        /// opened a second time.
        /// </remarks>
        public void OpenPanel(IPanel panel)
        {
            if (panel == null) return;

            bool alreadyOpen = _panels.Remove(panel);
            _panels.Add(panel);

            if (!alreadyOpen)
                panel.OpenPanel();

            RefreshVisibility();
        }

        /// <summary>
        /// Closes a panel and removes it from the stack.
        /// </summary>
        /// <param name="panel">The panel to close. Ignored if <c>null</c> or not open.</param>
        public void ClosePanel(IPanel panel)
        {
            if (panel == null || !_panels.Contains(panel)) return;

            panel.ClosePanel();

            // Normally the closed event already removed it; this covers panels that don't raise it.
            if (_panels.Remove(panel))
                RefreshVisibility();
        }

        /// <summary>
        /// Forwards a back input to the topmost panel.
        /// </summary>
        /// <returns>
        /// <c>true</c> if a panel received the back press; <c>false</c> if no panels are open,
        /// so the caller can handle it (for example by opening a pause menu).
        /// </returns>
        /// <remarks>
        /// The panel decides how to react, see <see cref="IPanel.OnBackPressed"/>.
        /// </remarks>
        public bool OnBackPressed()
        {
            var top = Top;
            if (top == null) return false;

            top.OnBackPressed();
            return true;
        }

        /// <summary>
        /// Removes a panel that closed itself and refreshes visibility.
        /// </summary>
        /// <param name="panel">The panel that was closed.</param>
        private void HandlePanelClosed(BasePanel panel)
        {
            if (_panels.Remove(panel))
                RefreshVisibility();
        }

        /// <summary>
        /// Shows or hides every panel based on the topmost non-stackable panel.
        /// </summary>
        /// <remarks>
        /// Panels below the topmost non-stackable panel are hidden; that panel and
        /// everything above it are visible. If no non-stackable panel exists,
        /// all panels are visible.
        /// </remarks>
        private void RefreshVisibility()
        {
            int firstVisible = 0;
            for (int i = _panels.Count - 1; i >= 0; i--)
            {
                if (!_panels[i].IsStackable)
                {
                    firstVisible = i;
                    break;
                }
            }

            for (int i = 0; i < _panels.Count; i++)
                _panels[i].SetVisible(i >= firstVisible);
        }
    }
}
