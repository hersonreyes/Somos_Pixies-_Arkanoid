using System;

namespace Arkanaoid_poo.Controlador
{
    public class ExceededMaxCharactersException : Exception
    {
        public ExceededMaxCharactersException(string Message) : base(Message) { }
    }
}