using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebMVC2.Validation
{
    public class DateAfterAttribute : ValidationAttribute, IClientModelValidator
    {
        private DateTime start;
        public DateAfterAttribute(string dateString, string format = "yyyy/MM/dd")
        {
            start = DateTime.ParseExact(dateString, format, null);
        }

        public override bool IsValid(object value)
        {
            var date = (DateTime)value;

            if (date.Ticks > start.Ticks)
            {
                return true;
            }
            return false;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            //方式一
            //MergeAttribute(context.Attributes, "data-val", "true");
            //MergeAttribute(context.Attributes, "data-val-publishdate", "your publishdate should after 2020/12/30(前端驗證)");

            //方式二
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-dateafter"] = "自訂驗證需在2020-01-01之前";
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
