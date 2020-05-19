using System;

namespace marketplaceservice.Exceptions
{
    [Serializable]
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException()
            : base("All fields have to be filled out.")
        {
        }
    }
}
