using System;

namespace Arkanaoid_poo.Controlador
{
    public class UnchargedBallException:Exception
    {
        public UnchargedBallException(string message) : base(message)
        {
        }
    }
}