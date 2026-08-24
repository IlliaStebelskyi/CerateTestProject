public class SimpleStack<T>
{
    private readonly List<T> list = new();

    public void Push(T item)
    {
        list.Add(item);
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack is empty");

        var lastIndex = list.Count - 1;
        var item = list[lastIndex];
        list.RemoveAt(lastIndex);
        return item;
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Stack is empty");

        return list[list.Count - 1];
    }

    public bool IsEmpty()
    {
        return list.Count == 0;
    }
}