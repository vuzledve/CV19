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

namespace CV19.ViewModels.Lesson_2
{
    internal class Lesson2ViewModel : ViewModel
    {
        #region Свойства
        public ObservableCollection<Group> Groups { get; }

        public object[] CompositeCollection { get; }



        #region SelectedCompositeValue : object - Выбранный непонятный элемент

        /// <summary>Выбранный непонятный элемент</summary>
        private object _SelectedCompositeValue;

        /// <summary>Выбранный непонятный элемент</summary>
        public object SelectedCompositeValue
        {
            get => _SelectedCompositeValue;
            set => Set(ref _SelectedCompositeValue, value);
        }

        #endregion


        #region SelectedGroup : Group - Выбранная группа

        /// <summary>Выбранная группа</summary>
        private Group _SelectedGroup;

        /// <summary>Выбранная группа</summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                if(!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                //_SelectedGroupStudents.View.Refresh();
                OnProrertyChanged(nameof(SelectedGroupStudents));
            }
        }

        #endregion



        #region StudentFilterText : string - текст фильтра студентов

        /// <summary>текст фильтра студентов</summary>
        private string _StudentFilterText;

        /// <summary>текст фильтра студентов</summary>
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }

        #endregion



        #region SelectedGroupStudents
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        private void OnStudentFiltred(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }

            var filter_text = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter_text))
                return;

            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View; 
        #endregion

        #endregion

        #region Команды

        #region CreateGroupCommand
        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecuted(object p)
        {
            int group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }
        #endregion

        #region DeleteGroupCommand
        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            int group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
                SelectedGroup = Groups[group_index];
        }
        #endregion


        #endregion

        #region Коллекция студентов для теста виртуализации
        public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
           .Select(i => new Student
           {
               Name = $"Имя {i}",
               Surname = $"Фамилия {i}"
           });
        #endregion

        

        public Lesson2ViewModel()
        {
            DeleteGroupCommand = new ActionCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);
            CreateGroupCommand = new ActionCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);

            int student_index = 1;
            var stusents = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = System.DateTime.Now,
                Rating = 0
            });

            //var stusents = Enumerable.Range(1, 10).Select(i => new Student
            //{
            //    Name = $"Name {i}",
            //    Surname = $"Surname {i}",
            //    Patronymic = $"Patronymic {i}",
            //    Birthday = System.DateTime.Now,
            //    Rating = 0
            //});
            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(stusents)
            }) ;
            Groups = new ObservableCollection<Group>(groups);


            #region CompositeCollection
            var data_list = new List<object>();

            data_list.Add("hello world");
            data_list.Add(43);
            data_list.Add(Groups[1]);
            data_list.Add(Groups[1].Students[0]);

            CompositeCollection = data_list.ToArray();
            #endregion

            _SelectedGroupStudents.Filter += OnStudentFiltred;

            //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        }

        
    }
}
