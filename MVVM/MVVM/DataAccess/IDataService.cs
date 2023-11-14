using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.DataAccess
{
    /// <summary>
    /// <param>Интерфейс базы данных</param>
    /// </summary>
    public interface IDataService
    {
        IList<Project> GetProjects();
    }
}
