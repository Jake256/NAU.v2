﻿namespace NAUCountryIdeaHub.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool IsRequestAdmin { get; set; }
        public bool IsITAdmin { get; set; }
        public bool RecieveEmailNotifications { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
