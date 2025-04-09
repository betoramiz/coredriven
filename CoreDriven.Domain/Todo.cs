namespace CoreDriven.Domain;

public class Todo
{
    public string Id { get; set; }
    public string Name { get; set; }

    public Todo(string name)
    {
        Name = name;
        Id = Guid.NewGuid().ToString();
    }
}