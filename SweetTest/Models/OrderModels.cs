using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SweetTest.Models
{
    public class OrderModels : DbContext
    {
        public OrderModels()
            : base("DefaultConnection")
        { }

        public DbSet<OrderItem> Orders { get; set; }
    }

    public class OrderItem {

        [Key]
        [Required]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Telephon")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.PhoneNumber)]
        public string UserTel { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.EmailAddress)]
        public string UserMail { get; set; }
        public int ItemsCount { get; set; }
    }
}