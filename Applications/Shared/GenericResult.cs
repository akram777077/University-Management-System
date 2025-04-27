namespace Applications.Shared
{
    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T value, string error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static new Result<T> Success(T value) => new Result<T>(true, value, null!);
        public static new Result<T> Failure(string error) => new Result<T>(false, default!, error);
    }
}
