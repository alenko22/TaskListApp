using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class ToDo
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public ToDo(string name, DateTime date, string description)
        {
            this.Name = name;
            this.Date = date;
            this.Description = description;
        }
    }
}
