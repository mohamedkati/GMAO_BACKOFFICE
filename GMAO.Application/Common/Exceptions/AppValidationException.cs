using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Exceptions
{
    public class AppValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public AppValidationException()
            : base("Une ou plusieurs erreurs de validation se sont produites.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public AppValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            //Errors = failures
            //    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            //    .ToDictionary(g => string.Concat((g.Key[0] + "").ToLower(), g.Key.Substring(1, g.Key.Length - 1)), g => g.ToArray());
            Errors = failures
              .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
              .ToDictionary(g => g.Key, g => g.ToArray());
        }
        public AppValidationException(Dictionary<string, string[]> erros)
           : this()
        {
            Errors = erros;
        }
    }
}
