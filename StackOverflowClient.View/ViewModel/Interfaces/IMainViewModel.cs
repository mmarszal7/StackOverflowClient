﻿namespace StackOverflowClient.View
{
    using System.Collections.Generic;
    using StackOverflowClient.Common;

    public interface IMainViewModel
    {
        List<Topic> Topics { get; set; }
        RelayCommand AddTopic { get; set; }
        RelayCommand Search { get; set; }
        RelayCommand PaginationCommand { get; set; }
        List<string> Pagination { get; set; }
        string Query { get; set; }
        
        Dictionary<string, string> RepositoryOption { get; }
        Dictionary<string, string> SortCriteria { get; }
        Dictionary<string, string> SortOrder { get; }
        string SelectedRepositoryOption { get; set; }
        string SelectedSortCriteria { get; set; }
        string SelectedSortOrder { get; set; }
    }
}