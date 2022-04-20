using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using CV19.Models.Decanat;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Data;
using System.ComponentModel;
using System;
using System.IO;
using System.Diagnostics;

namespace CV19.ViewModels.Lesson_3
{
    internal class DirectoryViewModel : ViewModel
    {
       

        private readonly DirectoryInfo _DirectoryInfo; //Статический класс Directory предоставляет ряд методов для управления каталогами

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                       .EnumerateDirectories()
                       .Select(dir_info => new DirectoryViewModel(dir_info.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<DirectoryViewModel>();
            }
        }
       

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                               .EnumerateFiles()
                               .Select(file_info => new FileViewModel(file_info.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<FileViewModel>();
            }
        }
        
        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    var items =  SubDirectories
                        .Cast<object>() //Cast - используется для приведения каждого элемента входной последовательности в выходную последовательность указанного типа
                        .Concat(Files); //Concat - Объединяет один или несколько экземпляров класса
                    return items;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<object>();
            }
        }
       

        public string Name => _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string Path) => _DirectoryInfo= new DirectoryInfo(Path);



    }

    class FileViewModel : ViewModel
    {
        private FileInfo _FileInfo;

        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public DateTime CreationTime => _FileInfo.CreationTime;

        public FileViewModel(string Path) => _FileInfo = new FileInfo(Path);
        
    }
}
