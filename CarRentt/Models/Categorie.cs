using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentt.Models
{
    [Table("categories")]
    public class Categorie
    {
        public int CategorieID { get; set; }
        [Required][StringLength(100)][Display(Name ="Categorie"),Index(IsUnique =true)]
        public string NomCategorie { get; set; }
        [Required]
        [StringLength(300)]
        [Display(Name = "Description")]
        public string DescriptionCategorie { get; set; }
    }
}