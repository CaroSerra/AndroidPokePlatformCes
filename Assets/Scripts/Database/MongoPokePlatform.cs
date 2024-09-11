using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using MongoDB.Driver;
using MongoPokePlatform.Models;

// Usado para las conexiones con la base de datos de MongoDB
namespace MongoPokePlatform
{
    public static class Db
    {
        // Crear perfil de usuario
        public static void CreatePlayer(string name)
        {
            // Obtener datos de la base de datos
            var DDBB = GetDbData();
            var PlayersDB = DDBB.GetCollection<Players>("players");

            // Revisar si ya existe el jugador
            var ThisPlayer = PlayersDB.Find(x => x.Name == name).FirstOrDefault();
            if (ThisPlayer != null) return;

            var NewPlayer = new Players()
            {
                Name = name,
                Score = 1
            };

            PlayersDB.InsertOne(NewPlayer);
        }


        // Retorna la puntuación del jugador
        // Si no existe, retorna null
        public static int? GetPlayerScore(string name) {

            // Obtener datos de la base de datos
            var DDBB = GetDbData();
            var PlayersDB = DDBB.GetCollection<Players>("players");

            // Revisar si ya existe el jugador
            var ThisPlayer = PlayersDB.Find(x => x.Name == name).FirstOrDefault();
            if (ThisPlayer == null) return null;
            
            return ThisPlayer.Score;
        }


        // Guardará la puntuación del jugador si es superior a su máxima puntuación
        // Retorna si tiene un nuevo récord
        public static bool SavePlayerScore(string name, int score)
        {

            // Obtener datos de la base de datos
            var DDBB = GetDbData();
            var PlayersDB = DDBB.GetCollection<Players>("players");

            // Obtener objeto de jugador
            var ThisPlayer = PlayersDB.Find(x => x.Name == name).FirstOrDefault();
            if (ThisPlayer == null) throw new Exception("MongoPokePlatform Error: Can't find user object to save score");

            // Si no ha superado su puntuación, volver
            if (ThisPlayer.Score > score) return false;

            // En el caso contrario, guarda la puntuación
            var UpdatedPlayer = Builders<Players>.Update.Set("Score", score);
            var Filter = Builders<Players>.Filter.Eq("Name", name);

            PlayersDB.UpdateOne(Filter, UpdatedPlayer);
            return true;

        }


        // Retorna las x puntuaciones más bajas
        public static List<Players> GetLowestScores(int amount)
        {

            // Obtener datos de la base de datos
            var DDBB = GetDbData();
            var PlayersDB = DDBB.GetCollection<Players>("players");

            if (amount < 1) return new List<Players>();

            // Obtener todos los jugadores
            var AllPlayers = PlayersDB.Find(x => true).ToList();
            if (AllPlayers == null) return new List<Players>();

            // Ordenarlos por puntuación
            AllPlayers.OrderBy(l => l.Score);

            // Arreglar cantidad solicitada
            var FixedAmount = amount;
            if (amount > AllPlayers.Count) FixedAmount = AllPlayers.Count;

            // Guardar los más bajos
            List<Players> LowestScores = new List<Players>();

            // Guardar puntuaciones más bajas
            for (int i = 0; i < FixedAmount; i++)
            {
                LowestScores.Add(AllPlayers[i]);
            }

            return LowestScores;

        }


        // Retorna las x puntuaciones más altas
        public static List<Players> GetHighestScores(int amount)
        {

            // Obtener datos de la base de datos
            var DDBB = GetDbData();
            var PlayersDB = DDBB.GetCollection<Players>("players");

            if (amount < 1) return new List<Players>();

            // Obtener todos los jugadores
            var AllPlayers = PlayersDB.Find(x => true).ToList();
            if (AllPlayers == null) return new List<Players>();

            // Ordenarlos por puntuación
            AllPlayers.OrderBy(l => l.Score);

            // Arreglar cantidad solicitada
            var FixedAmount = amount;
            if (amount > AllPlayers.Count) FixedAmount = AllPlayers.Count;

            // Guardar los más altos
            List<Players> HighestScores = new List<Players>();

            // Guarda las puntuaciones más altas
            for (int i = AllPlayers.Count-1; i >= AllPlayers.Count-FixedAmount; i--) {
                HighestScores.Add(AllPlayers[i]);
            }

            return HighestScores;

        }

           // Retorna las puntuaciones más altas que x
        public static List<Players> GetGreaterThan(int amount)
        {

            // Obtener datos de la base de datos
            var DDBB = GetDbData();
            var PlayersDB = DDBB.GetCollection<Players>("players");

            if (amount < 1) return new List<Players>();

            // Obtener todos los jugadores
            var AllPlayers = PlayersDB.Find(x => true).ToList();
            if (AllPlayers == null) return new List<Players>();

            //Lista guardar jugadores con puntuaciones más altas que x
            List<Players> GreaterThan = new List<Players>();

            foreach (var player in AllPlayers)
            {
                if (player.Score > amount)
                {
                    GreaterThan.Add(player);
                }
            }
             return GreaterThan;
        }


        // Conexión con MongoDB
        // Retorna la DB "data"
        #nullable enable // Deshabilita avisos por posibles valores null 
        private static IMongoDatabase? GetDbData()
        {
            try
            {
                string ConnectionUrl = "mongodb+srv://player:FLmKL-z4.d88wT_@cluster0.2q7ocry.mongodb.net/?retryWrites=true&w=majority";
                var Cluster = new MongoClient(ConnectionUrl);
                return Cluster.GetDatabase("data");
            }
            catch(Exception _err)
            {
                Debug.Log($"MongoPokePlatform Error: Can't establish a connection with the DB. Internet connection may be unstable\n\n{_err}");
                return null;
            }
        }
    }
}