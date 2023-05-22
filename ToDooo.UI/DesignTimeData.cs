using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDooo.CommonAPI;
using ToDooo.UI.Commands;
using ToDooo.UI.Models;

namespace ToDooo.UI
{
    internal class DesignTimeData
    {
        public ObservableCollection<ToDoItem> PlannedToDoItems { get; } = new ObservableCollection<ToDoItem>()
        {
            new ToDoItem()
            {
                Name = "Купить молоко",
            },
            new ToDoItem()
            {
                Name = "Погулять с собакой",
            },
            new ToDoItem()
            {
                Name = "Съесть",
            },
        };

        public ObservableCollection<ToDoItem> FinishedToDoItems { get; } = new ObservableCollection<ToDoItem>()
        {
            new ToDoItem()
            {
                Name = "Купить молоко",
                IsFinished = true
            },
            new ToDoItem()
            {
                Name = "Погулять с собакой",
                IsFinished = true
            },
        };

        public ObservableCollection<ToDoItem> DeletedToDoItems { get; } = new ObservableCollection<ToDoItem>()
        {
            new ToDoItem()
            {
                Name = "Слетать на луну",
                Deleted = true
            },    
        };

        public IEnumerable<SidebarItem> Actions { get; } = new SidebarItem[]
        {
            new SidebarItem("Запланированные")
            {
                IsActive = true,
                Command = new RelayCommand<string>((p)=>
                {
                })
            },
            new SidebarItem("Выполненные"),
            new SidebarItem("Архив"),
        };
    }
}
