// This is the code behind
using Microsoft.AspNetCore.Mvc;  // For IActionResult and Page()
using Microsoft.AspNetCore.Mvc.RazorPages;  // For PageModel base class
using CalculatorApp.Models;  // To use the Calculator class

namespace CalculatorApp.Pages
{
    public class CalculatorModel : PageModel  // Inherits from PageModel for Razor Pages functionality
    {
        [BindProperty]  // Binds form data to this property automatically on POST
        public Calculator Calculator { get; set; } = new Calculator();  // Holds user input

        public double? Result { get; set; }  // Stores the calculation result (nullable so it’s empty until calculated)

        public void OnGet()  // Runs when the page loads (GET request)
        {
            // Nothing needed here for now—just shows the empty form
        }

        public IActionResult OnPost()  // Runs when the form is submitted (POST request)
        {
            if (!ModelState.IsValid)  // Checks if the form data is valid (e.g., numbers are entered)
            {
                return Page();  // Reloads the page with errors if invalid
            }

            try  // Attempts the calculation
            {
                Result = CalculateResult();  // Calls the private method to compute the result
            }
            catch (DivideByZeroException)  // Catches division by zero
            {
                ModelState.AddModelError("", "Cannot divide by zero");  // Adds error message
                return Page();  // Reloads page with error
            }
            catch (ArgumentException ex)  // Catches invalid operation
            {
                ModelState.AddModelError("", ex.Message);  // Adds error message
                return Page();  // Reloads page with error
            }

            return Page();  // Reloads page with the result
        }

        private double CalculateResult()  // Does the actual math based on user input
        {
            switch (Calculator.Operation.ToLower())  // Checks the selected operation
            {
                case "add":
                    return Calculator.Number1 + Calculator.Number2;  // addition
                case "subtract":
                    return Calculator.Number1 - Calculator.Number2;  // subtraction
                case "multiply":
                    return Calculator.Number1 * Calculator.Number2;  // multiplication
                case "divide":
                    if (Calculator.Number2 == 0)  // Prevents division by zero
                    {
                        throw new DivideByZeroException();  // Throws error if second number is 0
                    }
                    return Calculator.Number1 / Calculator.Number2;  // Division
                default:
                    throw new ArgumentException("Invalid operation");  // Error for unknown operation
            }
        }
    }
}