using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EagleTeck_Task.Shared.Extensions
{
    public static class ValidationHandleExtension
    {
        public static Dictionary<string,List<string>> GetErrorDictionary(this List<ValidationFailure> validationFailures)
        {
            Dictionary<string, List<string>> errors = [];

            validationFailures.ForEach(e =>
            {
                errors.TryGetValue(e.PropertyName, out var list);

                if (list != null)
                {
                    list.Add(e.ErrorMessage);
                }
                else
                {
                    errors.Add(e.PropertyName, [e.ErrorMessage]);
                }
            });

            return errors;
        }
    }
}
