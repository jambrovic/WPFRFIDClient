using MongoDB.Bson;
using RFIDClient.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RFIDClient.Desktop.Core
{
    public class UserViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The Name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Surname of the user
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The username of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        public SecureString Password { get; set; }

        #endregion

        #region Commands

        public ICommand CreateUserCommand { get; set; }

        #endregion

        #region Constructor

        public UserViewModel()
        {
            CreateUserCommand = new RelayParameterizedCommand(async (parameter) => await InsertAsync(parameter));
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Insert new user
        /// </summary>
        /// <param name="parameter">Instance of the object to insert</param>
        /// <returns></returns>
        public async Task InsertAsync(object parameter)
        {
            if (parameter == null)
                return;

            //Insert user
            try
            {
                var newUser = parameter as IUser;

                await UserRepositoryServiceFactory.Service.Insert(new UserService
                {
                    Email = newUser.Email,
                    Name = newUser.FirstName,
                    Surname = newUser.LastName,
                    Username = newUser.Username,
                    Password = newUser.Password,
                    Id = ObjectId.GenerateNewId().ToString()
                });

                //Reset new user form
                newUser.ResetForm();

                //Call new receipt page
                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Receipt);

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
