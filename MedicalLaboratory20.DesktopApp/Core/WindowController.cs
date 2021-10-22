using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp.Core
{
    public class WindowController
    {
        private readonly Dictionary<Type, Type> _vmToWindowMap = new();
        private readonly Dictionary<object, Window> _openedWindows = new();

        public void RegisterVmAndWindow<TVm, TWin>()
            where TVm : class
            where TWin : Window
        {
            var typeVm = typeof(TVm);
            if (typeVm.IsInterface || typeVm.IsAbstract)
                throw new ArgumentException(nameof(typeVm) + " can't to register");

            if (_vmToWindowMap.ContainsKey(typeVm))
                throw new InvalidOperationException(nameof(typeVm) + " is registered");

            _vmToWindowMap[typeVm] = typeof(TWin);
        }

        private Window CreateWindowWithVm(object vm)
        {
            if (!_vmToWindowMap.TryGetValue(vm.GetType(), out Type? windType))
                throw new InvalidOperationException($"For {nameof(vm)} not found window");

            if (windType is null)
                throw new ArgumentNullException(nameof(windType));

            var wind = (Window)Activator.CreateInstance(windType);
            wind.DataContext = vm;
            return wind;
        }

        public void ShowWindow(object vm)
        {
            if (vm is null)
                throw new ArgumentNullException(nameof(vm));

            if (_openedWindows.ContainsKey(vm))
                throw new InvalidOperationException($"UI for {nameof(vm)} is displayed");

            var window = CreateWindowWithVm(vm);
            _openedWindows[vm] = window;
            window.Show();
        }

        public void HideWindow(object vm)
        {
            if (!_openedWindows.TryGetValue(vm, out Window? wind))
                throw new InvalidOperationException($"UI for this {nameof(vm)} is not displlayed");

            wind.Close();
            _openedWindows.Remove(vm);
        }

        public async Task ShowModal(object vm)
        {
            var window = CreateWindowWithVm(vm);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            await window.Dispatcher.InvokeAsync(() => window.ShowDialog());
        }
    }
}
