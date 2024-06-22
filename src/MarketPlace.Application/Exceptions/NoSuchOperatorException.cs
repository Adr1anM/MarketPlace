using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Exceptions
{
    public class NoSuchOperatorException : Exception
    {
        public string Operator;

        public NoSuchOperatorException(string oper)
            : base($"Operator {oper} not found.")
        {
            Operator = oper;
        }
    }
}
