using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils
{
    public static class ModelValidator
    {
        public static ValidationResult Validate(object obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            ValidationResult result = new ValidationResult();
            PropertyInfo[] props = obj.GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo prop = props[i];
                object? value = prop.GetValue(obj);

                RequiredAttribute? required = prop.GetCustomAttribute<RequiredAttribute>();
                if (required != null)
                {
                    if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
                    {
                        result.AddError($"{prop.Name}: {required.ErrorMessage}");
                    }
                }

                MaxLengthAttribute? maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLength != null)
                {
                    if (value is string text && text.Length > maxLength.Length)
                    {
                        result.AddError($"{prop.Name}: {maxLength.ErrorMessage}");
                    }
                }

                MinValueAttribute? minValue = prop.GetCustomAttribute<MinValueAttribute>();
                if (minValue != null && value != null)
                {
                    if (Convert.ToDouble(value) < minValue.Min)
                    {
                        result.AddError($"{prop.Name}: {minValue.ErrorMessage}");
                    }
                }
            }

            return result;
        }

        public static void PrintInfo(Type type)
        {
            Console.WriteLine($"Тип: {type.Name}");

            PropertyInfo[] props = type.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                Console.WriteLine($"  {props[i].Name} ({props[i].PropertyType.Name})");

                object[] attrs = props[i].GetCustomAttributes(true);
                for (int j = 0; j < attrs.Length; j++)
                {
                    Console.WriteLine($"    [{attrs[j].GetType().Name}]");
                }
            }
        }
    }
}