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

namespace CV19.ViewModels.Lesson_2
{
    internal class Lesson2ViewModel : ViewModel
    {
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
            set => Set(ref _SelectedGroup, value);
        }

        #endregion





        public Lesson2ViewModel()
        {

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
        }
    }
}
