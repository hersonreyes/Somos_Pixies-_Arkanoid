using System;

namespace Arkanaoid_poo.Controlador
{
    public class OutOfBoundsException: Exception
    {
        public OutOfBoundsException(string message) : base(message)
        {
        }
    }
}