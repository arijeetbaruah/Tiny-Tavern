using System;
using UnityEngine;

namespace Game.PanelSystem
{
    /// <summary>
    /// Contract for any UI panel that can be managed by a <see cref="PanelManager"/>.
    /// </summary>
    public interface IPanel
    {
        /// <summary>
        /// Gets a value indicating whether the panel is currently visible.
        /// </summary>
        /// <remarks>
        /// This reflects visibility, not open state. A panel that is open but covered
        /// by a non-stackable panel reports <c>false</c>.
        /// </remarks>
        bool IsActive { get; }

        /// <summary>
        /// Gets a value indicating whether this panel layers on top of the panels beneath it.
        /// </summary>
        /// <remarks>
        /// Stackable panels (popups, confirm dialogs) leave the panels below them visible.
        /// Non-stackable panels (full-screen menus) hide everything below them while open.
        /// </remarks>
        bool IsStackable { get; }

        /// <summary>
        /// Opens the panel and makes it visible.
        /// </summary>
        void OpenPanel();

        /// <summary>
        /// Closes the panel and hides it.
        /// </summary>
        void ClosePanel();

        /// <summary>
        /// Called when the back input is pressed while this panel is the topmost panel.
        /// </summary>
        /// <remarks>
        /// Implementations decide how to react: close, ignore, or show a confirmation first.
        /// </remarks>
        void OnBackPressed();

        /// <summary>
        /// Shows or hides the panel without changing its open state.
        /// </summary>
        /// <param name="visible"><c>true</c> to show the panel; <c>false</c> to hide it.</param>
        /// <remarks>
        /// Used by <see cref="PanelManager"/> to hide panels covered by a non-stackable panel.
        /// Must be safe to call repeatedly with the same value.
        /// </remarks>
        void SetVisible(bool visible);
    }

    /// <summary>
    /// Base class for UI panels. Handles visibility and raises static open/close events.
    /// </summary>
    /// <remarks>
    /// Derive from this class and override the virtual methods to add animations,
    /// initialization, or custom back-button behavior.
    /// </remarks>
    public abstract class BasePanel : MonoBehaviour, IPanel
    {
        /// <summary>
        /// Raised after any panel has been opened.
        /// </summary>
        public static event Action<BasePanel> OnPanelOpened;

        /// <summary>
        /// Raised after any panel has been closed.
        /// </summary>
        /// <remarks>
        /// <see cref="PanelManager"/> subscribes to this to stay in sync when a panel
        /// closes itself without going through the manager.
        /// </remarks>
        public static event Action<BasePanel> OnPanelClosed;

        /// <summary>
        /// The root object that is toggled to show or hide the panel.
        /// </summary>
        [SerializeField] protected Transform _panelRoot;

        /// <summary>
        /// Whether this panel layers on top of the panels beneath it.
        /// </summary>
        [SerializeField] protected bool _isStackable;

        /// <inheritdoc/>
        public bool IsActive => _panelRoot.gameObject.activeSelf;

        /// <inheritdoc/>
        public bool IsStackable => _isStackable;

        /// <inheritdoc/>
        /// <remarks>
        /// Shows the panel, then raises <see cref="OnPanelOpened"/>.
        /// </remarks>
        public virtual void OpenPanel()
        {
            SetVisible(true);
            OnPanelOpened?.Invoke(this);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Hides the panel, then raises <see cref="OnPanelClosed"/>.
        /// </remarks>
        public virtual void ClosePanel()
        {
            SetVisible(false);
            OnPanelClosed?.Invoke(this);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// The default implementation closes the panel.
        /// </remarks>
        public virtual void OnBackPressed() => ClosePanel();

        /// <inheritdoc/>
        public virtual void SetVisible(bool visible) => _panelRoot.gameObject.SetActive(visible);
    }
}
