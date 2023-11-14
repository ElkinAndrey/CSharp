using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using MVVM.DataAccess;

namespace MVVM.Model
{
    /// <summary>
    /// <param>Класс ProjectsEventArgs просто добавляет аргументы (в виде свойств) в обработчик события IProjectsModel.ProjectUpdated (в нашем примере будем передавать только ссылку на прототип IProject).</param>
    /// </summary>
    public class ProjectEventArgs : EventArgs
    {
        public IProject Project { get; set; }
        public ProjectEventArgs(IProject project)
        {
            Project = project;
        }
    }
}
