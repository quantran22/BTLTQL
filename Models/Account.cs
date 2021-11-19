namespace LTQL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [StringLength(50)]
        [Requied(ErrorMessage = "User name is requied")]
        public string Username { get; set; }
        [Requied(ErrorMessage = "User name is requied")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(10)]
        public string RoleID { get; set; }


    }
}
