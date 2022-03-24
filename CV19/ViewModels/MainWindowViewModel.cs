using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна

        private string _Title = "Анализ статистики CV19";

        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            //set 
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value;
            //    //OnProrertyChanged();

            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }
        #endregion

        #region Статус программы

        private string _Status = "готов!";

        /// <summary> Статус программы </summary>
        public string Status
        {
            get => _Status;         
            set => Set(ref _Status, value);
        }
        #endregion


    }
}
