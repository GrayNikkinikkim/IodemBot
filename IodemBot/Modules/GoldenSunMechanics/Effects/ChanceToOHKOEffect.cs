﻿using IodemBot.Modules.ColossoBattles;
using System.Collections.Generic;

namespace IodemBot.Modules.GoldenSunMechanics
{
    internal class ChancetoOHKOEffect : IEffect
    {
        private int Probability = 0;

        public override List<string> Apply(ColossoFighter User, ColossoFighter Target)
        {
            var log = new List<string>();
            if (Target.isImmuneToEffects)
            {
                return log;
            }

            if (Global.random.Next(1, 100) <= Probability)
            {
                Target.Kill();
                log.Add($":x: {Target.name}'s life was taken.");
            }
            return log;
        }

        public ChancetoOHKOEffect(string[] args)
        {
            timeToActivate = TimeToActivate.beforeDamge;

            if (args.Length == 1)
            {
                int.TryParse(args[0], out Probability);
            }
        }

        public override string ToString()
        {
            return $"{(Probability != 100 ? $"{Probability}% chance to eliminate" : "Eliminate")} target.";
        }
    }
}