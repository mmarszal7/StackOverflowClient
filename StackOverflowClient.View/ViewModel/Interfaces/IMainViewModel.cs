using System.Collections.Generic;
using StackOverflowClient.Common;

namespace StackOverflowClient.View
{
    public interface IMainViewModel
    {
        RelayCommand AddTopic { get; set; }
        List<Topic> CachedTopics { get; set; }
        RelayCommand FirstPage { get; set; }
        bool[] IsActive { get; set; }
        RelayCommand LastPage { get; set; }
        RelayCommand NextPage { get; set; }
        int Page { get; set; }
        int[] Pagination { get; set; }
        RelayCommand PaginationSearch { get; set; }
        RelayCommand PaginationSearch0 { get; set; }
        RelayCommand PaginationSearch1 { get; set; }
        RelayCommand PaginationSearch2 { get; set; }
        RelayCommand PaginationSearch3 { get; set; }
        RelayCommand PaginationSearch4 { get; set; }
        bool[] PaginationVisability { get; set; }
        RelayCommand PreviousPage { get; set; }
        string Query { get; set; }
        RelayCommand Search { get; set; }
        string SelectedSortOrder { get; set; }
        string SortCriteria { get; set; }
        List<Topic> Topics { get; set; }
        int TotalNumberOfPages { get; set; }

        List<Topic> MakeDatabaseRequest();
        void SearchForTopics();
    }
}