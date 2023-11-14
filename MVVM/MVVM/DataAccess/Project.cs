using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.DataAccess
{
    /// <summary>
    /// <param>Ячейка одного проекта в базе данных. Содержит ID, имя ...</param>
    /// </summary>
    public class Project : IProject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Estimate { get; set; }
        public double Actual { get; set; }

        public void Update(IProject project)
        {
            Name = project.Name;
            Estimate = project.Estimate;
            Actual = project.Actual;
        }
    }
}
