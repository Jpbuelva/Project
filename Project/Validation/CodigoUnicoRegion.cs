using Project.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Validation
{
    public class CodigoUnicoRegion: ValidationAttribute 
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = (ProjectContext)validationContext.GetService(typeof(ProjectContext));
            var result = _context.Region.SingleOrDefault(x => x.Codigo == (int)value);

            if (result != null)
                return new ValidationResult($"El código {(int)value} ya se encuentran registrado.");

            return ValidationResult.Success;
        }
    }
}
