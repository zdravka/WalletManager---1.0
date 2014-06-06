using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WalletManager.Models
{
    [Table("MOvementTypes")]
    public class MovementTypesModel
    {
        [Key]
        public int id { get; set; }

        [Column("directory")]
        [Display(Name = "Type")]
        [Required(ErrorMessage = "A movement type name is required.")]
        public string directory { get; set; }

        [Column("Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Type")]
        public int? sectionId { get; set; }

        [ForeignKey("sectionId")]
        public virtual FundsTypeModel movementType { get; set; }

        [Display(Name = "User")]
        [Column("userId")]
        public string userId { get; set; }
    }
}