using Image_Gallery.Model;
using Image_Gallery.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Image_Gallery.ViewModel
{
    class RegistrationViewModel: ViewModelBase, IDisposable
    {
        private GalleryContext context;
        private List<User> _users;
        public RegistrationViewModel()
        {
            Load();
        }

        public void Dispose()
        {
            if (this == null)
                context?.Dispose();
        }

        #region ModelProperies
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
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

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string PasswordConfirmation { get; set; }
        #endregion
        #region Commands   
        private DelegateCommand _okCommand;
        public DelegateCommand OkButtonClick
        {
            get
            {
                return _okCommand ??
                     (_okCommand = new DelegateCommand(obj =>
                     {
                         try
                         {
                             if (obj != null)
                             {
                                 var values = (obj as StackPanel).Children;
                                 Password = (values[4] as PasswordBox).Password;
                                 PasswordConfirmation = (values[6] as PasswordBox).Password;
                                 if (Password != PasswordConfirmation)
                                 {
                                     MessageBox.Show("Please make sure your passwords match!", "Error",
                                         MessageBoxButton.OK, MessageBoxImage.Warning);
                                     return;
                                 }
                             }
                             User newUser = new User()
                             {
                                 Id = _users.Count + 1,
                                 Login = Login.Trim(),
                                 Password = Login.Trim(),
                                 Name = Name.Trim(),
                                 Surname = Surname.Trim(),
                                 Marks = new List<Mark>()
                             };
                             if (_users.Count != 0)
                             {
                                 var selectedUser = _users.Where(u => u.Login == newUser.Login).SingleOrDefault();
                                 if (selectedUser != null)
                                 {
                                     MessageBox.Show("Login is already taken!", "Error",
                                         MessageBoxButton.OK, MessageBoxImage.Warning);
                                     Login = "";
                                     return;
                                 }
                             }
                             context.Users.Add(newUser);
                             context.SaveChanges(); //IMPORTANT
                             Gallery gallery = new Gallery(newUser);
                             Application.Current.Windows[0].Close();
                             gallery.ShowDialog();
                         }
                         catch (Exception e)
                         {
                             MessageBox.Show(e.Message);
                         }
                     },
                     CanRegister));
            }
        }

        private bool CanRegister(object obj)
        {
            if (obj != null)
            {
                var values = (obj as StackPanel).Children;
                Password = (values[4] as PasswordBox).Password;
                PasswordConfirmation = (values[6] as PasswordBox).Password;
                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password)
                    || string.IsNullOrEmpty(PasswordConfirmation) || string.IsNullOrEmpty(Name))
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelButtonClick
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new DelegateCommand(obj =>
                    {
                        Entrance entrance = new Entrance();
                        Application.Current.Windows[0].Close(); 
                        entrance.ShowDialog();
                    }));
            }
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
