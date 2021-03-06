﻿using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DA.Repos
{
    public class UserRepo
    {
        private FMIContext context;

        public UserRepo(FMIContext context)
        {
            this.context = context;
        }

        public void AddUser(User user, int userRole)
        {
            if (user != null)
            {
                var role = context.Roles.Where(u => u.RoleID == userRole).First();
                user.Roles.Add(role);
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User GetUserByEmail(string email)
        {
            var user = context
                .Users
                .Where(u => u.LoginEmail == email)
                .FirstOrDefault();
            return user;
        }

        public User GetUserWithEducation(int userID)
        {
            return context
                .Users
                .Include(u => u.EducationList)
                .Where(t => t.UserID == userID)
                .FirstOrDefault();
        }

        public User GetUserWithProfileInfo(int userId)
        {
            return context
                .Users
                .Include(u => u.EducationList)
                .Include(u => u.ProfilePicture)
                .Include(u => u.Publications)
                .Include(u => u.SoftwareDatasets)
                .Where(u => u.UserID == userId)
                .FirstOrDefault();
        }

        public User UpdateUserInformation(User user)
        {
            var userToUpdate = GetUserWithEducation(user.UserID);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Birthday = user.Birthday;
                userToUpdate.Title = user.Title;
                userToUpdate.Gender = user.Gender;
                userToUpdate.Nationality = user.Nationality;
                userToUpdate.Rank = user.Rank;
                userToUpdate.ContactEmail = user.ContactEmail;
                userToUpdate.CurrentInsitution = user.CurrentInsitution;
                userToUpdate.InterestAreas = user.InterestAreas;

                context.SaveChanges();
            }
            return userToUpdate;
        }

        public User GetUserById(int userID)
        {
            return context.Users.FirstOrDefault(u => u.UserID == userID);
        }

        public string AddProfilePicture(Image image, User user)
        {
            var addedImage = context.Images.Add(image);
            if (addedImage != null)
            {
                user.ProfilePicture = addedImage;
                return addedImage.FilePath;
            }

            return string.Empty;
        }

        public List<User> GetMembers()
        {
            var members = new List<User>();
            members = context.Users
                .Where(i => i.IsDeleted == false)
                .Include(u => u.EducationList)
                .OrderBy(u => u.LastName)
                .ToList();

            return members;
        }

        public List<User> GetFormerMembers()
        {
            var formers = new List<User>();

            formers = context.Users
                .Where(i => i.IsDeleted == true)
                .OrderBy(i => i.LastName)
                .ToList();

            return formers;
                
        }

        public List<Collaborator> GetCollaborators()
        {
            var collaborators = new List<Collaborator>();
            collaborators = context.Collaborators
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToList();

            return collaborators;
        }

        public void AddCollaborator(Collaborator collaborator)
        {
            context.Collaborators.Add(collaborator);
            context.SaveChanges();
        }

        public void DeleteCollaborator(int collaboratorId)
        {
            var collaborator = context.Collaborators
                .Where(i => i.CollaboratorID == collaboratorId)
                .FirstOrDefault();
            if (collaborator != null)
            {
                context.Collaborators.Remove(collaborator);
            }
            context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = context.Users
                .Include(i => i.Publications)
                .Include(i => i.EducationList)
                .Include(i => i.ProfilePicture)
                .Include(i => i.Awards)
                .Include(i => i.NewsList)
                .Include(i => i.SoftwareDatasets)
                .Include(i => i.Roles)
                .Where(i => i.UserID == userId)
                .FirstOrDefault();
            context.Users.Remove(user);
            context.SaveChanges();
        
        }

        public void UpdateToFormerOrMember(int userId, bool isDeleted)
        {
            var former = context.Users
                .Where(i => i.UserID == userId)
                .FirstOrDefault();
            if (former != null)
            {
                former.IsDeleted = isDeleted;
            }
            context.SaveChanges();
        }

        public bool IsEmailUnique(string email)
        {
            return context.Users
                .All(u => u.LoginEmail != email);
        }

        public List<Role> GetRoles()
        {
            return context.Roles
                .Select(r => r)
                .OrderBy(r => r.Title)
                .ToList();
        }
    }
}
