using System;
using System.ComponentModel.DataAnnotations;
 
namespace WeddingPlanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
//-----------------------------------------------------------------------------------
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters.")]
        [RegularExpression("^([a-zA-Z]+)$", ErrorMessage = "Must be letters only.")]
        public string BrideName {get; set;}
//-----------------------------------------------------------------------------------
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters.")]
        [RegularExpression("^([a-zA-Z]+)$", ErrorMessage = "Must be letters only.")]
        public string GroomName {get; set;}
//-----------------------------------------------------------------------------------
        [Required]
        [MyDate (ErrorMessage = "Date can not be in the past.")]
        [DataType(DataType.Date)]
        public string Date {get; set;}
//-----------------------------------------------------------------------------------
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'._-]+)+$", ErrorMessage = "Wrong address formating.")]
        public string Address {get; set;}
//-----------------------------------------------------------------------------------
        public class MyDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime theirDate = Convert.ToDateTime(value);
                return theirDate >= DateTime.Now;
            }
        }
//-----------------------------------------------------------------------------------
    }
}