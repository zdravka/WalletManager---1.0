using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WalletManager.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
    [Table("role")]
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
    [Table("UserRoles")]
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}