﻿using pindogramApp.Entities;

namespace pindogramApp.Dtos
{
    public class UserDto
    {
        public int? Id { get; set; } = null;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
        public bool IsActive { get; set; }
    }
}
