using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleWPFMVVM.DataAccess;
using ExampleWPFMVVM.Model;
using System.ComponentModel;

namespace ExampleWPFMVVM.ViewModel
{
    public interface IProjectsViewModel : INotifyPropertyChanged
    {
        IProjectViewModel SelectedProject { get; set; }
        void UpdateProject();
    }
}
