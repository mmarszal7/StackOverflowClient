using System.Collections.Generic;
using StackOverflowClient.Common;

namespace StackOverflowClient.View
{
    public interface IMainViewModel
    {
        List<Topic> Topics { get; set; }
        RelayCommand AddTopic { get; set; }
        RelayCommand Search { get; set; }
        RelayCommand PaginationCommand { get; set; }
        string[] Pagination { get; set; }
        string Query { get; set; }
        void SearchForTopics();
        
        Dictionary<string, string> RepositoryOption { get; }
        Dictionary<string, string> SortCriteria { get; }
        Dictionary<string, string> SortOrder { get; }
        string SelectedRepositoryOption { get; set; }
        string SelectedSortCriteria { get; set; }
        string SelectedSortOrder { get; set; }
    }
}