using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

using ToDooo.CommonAPI;

namespace ToDooo.UI
{
    internal class AppState
    {
        private ITodoApi _todoApi = new FakeApi();
        private event Action<ToDoItem, OperationType> ToDoItemsChanged;

        public ObservableCollection<ToDoItem> PlannedToDoItems { get; private set; }

        public ObservableCollection<ToDoItem> FinishedToDoItems { get; private set; }

        public ObservableCollection<ToDoItem> DeletedToDoItems { get; private set; }


        public AppState()
        {
            PlannedToDoItems = new(_todoApi.GetToDoItems(ToDoItemsType.Planned));
            FinishedToDoItems = new(_todoApi.GetToDoItems(ToDoItemsType.Finished));
            DeletedToDoItems = new(_todoApi.GetToDoItems(ToDoItemsType.Deleted));
            ToDoItemsChanged += AppState_ToDoItemsChanged;
        }

        private void AppState_ToDoItemsChanged(ToDoItem toDoItem, OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.Add:
                    PlannedToDoItems.Add(toDoItem);
                    break;
                case OperationType.Delete:
                    if (!toDoItem.IsFinished)
                    {
                        PlannedToDoItems.Remove(toDoItem);
                    }
                    else
                    {
                        FinishedToDoItems.Remove(toDoItem);
                    }
                    toDoItem.Deleted = true;
                    DeletedToDoItems.Add(toDoItem);
                    break;
                case OperationType.ChangeCompletition:
                    if (!toDoItem.IsFinished)
                    {
                        PlannedToDoItems.Remove(toDoItem);
                        FinishedToDoItems.Add(toDoItem);
                    }
                    else
                    {
                        FinishedToDoItems.Remove(toDoItem);
                        PlannedToDoItems.Add(toDoItem);
                    }
                    toDoItem.IsFinished = !toDoItem.IsFinished;
                    break;
            }
        }

        public void CreateTask(string name)
        {
            Response<int> result = _todoApi.CreateTask(name);
            if (result.Success)
            {
                var todoItem = new ToDoItem()
                {
                    Id = result.Data,
                    Name = name,
                    Deleted = false,
                };
                ToDoItemsChanged.Invoke(todoItem, OperationType.Add);
            }
        }

        public void DeleteTask(ToDoItem toDoItem)
        {
            Response result = _todoApi.DeleteTask(toDoItem.Id);
            if (result.Success)
            {
                ToDoItemsChanged.Invoke(toDoItem, OperationType.Delete);
            }
        }

        public void ChangeCompletition(ToDoItem toDoItem)
        {
            Response result = _todoApi.ChangeCompletition(toDoItem.Id);
            if (result.Success)
            {
                ToDoItemsChanged.Invoke(toDoItem, OperationType.ChangeCompletition);
            }
        }

        enum OperationType
        {
            Add,
            Delete,
            ChangeCompletition
        }
    }
}
