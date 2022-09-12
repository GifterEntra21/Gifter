namespace Shared.Responses
{
    public class SingleResponse<T>
    {
        public SingleResponse(string message, bool hasSucces, T item, Exception exception)
        {
            Message = message;
            HasSucces = hasSucces;
            Item = item;
            Exception = exception;
        }

        public string Message { get; set; }
        public bool HasSucces { get; set; }
        public T Item { get; set; }
        public Exception Exception { get; set; }

    }
}