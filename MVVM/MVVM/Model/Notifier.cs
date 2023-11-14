using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using MVVM.DataAccess;


namespace MVVM.Model
{
    /// <summary>
    /// <param>Класс Notifier реализует интерфейс INotifyPropertyChanged и будет использоваться в модели и модели-представлении для уведомлений об изменениях. Фактически он является оболочкой для инкапсуляции INotifyPropertyChanged</param>
    /// </summary>
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
