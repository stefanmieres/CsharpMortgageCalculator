using CsharpMortgageCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsharpMortgageCalculator.helper
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {
            //calculate monthly payment
            loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term);
            //create a loop from 1 to the term
            var balance = loan.Amount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalcMonthlyRate(loan.Rate);
            //calculate a payment schedule
            for (int month = 1; month < 60; month++)
            {

                //Calcular monthly interest llamamndo a la funcion "CalcMonthlyInterest
                monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new LoanPayment();
                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.Monthlyinterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;
                //push paymnt into the loan
                loan.Payments.Add(loanPayment);
            }
            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

           

            //return the loan to the view
            return (loan);
        }
        //only LoanHelper class can access this class
        private decimal CalcPayment(decimal amount, decimal rate, int term ) 
        {
            
            // they need to be converted to doubles as the math libray in c# works in doubles and not in decimal
            //Use "var" rather than other data type as var equals whatever is at the other side of the "="
            var amountD = Convert.ToDouble(amount);

            //The parameter rate is assigned to call the fucntion "CalcMonthlyRate" to get the value monthly as the original value passed is annual
            rate = CalcMonthlyRate(rate);
            //or -->
            //var monthlyRate = CalcMonthlyRate(rate); 
            var rateD = Convert.ToDouble(rate);
            
            //everything must be changed to double 
            var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -term));
            
            //Converting to decimal as it was double             
            return Convert.ToDecimal(paymentD);  
        }

        private decimal CalcMonthlyRate(decimal rate) 
        {
            
            return rate / 1200;
        }

        private decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate) 
        {
            return (balance * monthlyRate);
        }
    }
}
