using CarRentt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentt.Models
{
    [Table("reservations")]
    public class Reservation
    {
        public int ReservationID { get; set; }
        [Required, Display(Name = ("Date de reservation"))]
        public DateTime DateReservation { get; set; } = DateTime.Now;
        [ForeignKey("User"),Display(Name =("Reservé par"))]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public string Etat { get; set; }

        [Required,Range(0,double.MaxValue)]
        public double Total { get; set; }

    }
}