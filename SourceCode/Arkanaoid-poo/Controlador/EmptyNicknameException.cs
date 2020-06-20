using System;

namespace Arkanaoid_poo.Controlador
{
    public class EmptyNicknameException : Exception
    {
        public EmptyNicknameException(string Message) : base(Message) { }
    }
}