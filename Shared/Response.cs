namespace Shared
{
    public class Response<T>
    {
        public bool HasSucces { get; set; }
        public string Message { get; set; }
        public T Item { get; set; }

    }
}