using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Exceptions
{
    public class DuplicateUserNameException : Exception
    {
        public string FirsName { get; }
        public string LastName { get; }
        public DuplicateUserNameException(string firsName, string lastName) 
            : base($"User with FirstName: {firsName} and LastName {lastName} already exists")
        {
            FirsName = firsName;
            LastName = lastName;
        }
    }
}
