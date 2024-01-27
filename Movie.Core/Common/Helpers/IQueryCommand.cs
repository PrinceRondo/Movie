namespace Movie.Core.Common.Helpers
{
    public interface IQueryCommand<out TResult>
    {
        TResult Execute();
    }
}