namespace RecipeNest.Dto;

public class Paged<T>
{
    public int Start
    {
        get => start;
        set => start = value;
    }

    public int Limit
    {
        get => limit;
        set => limit = value;
    }

    public int Count
    {
        get => count;
        set => count = value;
    }

    public List<T> Items
    {
        get => items;
        set => items = value ?? throw new ArgumentNullException(nameof(value));
    }

    private int start;
    private int limit;
    private int count;
    private List<T> items;

    public Paged(int start, int limit, int count, List<T> items)
    {
        this.start = start;
        this.limit = limit;
        this.count = count;
        this.items = items;
    }
}