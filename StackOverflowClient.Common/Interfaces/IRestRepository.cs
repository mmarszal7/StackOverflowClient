using StackOverflowClient.Common;

namespace StackOverflowClient.Common
{
    public interface IRestRepository
    {
        Response MakeHttpRequest(object parameter);
    }
}