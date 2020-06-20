using System;
using System.Collections.Generic;
using System.Data;
using Arkanaoid_poo.Modelo;

namespace Arkanaoid_poo.Controlador
{
    public static class PlayerController
    {
        public static bool CreatePlayer(string nickname)
        {
            var dt = DBConnection.ExecuteQuery($"SELECT * FROM PLAYER WHERE nickname = '{nickname}'");

            if(dt.Rows.Count > 0)
                return true;
            else
            {
                DBConnection.ExecuteNonQuery("INSERT INTO PLAYER(nickname) VALUES" +
                                                   $"('{nickname}')");

                return false;
            }
        }

        public static void CreateNewScore(int idPlayer, int score)
        {
            DBConnection.ExecuteNonQuery("INSERT INTO SCORES(idPlayer, score) VALUES" +
                                               $"({idPlayer}, {score})");
        }

        public static List<Player> ObtainTopPlayers()
        {
            var topPlayers = new List<Player>();
            DataTable dt = DBConnection.ExecuteQuery("SELECT pl.nickname, sc.score " +
                                                           "FROM PLAYER pl, SCORES sc " +
                                                           "WHERE pl.idPlayer = sc.idPlayer " +
                                                           "ORDER BY sc.score DESC " +
                                                           "LIMIT 10");

            foreach(DataRow dr in dt.Rows)
            {
                topPlayers.Add(new Player(dr[0].ToString(), Convert.ToInt32(dr[1])));
            }

            return topPlayers;
        }
    }
}