using System;
using System.Collections.Generic;
using System.Text;

namespace HeMaNe.Shared.TransferObjects
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int Group { get; set; }
    }
}
