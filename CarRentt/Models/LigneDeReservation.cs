using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentt.Models
{
    [Table("LigneDeReservations")]
    public class LigneDeReservation
    {
        [Key]
        [ForeignKey("Reservation"),Column(Order =0)]
        public int ReservationID { get; set; }
        public virtual Reservation Reservation { get; set; }
        [Key]
        [ForeignKey("Voiture"), Column(Order = 1)]
        public int VoitureID { get; set; }
        public virtual  Voiture  Voiture{ get; set; }
        [Required,Range(0,int.MaxValue)]
        public int Duree { get; set; }
        [Required,Range(0,double.MaxValue)]
        public double PrixTTC { get; set; }


    }
}