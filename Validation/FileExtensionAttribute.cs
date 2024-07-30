using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebMVC2.Validation
{
    public class FileExtensionAttribute : ValidationAttribute, IClientModelValidator
    {
        private string[] start;
        public FileExtensionAttribute(string extensions)
        {
            start = extensions.Split(',');
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
                var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant().TrimStart('.');

                if (!start.Contains(extension))
                {
                    valid = false;
                    break;
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
            context.Attributes["data-val-fileextension"] = ErrorMessage ?? "請上傳正確的檔案格式";
        }
    }
}
