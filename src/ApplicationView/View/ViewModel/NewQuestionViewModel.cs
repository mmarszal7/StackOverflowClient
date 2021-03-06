﻿namespace StackOverflowClient.ViewModel
{
    using StackOverflowClient.Common;
    using StackOverflowClient.Helpers;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    public class NewQuestionViewModel : BaseViewModel, IDataErrorInfo
    {
        public RelayCommand AddQuestion { get; set; }
        private IDataBaseRepository DataBaseRepository;

        #region Fields

        private string questionTitle = "";
        private string questionContent = "";
        private string questionTags = "";
        private string questionAuthor = "";

        static readonly string[] ValidatedProperties = { "QuestionTitle",
            "QuestionContent", "QuestionTags", "QuestionAuthor" };

        #endregion

        #region Properties

        public string QuestionTitle
        {
            get { return questionTitle; }
            set
            {
                if (questionTitle == value)
                    return;
                questionTitle = value;
                RaisePropertyChanged();
            }
        }

        public string QuestionContent
        {
            get { return questionContent; }
            set
            {
                if (questionContent == value)
                    return;
                questionContent = value;
                RaisePropertyChanged();
            }
        }

        public string QuestionTags
        {
            get { return questionTags; }
            set
            {
                if (questionTags == value)
                    return;
                questionTags = value;
                RaisePropertyChanged();
            }
        }

        public string QuestionAuthor
        {
            get { return questionAuthor; }
            set
            {
                if (questionAuthor == value)
                    return;
                questionAuthor = value;
                RaisePropertyChanged();
            }
        }


        public string Error => null;
        public string this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;
                return true;
            }
        }

        #endregion

        public NewQuestionViewModel(IDataBaseRepository dbRepository)
        {
            AddQuestion = new RelayCommand(param => this.AddNewQuestion((Window)param), (object parameter) => { return IsValid; });
            DataBaseRepository = dbRepository;
        }

        private void AddNewQuestion(Window window)
        {
            Random rand = new Random();
            Task task = new Task(() =>
            {
                BadgeCollection Badges = new BadgeCollection()
                {
                    GoldenBadges = rand.Next(0, 10),
                    SilverBadges = rand.Next(0, 50),
                    BronzeBadges = rand.Next(0, 200)
                };

                User Owner = new User()
                {
                    Name = questionAuthor,
                    Reputation = rand.Next(0, 10000),
                };
                Owner.BadgeCollection = Badges;

                Topic newTopic = new Topic()
                {
                    Title = questionTitle,
                    Content = questionContent,
                    StringTags = questionTags,
                    VoteCount = rand.Next(0, 100),
                    AnswerCount = rand.Next(0, 50),
                    ViewCount = rand.Next(0, 10000),
                    CreationDate = 0
                };
                newTopic.User = Owner;

                DataBaseRepository.AddNewTopic(newTopic);
            });

            task.Start();

            if (window != null)
                window.Close();
        }

        public string GetValidationError(string propertyName)
        {
            string result = null;

            switch (propertyName)
            {
                case "QuestionTitle":
                    if (QuestionTitle.Count() > 50 || QuestionTitle.Count() == 0)
                        result = "Title can't be longer that 50 characters";
                    break;

                case "QuestionContent":
                    if (QuestionContent.Count() > 500 || QuestionContent.Count() == 0)
                        result = "Content can't be longer that 500 characters";
                    break;

                case "QuestionTags":
                    foreach (var tag in QuestionTags.Split(' ').ToList())
                    {
                        if (tag.Count() > 10 || QuestionTags.Count() == 0)
                        {
                            result = "Tags must be separated with spaces and can't be longer that 10 characters";
                            break;
                        }
                    }
                    break;

                case "QuestionAuthor":
                    if (QuestionAuthor.Count() > 15 || !QuestionAuthor.All(char.IsLetterOrDigit) || QuestionAuthor.Count() == 0)
                        result = "Username can't be longer that 15 characters";
                    break;
            }

            return result;
        }
    }
}
