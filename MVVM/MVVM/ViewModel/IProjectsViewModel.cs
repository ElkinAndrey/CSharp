using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.DataAccess;
using MVVM.Model;
using System.ComponentModel;

namespace MVVM.ViewModel
{
    public interface IProjectsViewModel : INotifyPropertyChanged
    {
        IProjectViewModel SelectedProject { get; set; }
        void UpdateProject();
    }
}
