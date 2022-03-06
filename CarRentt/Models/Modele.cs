using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentt.Models
{
    [Table("modeles")]
    public class Modele
    {
        public int ModeleID { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name =("Serie Voiture"))]
        public string SerieVoiture { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = ("Marque"))]
        public string MarqueVoiture { get; set; }
        [ForeignKey("Categorie")]
        public int CategorieID { get; set; }
        public virtual Categorie Categorie { get; set; }
    }
}