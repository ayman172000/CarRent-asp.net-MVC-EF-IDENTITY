using CarRentt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace CarRentt.Cart
{
    public class ShoppingCartActions
    {
        public string ShoppingCartId { get; set; }

        private ApplicationDbContext _db = new ApplicationDbContext();

        public const string CartSessionKey = "CartId";

        public void AddToCart(int id)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.Idvoiture == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    Idvoiture = id,
                    CartId = ShoppingCartId,
                    Voiture = _db.Voitures.SingleOrDefault(
                   p => p.VoitureID == id),
                    Duree = 1,
                    DateCreated = DateTime.Now
                };

                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Duree++;
            }
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null || 
                !string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    foreach (var item in _db.ShoppingCartItems.Where(d => d.CartId == ShoppingCartId).ToList())
                    {
                        CartItem cart = new CartItem();
                        cart = _db.ShoppingCartItems.Find(item.ItemId);
                        cart.CartId = HttpContext.Current.User.Identity.GetUserId();
                        _db.Entry(cart).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.GetUserId();
                }
                else
                {

                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _db.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }
        public decimal GetTotal()
        {
            ShoppingCartId = GetCartId();
            // Multiply product price by quantity of that product to get        
            // the current price for each of those products in the cart.  
            // Sum all product price totals to get the cart total.   
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _db.ShoppingCartItems
                               where cartItems.CartId == ShoppingCartId
                               select (int?)cartItems.Duree *
                               cartItems.Voiture.PrixJournaliere).Sum();
            return total ?? decimal.Zero;
        }
        public void setCartIdNull()
        {
            HttpContext.Current.Session[CartSessionKey] = null;
        }
    }
}