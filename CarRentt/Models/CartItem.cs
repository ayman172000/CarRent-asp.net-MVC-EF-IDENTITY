using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentt.Models
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public int Duree { get; set; }

        public System.DateTime DateCreated { get; set; } = System.DateTime.Now;

        public int Idvoiture { get; set; }

        public virtual Voiture Voiture { get; set; }
    }
}