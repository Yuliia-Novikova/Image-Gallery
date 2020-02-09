using Image_Gallery.Model;
using Image_Gallery.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Image_Gallery.ViewModel
{
    class EntranceViewModel: ViewModelBase, IDisposable
    {

        private GalleryContext context;
        private List<User> _users;
        public EntranceViewModel()
        {
            Load();
        }
        public void Dispose()
        {
            if (this == null)
                context?.Dispose();
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string Password { get; set; }

        #region Commands       
        private DelegateCommand _registrationCommand;
        public DelegateCommand RegistrationButtonClick
        {
            get
            {
                return _registrationCommand ??
                    (_registrationCommand = new DelegateCommand(obj =>
                    {
                        Registration registration = new Registration();
                        Application.Current.Windows[0].Close();
                        registration.ShowDialog();
                    }));
            }
        }

        private DelegateCommand _okCommand;
        public DelegateCommand OkButtonClick
        {
            get
            {
                return _okCommand ??
                     (_okCommand = new DelegateCommand(obj =>
                     {
                         if (_users != null)
                         {
                             Password = (obj as PasswordBox).Password;
                             bool isRegistrated = false;
                             foreach (var u in _users)
                             {
                                 isRegistrated = (u.Login == Login && u.Password == Password) ? true : false;
                                 if (isRegistrated == true)
                                 {
                                     Gallery gallery = new Gallery(u);
                                     Application.Current.Windows[0].Close();
                                     gallery.ShowDialog();
                                     return;
                                 }
                             }
                             if (isRegistrated == false)
                                 MessageBox.Show("Wrong login or password. Please try again!", "Error", 
                                     MessageBoxButton.OK, MessageBoxImage.Error);
                         }
                     },
                     CanLogIn));
            }
        }

        private bool CanLogIn(object obj)
        {
            if(obj != null)
                Password = (obj as PasswordBox).Password;
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
                return false;
            else
                return true;
        }
        #endregion
        #region DataBase
        private void Load()
        {
            try
            {
                context = new GalleryContext();
                _users = context.Users.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }
}