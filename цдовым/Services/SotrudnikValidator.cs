using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using цдовым.Models;
using цдовым.Pages;

namespace WpfApp1.Validators
{
    public class EmployeeValidator
    {
        public List<ValidationResult> Validate(Sotrudniki employee)
        {
            var context = new ValidationContext(employee);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(employee, context, results, true);
            return results;
        }
    }
}