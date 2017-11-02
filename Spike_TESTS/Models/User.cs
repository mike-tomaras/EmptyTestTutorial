using System;

namespace Spike_TESTS.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum UserStatus
    {
        Active,
        Inactive,
        Disabled
    }
}