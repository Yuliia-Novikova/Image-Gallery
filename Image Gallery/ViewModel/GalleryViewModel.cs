using Image_Gallery.Model;
using Image_Gallery.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace Image_Gallery.ViewModel
{
    class GalleryViewModel : ViewModelBase, IDisposable
    {
        public GalleryViewModel(User user)
        {
            try
            {
                CurrentIndex = 0;
                context = new GalleryContext();
                currentUser = context.Users.Where(u => u.Login == user.Login).SingleOrDefault();
                List<Mark> currentUserMarks = context.Marks.Where(m => m.UserId == currentUser.Id).ToList();
                currentUser.Marks = new List<Mark>();
                foreach (var item in currentUserMarks)
                    currentUser.Marks.Add(item);
                Pictures = context.Pictures.ToList();
                Marks = context.Marks.ToList();
                TotalImgCount = Pictures.Count - 1;
                ChangePicture();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #region IDisposable
        public void Dispose()
        {
            if (this == null)
                context?.Dispose();
        }
        #endregion
        #region Properties
        private GalleryContext context;
        private List<Picture> Pictures { get; set; }
        private List<Mark> Marks { get; set; }

        private User currentUser;

        private Picture _currentPicture;
        public Picture CurrentPicture
        {
            get => _currentPicture;
            set
            {
                _currentPicture = value;
                OnPropertyChanged(nameof(CurrentPicture));
            }
        }
        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                ChangePicture();
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        private int _totalImgCount;
        public int TotalImgCount
        {
            get => _totalImgCount;
            set
            {
                _totalImgCount = value;
                OnPropertyChanged(nameof(TotalImgCount));
            }
        }

        private string _currentMark;
        public string CurrentMark
        {
            get => _currentMark;
            set
            {
                _currentMark = value;
                ChangeMark();
                OnPropertyChanged(nameof(CurrentMark));
            }
        }
        private string _pictureDescription;
        public string PictureDescription
        {
            get => _pictureDescription;
            set
            {
                _pictureDescription = value;
                OnPropertyChanged(nameof(PictureDescription));
            }
        }

        #endregion
        #region Commands       
        private DelegateCommand _firstCommand;
        public DelegateCommand FirstButtonClick
        {
            get
            {
                return _firstCommand ??
                    (_firstCommand = new DelegateCommand(obj =>
                    {
                        //var values = obj as object[];
                        //var slider = (values[0] as Slider);
                        CurrentIndex = 0;
                        ChangePicture();
                        //slider.Value = _currentIndex;
                        //if (pictures != null)
                        //{
                        //    CurrentPicture = pictures[_currentIndex];
                        //    SetMark(values[1]);
                        //}
                    }));
            }
        }
        private DelegateCommand _lastCommand;
        public DelegateCommand LastButtonClick
        {
            get
            {
                return _lastCommand ??
                    (_lastCommand = new DelegateCommand(obj =>
                    {
                        //var values = obj as object[];
                        //var slider = (values[0] as Slider);
                        CurrentIndex = Pictures.Count - 1;
                        ChangePicture();
                        //slider.Value = _currentIndex;
                        //if (pictures != null)
                        //{
                        //    CurrentPicture = pictures[_currentIndex];
                        //    SetMark(values[1]);
                        //}
                    }));
            }
        }
        private DelegateCommand _previousCommand;
        public DelegateCommand PreviousButtonClick
        {
            get
            {
                return _previousCommand ??
                    (_previousCommand = new DelegateCommand(obj =>
                    {
                        //var values = obj as object[];
                        //var slider = (values[0] as Slider);
                        if (CurrentIndex == 0)
                            CurrentIndex = Pictures.Count - 1;
                        else
                            CurrentIndex--;
                        ChangePicture();
                        //slider.Value = _currentIndex;
                        //if (pictures != null)
                        //{
                        //    CurrentPicture = pictures[_currentIndex];
                        //    SetMark(values[1]);
                        //}
                    }));
            }
        }
        private DelegateCommand _nextCommand;
        public DelegateCommand NextButtonClick
        {
            get
            {
                return _nextCommand ??
                    (_nextCommand = new DelegateCommand(obj =>
                    {
                        //var values = obj as object[];
                        //var slider = (values[0] as Slider);
                        if (CurrentIndex == Pictures.Count - 1)
                            CurrentIndex = 0;
                        else
                            CurrentIndex++;
                        ChangePicture();
                        //slider.Value = _currentIndex;
                        //if (pictures != null)
                        //{
                        //    CurrentPicture = pictures[_currentIndex];
                        //    SetMark(values[1]);
                        //}
                    }));
            }
        }
        private DelegateCommand _exitCommand;
        public DelegateCommand ExitButtonClick
        {
            get
            {
                return _exitCommand ??
                    (_exitCommand = new DelegateCommand(obj =>
                    {
                        Entrance entrance = new Entrance();
                        Application.Current.Windows[0].Close();
                        entrance.ShowDialog();
                    }));
            }
        }
        #endregion

        private void ChangePicture()
        {
            if(Pictures != null)
            {
                CurrentPicture = Pictures[CurrentIndex];
                List<Mark> currentPictureMarks = context.Marks.Where(m => m.PictureId == CurrentPicture.Id).ToList();
                CurrentPicture.Marks = new List<Mark>();
                foreach (var item in currentPictureMarks)
                    CurrentPicture.Marks.Add(item);
                    PictureDescription = CurrentPicture.ToString();

                Mark tempMark = Marks.Where(m => m.UserId == currentUser.Id && m.PictureId == CurrentPicture.Id).SingleOrDefault();
                if (tempMark != null)
                    CurrentMark = Marks.Where(m => m.UserId == currentUser.Id && m.PictureId == CurrentPicture.Id).SingleOrDefault().Value;
                else
                    CurrentMark = String.Empty;
            }
        }
        private void ChangeMark()
        {
            if (!String.IsNullOrEmpty(CurrentMark))
            {
                Mark newMark = Marks.Where(m => m.UserId == currentUser.Id && m.PictureId == CurrentPicture.Id).FirstOrDefault();
                if (newMark != null)
                    newMark.Value = CurrentMark;
                else
                {
                    newMark = new Mark { UserId = currentUser.Id, PictureId = CurrentPicture.Id, Value = CurrentMark, User = currentUser, Picture = CurrentPicture };
                    Marks.Add(newMark);
                    context.Marks.Add(newMark);
                }
                context.SaveChanges();
                PictureDescription = CurrentPicture.ToString();
            }
        }
    }
}
