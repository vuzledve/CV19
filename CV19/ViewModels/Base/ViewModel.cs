using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //CallerMemberName Атрибут применяется к необязательному параметру, имеющему значение по умолчанию.
        //Необходимо указать явное значение по умолчанию для необязательного параметра. 
        //Этот атрибут нельзя применить к параметрам, которые не указаны как необязательные.
        protected virtual void OnProrertyChanged([CallerMemberName] string ProrertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ProrertyName));
        }

        //обновление значения свойства, для которого определено поле, в котором это свойство хранит свои данные
        //field - ссылка на поле свойства, value - новое значение, которое мы хотим установить
        //задача - разрешить кольцевые изменения свойств (св1 -> св2 -> св3 -> св1->...)
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string ProrertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;  
            OnProrertyChanged(ProrertyName);
            return true;
        }

        #region освобождение ресурсов
        //~ViewModel()
        //{
        //    Dispose(false);
        //}

        public void Dispose()
        {
            Dispose(true);
        }
        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
            //освобождение управляемых ресурсов
        }
        #endregion
    }
}
