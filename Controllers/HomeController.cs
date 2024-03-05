using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignBootstrap.Models;

namespace SignBootstrap.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string Nome_Utente, string Password)
    {
    bool isAuthenticated = false; // Imposta il valore di default
    if (Nome_Utente == "q" && Password == "q")
    {
        isAuthenticated = (HttpContext.Session.GetString("IsAuthenticated") == "true");
    }
    return View(isAuthenticated);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    

    public IActionResult SignUp()
    {
        return View( );
    }

    [HttpPost]
    public IActionResult Registra(SignUp s)
    {
        return View(s);
    }

    [HttpGet]
    public IActionResult Purchase()
    {
        return View( );
    }
    
    [HttpPost]
    public IActionResult Cart(Purchase C)
    {
        return View(C);
    }

    [HttpPost]
    public IActionResult AddToCart(Purchase purchase)
    {
        var purchaseController = new PurchaseController();
        return purchaseController.AddToCart(purchase);
    }
    
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(string loginUsername, string loginPassword)
    {
    // Verifica che le credenziali siano corrette
    if (loginUsername == "q" && loginPassword == "q")
    {
        // Imposta isAuthenticated a true nella sessione
        HttpContext.Session.SetString("IsAuthenticated", "true");
        return RedirectToAction("Index", "Home"); // Reindirizza alla homepage
    }
    else
    {
        // Se le credenziali non sono corrette, mostra un messaggio di errore o reindirizza a una pagina di login fallito
        ViewBag.ErrorMessage = "Credenziali non valide. Riprova.";
        return View();
    }
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
