using StackOverflowClient.Common;

namespace StackOverflowClient.View
{
    public interface INewQuestionViewModel
    {
        string this[string propertyName] { get; }

        RelayCommand AddQuestion { get; set; }
        string Error { get; }
        bool IsValid { get; }
        string QuestionAuthor { get; set; }
        string QuestionContent { get; set; }
        string QuestionTags { get; set; }
        string QuestionTitle { get; set; }

        string GetValidationError(string propertyName);
    }
}