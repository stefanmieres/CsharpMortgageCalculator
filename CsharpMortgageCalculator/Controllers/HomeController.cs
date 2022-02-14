using CsharpMortgageCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CsharpMortgageCalculator.helper;

namespace CsharpMortgageCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult App()
        {
            Loan prestamo = new Loan();
            prestamo.Payment = 0.0m;
            prestamo.TotalInterest = 0.0m;
            prestamo.TotalCost = 0.0m;
            prestamo.Rate = 3.5m;
            prestamo.Amount = 150000m;
            prestamo.Term = 60;


            return View(prestamo);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult App(Loan loan) 
        {
            //calcualte the loan
            var loanHelper = new LoanHelper();

            //as thge majority of the class in LoanHelper are private they cannot be seen as property but loanHelper.Getpayment can.
            var newLoan = loanHelper.GetPayments(loan);

            return View(newLoan);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
