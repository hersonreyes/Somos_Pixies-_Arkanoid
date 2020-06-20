﻿namespace Arkanaoid_poo.Modelo
{
    public class Player
    {
        public int idPlayer { get; set; }
        public string Nickname { get; set; }
        public int Score { get; set; }

        public Player(string nickname, int score)
        {
            Nickname = nickname;
            Score = score;
        }
    }
}