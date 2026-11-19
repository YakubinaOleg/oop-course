using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils
{
    public static class FormBuilder
    {
        public static T Build<T>() where T : new()
        {
            T obj = new T();
            PropertyInfo[] props = typeof(T).GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo prop = props[i];

                // лише з публічним сеттером (Id і Status пропускаються)
                if (prop.GetSetMethod() == null)
                    continue;

                string label = BuildLabel(prop);

                while (true)
                {
                    Console.Write($"{label}: ");
                    string input = Console.ReadLine() ?? "";

                    try
                    {
                        object converted = Convert.ChangeType(input, prop.PropertyType);
                        prop.SetValue(obj, converted);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Некоректне значення, спробуйте ще раз.");
                    }
                }
            }

            return obj;
        }

        private static string BuildLabel(PropertyInfo prop)
        {
            string hint = "";

            if (prop.GetCustomAttribute<RequiredAttribute>() != null)
                hint = "обов'язкове";

            MaxLengthAttribute? max = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (max != null)
            {
                if (hint != "") hint += ", ";
                hint += $"макс. {max.Length}";
            }

            MinValueAttribute? min = prop.GetCustomAttribute<MinValueAttribute>();
            if (min != null)
            {
                if (hint != "") hint += ", ";
                hint += $"мін. {min.Min}";
            }

            return hint == "" ? prop.Name : $"{prop.Name} ({hint})";
        }
    }
}