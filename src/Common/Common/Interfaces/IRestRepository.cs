namespace StackOverflowClient.Common
{
    using StackOverflowClient.Common;

    public interface IRestRepository
    {
        Response MakeRequest(string parameter);
    }
}