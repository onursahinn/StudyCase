namespace SharedKernel;

public class PaginatedResult<T>
{
    public List<T> Items { get; } = new List<T>();
    public int Limit { get; }
    public int Offset { get;}
    public long TotalCount { get; }
    public PaginatedResult(int limit, int offset, long totalCount, List<T> items)
    {
        Limit = limit;
        Offset = offset;
        TotalCount = totalCount;
        if (items.Count > 0)
        {
            Items.AddRange(items);
        }
    }
    
}
