using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.InputSystem
{
    public class InputSettingService : IInitializable, IDisposable
    {
        private InputSettings _inputSettings;

        public InputSettings InputSettings => _inputSettings;

        public delegate void OnDeviceChangeDelegate(InputDevice device);
        public event OnDeviceChangeDelegate DeviceAdded, DeviceRemoved, DeviceDisconnected, DeviceReconnected;

        public void Initialize()
        {
            _inputSettings = new InputSettings();

            UnityEngine.InputSystem.InputSystem.onDeviceChange += OnDeviceChange;
            
            DeviceAdded += OnInputDeviceAdded;
            DeviceRemoved += OnInputDeviceRemoved;
            DeviceDisconnected += OnInputDeviceDisconnected;
            DeviceReconnected += OnInputDeviceReconnected;
        }

        public void Dispose()
        {
            UnityEngine.InputSystem.InputSystem.onDeviceChange -= OnDeviceChange;
            
            DeviceAdded -= OnInputDeviceAdded;
            DeviceRemoved -= OnInputDeviceRemoved;
            DeviceDisconnected -= OnInputDeviceDisconnected;
            DeviceReconnected -= OnInputDeviceReconnected;
            
            _inputSettings.Disable();
            _inputSettings.Dispose();
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Added: DeviceAdded?.Invoke(device); break;
                case InputDeviceChange.Removed: DeviceRemoved?.Invoke(device); break;
                case InputDeviceChange.Disconnected: DeviceDisconnected?.Invoke(device); break;
                case InputDeviceChange.Reconnected: DeviceReconnected?.Invoke(device); break;
            }
        }

        private void OnInputDeviceAdded(InputDevice device)
        {
            Debug.Log($"On Device Added: {device}");
        }

        private void OnInputDeviceRemoved(InputDevice device)
        {
            Debug.Log($"On Device Removed: {device}");
        }

        private void OnInputDeviceDisconnected(InputDevice device)
        {
            Debug.Log($"On Device Disconnected: {device}");
        }

        private void OnInputDeviceReconnected(InputDevice device)
        {
            Debug.Log($"On Device Reconnected: {device}");
        }
    }
}
