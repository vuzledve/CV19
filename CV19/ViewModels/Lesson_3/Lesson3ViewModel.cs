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

namespace CV19.ViewModels.Lesson_3
{
    internal class Lesson3ViewModel : ViewModel
    {
        public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("c:\\");



        #region SelectedDirectory : DirectoryViewModel - Выбранная директория

        /// <summary>Выбранная директория</summary>
        private DirectoryViewModel _SelectedDirectory;

        /// <summary>Выбранная директория</summary>
        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }

        #endregion


        public Lesson3ViewModel()
        {

        }

    }
}
