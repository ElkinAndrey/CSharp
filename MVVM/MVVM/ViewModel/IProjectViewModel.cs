using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.DataAccess;
using System.ComponentModel;

namespace MVVM.ViewModel
{
    public interface IProjectViewModel : IProject
    {
        Status EstimateStatus { get; set; }
    }
}
