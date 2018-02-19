namespace StackOverflowClient.View
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using StackOverflowClient.Common;
    using NLog;
    using System;

    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        #region Fields, Properties and Commands

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private IRestRepository RestRepository;
        private IDataBaseRepository DataBaseRepository;
        private IDialogService<NewQuestionWindow> NewQuestionView;
        private List<Topic> topics;
        private List<Topic> CachedTopics;
        private int actualPage = 1;
        private int lastPage = 1;
        private static readonly int topicsOnPage = 5;

        public string Query { get; set; } = "MVVM";
        public string SelectedSortOrder { get; set; } = "desc";
        public string SelectedSortCriteria { get; set; } = "votes";
        public string SelectedRepositoryOption { get; set; } = "api";
        public List<string> Pagination { get; set; } = new List<string>() { "<<", "<", "1", "2", "3", "4", "5", ">", ">>" };
        public Dictionary<string, string> RepositoryOption { get; } = new Dictionary<string, string>
        {
            { "Stack Overflow", "api" },
            { "Local database", "db" }
        };
        public Dictionary<string, string> SortOrder { get; } = new Dictionary<string, string>
        {
            { "Ascending", "asc" },
            { "Descending", "desc" }
        };
        public Dictionary<string, string> SortCriteria { get; } = new Dictionary<string, string>
        {
            { "Votes", "votes" },
            { "Activity", "activity" },
            { "Creation", "creation" },
            { "Relevance", "relevance" }
        };
        public List<Topic> Topics
        {
            get { return topics; }
            set
            {
                if (topics == value)
                    return;
                topics = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand PaginationCommand { get; set; }
        public RelayCommand Search { get; set; }
        public RelayCommand AddTopic { get; set; }

        #endregion

        #region Public methods

        public MainViewModel(IDataBaseRepository dbRepository, IRestRepository restRepository, IDialogService<NewQuestionWindow> newQuestionView)
        {
            DataBaseRepository = dbRepository;
            RestRepository = restRepository;
            NewQuestionView = newQuestionView;

            PaginationCommand = new RelayCommand(param => ChangePage((string)param));
            Search = new RelayCommand(SearchForTopicsAsync);
            AddTopic = new RelayCommand(NewQuestionView.Show);
        }

        private async void SearchForTopicsAsync()
        {
            CachedTopics?.Clear();

            if (SelectedRepositoryOption == "api")
            {
                CachedTopics = await Task.Run(() => UseRestApi());
            }
            else
            {
                CachedTopics = await Task.Run(() => UseDataBase());
            }

            lastPage = (int)Math.Ceiling((double)CachedTopics.Count / topicsOnPage);
            Sort();
            ChangePage("<<");
        }

        private List<Topic> UseRestApi()
        {
            string parameters = $@"page={1}&pagesize={100}&order={SelectedSortOrder}&sort={SelectedSortCriteria}&intitle={Query}&site=stackoverflow&filter=";
            try
            {
                return RestRepository.MakeRequest(parameters).TopicList;
            }
            catch (System.AggregateException)
            {
                Query = "Błąd 404";
                RaisePropertyChanged();
                return new List<Topic>();
            }
        }

        private List<Topic> UseDataBase()
        {
            return DataBaseRepository.GetTopics(Query);
        }

        #endregion

        #region Private methods

        private void ChangePage(string option)
        {
            switch (option)
            {
                case ">>":
                    actualPage = lastPage;
                    break;
                case "<<":
                    actualPage = 1;
                    break;
                case ">":
                    if (actualPage < lastPage)
                        actualPage++;
                    break;
                case "<":
                    if (actualPage > 1)
                        actualPage--;
                    break;
                default:
                    if (int.Parse(option) > 0 && int.Parse(option) < lastPage)
                    {
                        actualPage = int.Parse(option);
                        List<string> temp;

                        if (actualPage <= 3)
                            temp = Enumerable.Range(1, 5).ToList().ConvertAll(n => n.ToString());
                        else if (lastPage - actualPage < 3)
                            temp = Enumerable.Range(lastPage - 4, 5).ToList().ConvertAll(n => n.ToString());
                        else
                            temp = Enumerable.Range(actualPage - 2, 5).ToList().ConvertAll(n => n.ToString());

                        Pagination = new List<string>() { "<<", "<", ">", ">>" };
                        Pagination.InsertRange(2, temp);
                    }
                    else return;
                    break;
            }

            if (CachedTopics.Count >= actualPage * topicsOnPage)
                Topics = CachedTopics.GetRange((actualPage - 1) * topicsOnPage, topicsOnPage);
            else
                Topics = CachedTopics.GetRange((actualPage - 1) * topicsOnPage, CachedTopics.Count % topicsOnPage);
        }

        private void Sort()
        {
            if (SelectedSortOrder == "desc")
            {
                switch (SelectedSortCriteria)
                {
                    case "activity":
                        CachedTopics = CachedTopics.OrderByDescending(p => p.ViewCount).ToList();
                        break;
                    case "votes":
                        CachedTopics = CachedTopics.OrderByDescending(p => p.VoteCount).ToList();
                        break;
                    case "creation":
                        CachedTopics = CachedTopics.OrderByDescending(p => p.CreationDate).ToList();
                        break;
                    case "relevance":
                        CachedTopics = CachedTopics.OrderByDescending(p => p.AnswerCount).ToList();
                        break;
                }
            }
            else
            {
                switch (SelectedSortOrder)
                {
                    case "activity":
                        CachedTopics = CachedTopics.OrderBy(p => p.ViewCount).ToList();
                        break;
                    case "votes":
                        CachedTopics = CachedTopics.OrderBy(p => p.VoteCount).ToList();
                        break;
                    case "creation":
                        CachedTopics = CachedTopics.OrderBy(p => p.CreationDate).ToList();
                        break;
                    case "relevance":
                        CachedTopics = CachedTopics.OrderBy(p => p.AnswerCount).ToList();
                        break;
                }
            }
        }

        #endregion
    }
}
