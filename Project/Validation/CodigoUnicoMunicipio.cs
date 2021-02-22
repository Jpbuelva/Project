using Project.Models.Context;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Project.Validation
{
    public class CodigoUnicoMunicipio : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = (ProjectContext)validationContext.GetService(typeof(ProjectContext));
            var result = _context.Municipio.SingleOrDefault(x => x.Codigo == (int)value);

            if (result != null)
                return new ValidationResult($"El código {(int)value} ya se encuentran registrado.");

            return ValidationResult.Success;
        }
    }
}
