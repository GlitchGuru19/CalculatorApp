// Models/Calculator.cs
namespace CalculatorApp.Models  // Namespace matches folder structure
{
    //Simple class for Calculator 
    public class Calculator
    {
        // First number entered by the user
        public double Number1 { get; set; }

        // Second number entered by the user
        public double Number2 { get; set; }

        // Operation selected by the user (e.g., "add", "subtract")
        public string? Operation { get; set; }
    }
}