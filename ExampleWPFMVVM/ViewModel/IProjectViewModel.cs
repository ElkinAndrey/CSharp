using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleWPFMVVM.DataAccess;
using System.ComponentModel;

namespace ExampleWPFMVVM.ViewModel
{
    public interface IProjectViewModel : IProject
    {
        Status EstimateStatus { get; set; }
    }
}
