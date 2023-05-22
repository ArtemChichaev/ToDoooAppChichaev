using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDooo.UI.Models
{
    internal class SidebarItem
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }

        public SidebarItem(string title)
        {
            Title = title;
        }


        public ICommand Command { get; set; }
    }
}
