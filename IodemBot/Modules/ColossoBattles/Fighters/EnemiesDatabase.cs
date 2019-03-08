﻿using IodemBot.Modules.GoldenSunMechanics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IodemBot.Modules.ColossoBattles.ColossoPvE;

namespace IodemBot.Modules.ColossoBattles
{
    public class EnemiesDatabase
    {
        private static List<List<ColossoFighter>> bronzeFighters;
        private static List<List<ColossoFighter>> silverFighters;
        private static List<List<ColossoFighter>> goldFighters;

        static EnemiesDatabase()
        {
            try
            {
                bronzeFighters = loadEnemiesFromFile("Resources/bronzeFighters.json");
                silverFighters = loadEnemiesFromFile("Resources/silverFighters.json");
                goldFighters = loadEnemiesFromFile("Resources/goldFighters.json");

            }
            catch (Exception e) // Just for debugging
            {
                Console.Write("A");
            }
        }

        public static List<List<ColossoFighter>> loadEnemiesFromFile(string filePath)
        {
            //List<List<ColossoFighter>> fighters = new List<List<ColossoFighter>>();
            string json = File.ReadAllText(filePath);
            List<List<NPCEnemy>> fighters = JsonConvert.DeserializeObject<List<List<NPCEnemy>>>(json);
            return fighters.Select(s1 => s1.Select(s2 => (ColossoFighter)s2).ToList()).ToList();
        }

        internal static List<ColossoFighter> getRandomEnemies(BattleDifficulty diff)
        {
            List<List<ColossoFighter>> selectedDifficulty;
            switch (diff)
            {
                case (BattleDifficulty.Easy):
                    selectedDifficulty = bronzeFighters;
                    break;
                case (BattleDifficulty.Medium):
                    selectedDifficulty = silverFighters;
                    break;
                case (BattleDifficulty.Hard):
                    selectedDifficulty = goldFighters;
                    break;
                default:
                    selectedDifficulty = bronzeFighters;
                    break;
            }
            return selectedDifficulty[(new Random()).Next(0, selectedDifficulty.Count)].Select(f => (ColossoFighter)f.Clone()).ToList();
        }

    }

}
