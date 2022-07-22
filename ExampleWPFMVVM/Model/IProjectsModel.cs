using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using ExampleWPFMVVM.DataAccess;

namespace ExampleWPFMVVM.Model
{
    public interface IProjectsModel
    {
        ObservableCollection<Project> Projects { get; set; }
        event EventHandler<ProjectEventArgs> ProjectUpdated;
        void UpdateProject(IProject updatedProject);
    }
}
