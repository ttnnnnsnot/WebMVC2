using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebMVC2.Validation
{
    public class MaxFileCountAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxCount;

        public MaxFileCountAttribute(int maxCount)
        {
            _maxCount = maxCount;
        }

        public override bool IsValid(object? value)
        {
            var fileList = value as List<IFormFile>;
            if (fileList != null && fileList.Count > _maxCount)
            {
                return false;
            }
            return true;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-filemaxcount"] = ErrorMessage ?? "檔案數量超過限制";
        }
    }
}
