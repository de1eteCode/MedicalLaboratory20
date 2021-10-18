using System;
using System.Collections.Generic;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp.Core
{
    sealed class WindowManager
    {
        #region Singleton
        private static object _syncObj = new object();
        private static WindowManager _instance;
        public static WindowManager GetInstance()
        {
            if (_instance is null)
            {
                lock (_syncObj)
                {
                    if (_instance is null)
                    {
                        _instance = new WindowManager();
                    }
                }
            }
            return _instance;
        }
        #endregion

        private Dictionary<Type, Type> _vmToWindow = new();

        private WindowManager() { }

        public void RegisterWindow<VM, Win>()
            where VM : class
            where Win : class
        {
            var vmType = typeof(VM);

            if (vmType.IsInterface)
                throw new ArgumentException($"{nameof(vmType)} is interface");

            if (_vmToWindow.ContainsKey(vmType))
                throw new InvalidOperationException($"{nameof(vmType)} is registered");

            _vmToWindow[vmType] = typeof(Win);
        }

        private Window CreateWindowInstanceWithVM(object vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            var vmType = vm.GetType();
            _vmToWindow.TryGetValue(vmType, out Type windowType);

            if (windowType is null)
                throw new InvalidOperationException(nameof(windowType));

            var window = (Window)Activator.CreateInstance(windowType);
            window.DataContext = vm;
            return window;
        }

        private Dictionary<object, Window> _openedWindow = new();

        public void ShowWindow(object vm)
        {
            if (vm is null)
                throw new ArgumentNullException(nameof(vm));
            if (_openedWindow.ContainsKey(vm))
                throw new InvalidOperationException("UI for this VM is displayed");

            var window = CreateWindowInstanceWithVM(vm);
            window.Show();
            _openedWindow[vm] = window;
        }

        public void HideWindow(object vm)
        {
            if (_openedWindow.TryGetValue(vm, out Window win) is false)
                throw new InvalidOperationException("UI for this VM is not displayed");

            win.Close();
            _openedWindow.Remove(vm);
        }
    }
}
