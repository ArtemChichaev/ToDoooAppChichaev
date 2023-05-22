namespace ToDooo.CommonAPI
{
    public interface ITodoApi
    {
        IEnumerable<ToDoItem> GetToDoItems(ToDoItemsType type);
        Response<int> CreateTask(string name);
        Response DeleteTask(int id);
        Response ChangeCompletition(int id);
    }
}
