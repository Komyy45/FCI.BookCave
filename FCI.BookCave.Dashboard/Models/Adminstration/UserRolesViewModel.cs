﻿namespace FCI.BookCave.Dashboard.Models.Adminstration
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RolesCheckedViewModel> UserRoles { get; set; }
    }
}
