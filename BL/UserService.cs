using DA;
using BE;
using System.Collections.Generic;

namespace BL
{
    public class UserService
    {
        private Repositories repository;

        public UserService(Repositories repository)
        {
            this.repository = repository;
        }

        public void AddUser(User user, int userRole)
        {
            repository.UserRepo.AddUser(user, userRole);
        }

        public User GetUserByEmail(string email)
        {
            return repository.UserRepo.GetUserByEmail(email);
        }

        public User GetUserWithEducation(int userID)
        {
            return repository.UserRepo.GetUserWithEducation(userID);
        }

        public User UpdateUserInformation(User user)
        {
            return repository.UserRepo.UpdateUserInformation(user);
        }

        public User GetUserById(int userID)
        {
            return repository.UserRepo.GetUserById(userID);
        }
        public string AddProfilePicture(Image image, int userID)
        {
            var user = GetUserById(userID);
            if(user!=null)
            {
                return repository.UserRepo.AddProfilePicture(image, user);
            }
            return string.Empty;
        }

        public List<User> GetMembers()
        {
            return repository.UserRepo.GetMembers();
        }

        public List<Collaborator> GetCollaborators()
        {
            return repository.UserRepo.GetCollaborators();
        }
        public bool IsEmailUnique(string email)
        {
            return repository.UserRepo.IsEmailUnique(email);
        }

        public string GetFullName(int userID)
        {
            var user = GetUserById(userID);
            return user.FirstName + " " + user.LastName;
        }
        public List<Role> GetRoles()
        {
            return repository.UserRepo.GetRoles();
        }
    }
}
