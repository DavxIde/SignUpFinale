using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SignBootstrap.Models;

namespace SignBootstrap.Controllers
{
    public class PurchaseController : Controller
    {
        // Altri metodi del controller PurchaseController...

        [HttpPost]
        public IActionResult AddToCart(Purchase purchase)
        {
            List<Purchase> cartItems;

            if (HttpContext.Session.TryGetValue("Cart", out byte[] cartData))
            {
                // Se il carrello esiste già nella sessione, lo recuperiamo
                cartItems = System.Text.Json.JsonSerializer.Deserialize<List<Purchase>>(cartData);
            }
            else
            {
                // Se il carrello non esiste, creiamo una nuova lista vuota
                cartItems = new List<Purchase>();
            }

            var existingItem = cartItems.FirstOrDefault(item => item.NomeProdotto == purchase.NomeProdotto);

            if (existingItem != null)
            {
                existingItem.Quantita += purchase.Quantita;
            }
            else
            {
                cartItems.Add(purchase);
            }

            // Salva il carrello nella sessione
            HttpContext.Session.Set("Cart", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(cartItems));

            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            List<Purchase> cartItems;

            if (HttpContext.Session.TryGetValue("Cart", out byte[] cartData))
            {
                cartItems = System.Text.Json.JsonSerializer.Deserialize<List<Purchase>>(cartData);
            }
            else
            {
                cartItems = new List<Purchase>();
            }

            // Passa i dati del carrello alla vista
            return View(cartItems);
        }
    }
}
