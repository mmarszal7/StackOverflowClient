using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using StackOverflowClient.Common;
using NLog;

namespace StackOverflowClient.View
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private IRestRepository RestRepository;
        private IDataBaseRepository DataBaseRepository;

        private string selectedSortOrder = "desc";
        private string sortCriteria = "votes";
        private string query = "MVVM";
        private int page = 1;
        private int cacheLimit = 10;

        private int totalNumberOfPages = 10;
        private static readonly int topicsOnPage = 5;
        private static readonly int topicsPerRequestLimit = 100;
        private static readonly int cachedPagesPerLimit = (topicsPerRequestLimit / topicsOnPage);

        private bool[] isActive = new bool[5] { true, false, false, false, false };
        private int[] pagination = new int[5] { 1, 2, 3, 4, 5 };
        private bool[] paginationVisability = new bool[5] { false, false, false, false, false };

        private List<Topic> topics;
        private List<Topic> cachedTopics;
        private Dictionary<string, string> sortOrder = new Dictionary<string, string>
        {
            { "Ascending", "asc" },
            { "Descending", "desc" }
        };

        #endregion

        #region Properties

        public string SelectedSortOrder
        {
            get { return selectedSortOrder; }
            set
            {
                if (selectedSortOrder == sortOrder[value])
                    return;
                selectedSortOrder = sortOrder[value];
                Sort();
            }
        }

        public string SortCriteria
        {
            get { return sortCriteria; }
            set
            {
                if (sortCriteria == value)
                    return;
                sortCriteria = value;
                Sort();
            }
        }

        public string Query
        {
            get { return query; }
            set
            {
                if (query == value)
                    return;
                query = value;
            }
        }

        public int Page
        {
            get { return page; }
            set
            {
                if (page == value && page != 1)
                    return;
                page = value;
                ChangePage();
                SetPagination();
                ActivePageButton();
                RaisePropertyChanged();
            }
        }

        public int TotalNumberOfPages
        {
            get { return totalNumberOfPages; }
            set
            {
                if (totalNumberOfPages == value)
                    return;
                totalNumberOfPages = value;

                PaginationVisability = Enumerable.Repeat(true, 5).ToArray();
                if (totalNumberOfPages < 5)
                {
                    for (int i = 0; i < totalNumberOfPages; i++)
                        PaginationVisability[4 - i] = false;
                }
            }
        }

        #region Pagination Properties
        public bool[] IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive == value)
                    return;
                isActive = value;

            }
        }

        public int[] Pagination
        {
            get { return pagination; }
            set
            {
                if (pagination == value)
                    return;
                pagination = value;
            }
        }

        public bool[] PaginationVisability
        {
            get { return paginationVisability; }
            set
            {
                if (paginationVisability == value)
                    return;
                paginationVisability = value;
            }
        }

        #endregion

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

        public List<Topic> CachedTopics
        {
            get { return cachedTopics; }
            set
            {
                if (cachedTopics == value)
                    return;
                cachedTopics = value;
            }
        }

        #endregion

        #region Commands

        public RelayCommand FirstPage { get; set; }
        public RelayCommand LastPage { get; set; }
        public RelayCommand NextPage { get; set; }
        public RelayCommand PreviousPage { get; set; }
        public RelayCommand PaginationSearch { get; set; }

        public RelayCommand PaginationSearch0 { get; set; }
        public RelayCommand PaginationSearch1 { get; set; }
        public RelayCommand PaginationSearch2 { get; set; }
        public RelayCommand PaginationSearch3 { get; set; }
        public RelayCommand PaginationSearch4 { get; set; }

        public RelayCommand Search { get; set; }
        public RelayCommand AddTopic { get; set; }

        #endregion

        #region Public methods
        public MainViewModel(IDataBaseRepository dbRepository, IRestRepository restRepository)
        {
            #region Paging Commands

            FirstPage = new RelayCommand(() => { Page = 1; });
            LastPage = new RelayCommand(() => { Page = totalNumberOfPages; });
            PaginationSearch0 = new RelayCommand(() => { Page = Pagination[0]; });
            PaginationSearch1 = new RelayCommand(() => { Page = Pagination[1]; });
            PaginationSearch2 = new RelayCommand(() => { Page = Pagination[2]; });
            PaginationSearch3 = new RelayCommand(() => { Page = Pagination[3]; });
            PaginationSearch4 = new RelayCommand(() => { Page = Pagination[4]; });

            NextPage = new RelayCommand(() => { Page++; },
                (object parameter) => { return Page + 1 <= totalNumberOfPages; });

            PreviousPage = new RelayCommand(() => { Page--; },
                (object parameter) => { return Page - 1 > 0; });

            #endregion

            Search = new RelayCommand(SearchForTopics);
            AddTopic = new RelayCommand(AddNewTopic);

            DataBaseRepository = dbRepository;
            RestRepository = restRepository;
        }

        public async void SearchForTopics()
        {
            cacheLimit = (Page + cachedPagesPerLimit - 1);
            string parameters = $@"page={cacheLimit / cachedPagesPerLimit}&pagesize={topicsPerRequestLimit}&order={selectedSortOrder}&sort={sortCriteria.ToLower()}&intitle={Query}&site=stackoverflow&filter=";

            Task<Response> htmlTask = new Task<Response>(() => RestRepository.MakeHttpRequest(parameters));
            Task<List<Topic>> sqlTask = new Task<List<Topic>>(() => MakeDatabaseRequest());
            htmlTask.Start();
            sqlTask.Start();

            try
            {
               CachedTopics = (await htmlTask).TopicList;
            }
            catch (System.AggregateException)
            {
                Query = "Błąd 404";
                RaisePropertyChanged();
                return;
            }
            CachedTopics.AddRange(await sqlTask);

            TotalNumberOfPages = (await htmlTask).NumberOfTopics / topicsOnPage;
            Topics = cachedTopics.Count == topicsOnPage ? cachedTopics.GetRange(0, topicsOnPage) : null;
            Sort();
        }

        public List<Topic> MakeDatabaseRequest()
        {
            List<Topic> response = new List<Topic>();
            Random rand = new Random();

            response = DataBaseRepository.GetTopics(Query);

            return response;
        }

        #endregion

        #region Private methods

        private void ChangePage()
        {
            if (page > cacheLimit || page < cacheLimit - cachedPagesPerLimit)
                SearchForTopics();
            else
            {
                int topicsRange = ((Page - 1) % cachedPagesPerLimit) * topicsOnPage;
                Topics = cachedTopics.GetRange(topicsRange, topicsOnPage);
            }
        }

        private void AddNewTopic()
        {
            var vm = new NewQuestionViewModel(DataBaseRepository);
            var win = new NewQuestionWindow { DataContext = vm };
            vm.OnRequestClose += (s, e) => win.Close();
            win.Show();
        }

        private void Sort()
        {
            if (selectedSortOrder == "desc")
            {
                switch (sortCriteria)
                {
                    case "Activity":
                        cachedTopics = cachedTopics.OrderByDescending(p => p.ViewCount).ToList();
                        break;
                    case "Votes":
                        cachedTopics = cachedTopics.OrderByDescending(p => p.VoteCount).ToList();
                        break;
                    case "Creation":
                        cachedTopics = cachedTopics.OrderByDescending(p => p.CreationDate).ToList();
                        break;
                    case "Relevance":
                        cachedTopics = cachedTopics.OrderByDescending(p => p.AnswerCount).ToList();
                        break;
                }
            }
            else
            {
                switch (sortCriteria)
                {
                    case "Activity":
                        cachedTopics = cachedTopics.OrderBy(p => p.ViewCount).ToList();
                        break;
                    case "Votes":
                        cachedTopics = cachedTopics.OrderBy(p => p.VoteCount).ToList();
                        break;
                    case "Creation":
                        cachedTopics = cachedTopics.OrderBy(p => p.CreationDate).ToList();
                        break;
                    case "Relevance":
                        cachedTopics = cachedTopics.OrderBy(p => p.AnswerCount).ToList();
                        break;
                }
            }

            ChangePage();
            RaisePropertyChanged();
        }

        private void SetPagination()
        {
            if (page < 3)
            {
                Pagination = Enumerable.Range(1, 5).ToArray();
            }
            else if (page > totalNumberOfPages - 2)
            {
                Pagination = Enumerable.Range(totalNumberOfPages - 4, totalNumberOfPages).ToArray();
            }
            else
            {
                Pagination = Enumerable.Range(page - 2, page + 2).ToArray();
            }
        }

        private void ActivePageButton()
        {
            if (page < 3)
            {
                if (page == 1)
                    IsActive = new bool[5] { true, false, false, false, false };
                else
                    IsActive = new bool[5] { false, true, false, false, false };
            }
            else if (page > totalNumberOfPages - 2)
            {
                if (page == totalNumberOfPages)
                    IsActive = new bool[5] { false, false, false, false, true };
                else
                    IsActive = new bool[5] { false, false, false, true, false };
            }
            else
            {
                IsActive = new bool[5] { false, false, true, false, false };
            }
        }

        #endregion
    }
}
