using StackOverflowClient.Common;

namespace StackOverflowClient.Common
{
    public interface IRestRepository
    {
        Response MakeRequest(string parameter);
    }
}