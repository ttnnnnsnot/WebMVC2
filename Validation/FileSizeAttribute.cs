using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebMVC2.Validation
{
    public class FileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private long start;
        public FileSizeAttribute(int sizeMB)
        {
            start = 1024 * 1024 * sizeMB;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var formFiles = (List<IFormFile>)value;
            bool valid = true;
            
            foreach (var formFile in formFiles)
            {
                if (formFile.Length > start)
                {
                    valid = false;
                }
            }

            return valid;
        }


        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-filesize"] = ErrorMessage ?? "單檔案大小超過限制";
        }
    }
}
