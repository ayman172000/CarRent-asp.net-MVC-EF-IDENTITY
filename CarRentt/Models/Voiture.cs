using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentt.Models
{
    [Table("voitures")]
    public class Voiture
    {
        public int VoitureID { get; set; }
        [Required,StringLength(30),Index(IsUnique =true)]
        public string Matricule { get; set; }
        [Required,Display(Name ="Date de mise en circulation")]
        public DateTime DateDeMiseEnCirculation { get; set; }
        [Required,Display(Name ="type de carburant")]
        public string TypeCarburant { get; set; }
        [Required,Range(0,double.MaxValue),Display(Name =("Prix de location journaliere"))]
        public double PrixJournaliere { get; set; }
        [Required,MaxLength(200)]
        public string Image { get; set; }
        [ForeignKey("Modele")]
        public int ModeleID { get; set; }
        public virtual Modele Modele { get; set; }
        public int IsAvailable { get; set; }

    }
}