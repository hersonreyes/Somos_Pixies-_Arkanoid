using System;

namespace Arkanaoid_poo.Controlador
{
    public class WrongKeyPressedException: Exception

    {
        public WrongKeyPressedException(string message) : base(message)
        {
        }
    }
}