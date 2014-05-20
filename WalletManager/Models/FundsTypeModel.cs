using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WalletManager.Models
{
    [Table("section")]
    public class FundsTypeModel
    {
        [Key]
        public int id { get; set; }

        [Column("name")]
        [Display(Name="Fund type")]
        public string name { get; set; }
    }
}