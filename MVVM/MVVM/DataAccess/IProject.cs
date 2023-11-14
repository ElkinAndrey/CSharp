using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.DataAccess
{
    /// <summary>
    /// <param>Интерфейс ячейки одного проекта в базе данных. Содержит ID, имя ...</param>
    /// </summary>
    public interface IProject
    {
        int ID { get; set; }
        string Name { get; set; }
        double Estimate { get; set; }
        double Actual { get; set; }
        void Update(IProject project);
    }
}
