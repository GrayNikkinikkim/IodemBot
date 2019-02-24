﻿using IodemBot.Modules.ColossoBattles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IodemBot.Modules.GoldenSunMechanics
{
    public class Attack : Move
    {
        public Attack() : base("Attack", "<:Attack:536919809393295381>", Target.otherSingle, 1)
        {
        }

        public override object Clone()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Attack>(serialized);
        }

        public override List<string> Use(ColossoFighter User)
        {
            var enemy = User.battle.getTeam(User.enemies)[targetNr];

            var log = new List<string>();
            log.Add($"{emote} {User.name} attacks!");
            if (!enemy.IsAlive())
            {
                log.Add($"{enemy.name} is down already!");
                return log;
            }

            int chanceToMiss = 8;
            if (User.HasCondition(Condition.Delusion)) chanceToMiss = 3;
           
            if(Global.random.Next(0, chanceToMiss) == 0)
            {
                log.Add($"{enemy.name} dodges the blow!");
                return log;
            }
            
            var atk = User.stats.Atk * User.MultiplyBuffs("Attack");
            var def = enemy.stats.Def * enemy.MultiplyBuffs("Defense");
            Console.WriteLine(User.MultiplyBuffs("Attack"));
            uint damage = 1;
            if (def < atk)
            {
                damage = (uint) (atk - def)/2 + (uint)Global.random.Next(1, 4);
            }
            if (Global.random.Next(0, 8) == 0)
            {
                log.Add("Critical!!");
                damage = (uint)(damage*1.25 + Global.random.Next(5,15));    
            }
            log.AddRange(enemy.dealDamage(damage));
            if (User is PlayerFighter)
            {
                ((PlayerFighter)User).avatar.dealtDmg(damage);
                if (!enemy.IsAlive()) ((PlayerFighter)User).avatar.killedByHand();
            }
            return log;
        }
    }
}