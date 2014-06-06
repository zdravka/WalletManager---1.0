using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WalletManager.Models
{
    [Table("MovementOfFunds")]
    public class MovementOfFundsModel
    {
        [Key]
        public int Id { get; set; }

        //[Column("name")]
        //[Display(Name = "movement")]
        //[Required(ErrorMessage = "A movement name is required.")]
        //public string name { get; set; }

        [Column("RealDate")]
        [Display(Name = "date")]
        [DataType(DataType.Date)]
        public DateTime? RealDate { get; set; }

        [Column("EstimateDate")]
        [Display(Name = "Estimate Date")]
        [DataType(DataType.Date)]
        public DateTime? EstimateDate { get; set; }

        [Column("RealPrice")]
        [Display(Name = "Real Price")]
        public Decimal? RealPrice { get; set; }

        [Column("EstimatePrice")]
        [Display(Name = "Estimate Price")]
        public Decimal? EstimatePrice { get; set; }

        [Display(Name="Type")]
        public int? sectionId { get; set; }

        [ForeignKey("sectionId")]
        public virtual FundsTypeModel fundType { get; set; }

        [Display(Name = "User")]
        [Column("userId")]
        public string userId { get; set; }

        [Display(Name = "Movement")]
        public int movementTypeId { get; set; }

        [ForeignKey("movementTypeId")]
        public virtual MovementTypesModel movementType { get; set; }
        //[ForeignKey("userId")]
        //public virtual FundsTypeModel fundType { get; set; }
    }
}