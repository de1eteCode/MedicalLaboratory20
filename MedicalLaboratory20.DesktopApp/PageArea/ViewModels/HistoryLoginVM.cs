using MedicalLaboratory20.DesktopApp.PageArea.Abstract;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MedicalLaboratory20.DesktopApp.Models;
using SharedModels;
using MedicalLaboratory20.DesktopApp.Services.ApiServices;

namespace MedicalLaboratory20.DesktopApp.PageArea.ViewModels
{
    internal class HistoryLoginVM : PageVMBase
    {
        private ObservableCollection<LogingAuth> _logingAuths;
        public HistoryLoginVM()
        {
            _selectedFilter = Filters.First();
        }

        public override async void OnLoad()
        {
            base.OnLoad();
            var clientRest = Client.GetInstance().RestClient;
            var response = await new AuthLoggerService(clientRest).GetDataLog();
            var data = JsonSerializer.Deserialize<IEnumerable<LogingAuth>>(response.Content);
            _logingAuths = new ObservableCollection<LogingAuth>(data);
        }

        public override string Title => "История входов";

        #region Filters

        private string _searchQuery = string.Empty;
        private Filter _selectedFilter;

        public ObservableCollection<Filter> Filters = new()
        {
            new Filter("Логин", "Login"),
            new Filter("Причина", "Description"),
            new Filter("Дата", "Date"),
            new Filter("Статус", "Result")
        };

        public Filter SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<LogingAuth> FilteredCollection => _logingAuths.Where(item => SelectedFilter.IsEqual(item, SearchQuery));

        #endregion
    }
}
