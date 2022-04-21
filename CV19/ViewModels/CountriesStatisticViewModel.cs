using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using CV19.ViewModels;
using CV19.Models.Decanat;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Data;
using System.ComponentModel;
using System;
using CV19.Services;
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;*/



namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel MainModel { get; }



        #region Countries : IEnumerable<CountryInfo> - Статистика по странам

        /// <summary>Статистика по странам</summary>
        private IEnumerable<CountryInfo> _Countries;

        /// <summary>Статистика по странам</summary>
        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries;
            private set => Set(ref _Countries, value);
        }

        #endregion




        #region Commands

        #region RefreshDataCommand
        public ICommand RefreshDataCommand { get; }

        private bool CanRefreshDataCommandExecute(object p) => true;

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData();
        }
        #endregion 

        #endregion

       


        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;
            _DataService = new DataService();

            #region Commands

            RefreshDataCommand = new ActionCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute); 
           
            #endregion
        }
    }
}
