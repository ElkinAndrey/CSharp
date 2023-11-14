using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using MVVM.DataAccess;

namespace MVVM.Model
{
    public interface IProjectsModel
    {
        ObservableCollection<Project> Projects { get; set; }
        event EventHandler<ProjectEventArgs> ProjectUpdated;
        void UpdateProject(IProject updatedProject);
    }
}
