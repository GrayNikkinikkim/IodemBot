﻿using IodemBot.Modules.ColossoBattles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IodemBot.Modules.GoldenSunMechanics
{
    internal class ConditionEffect : IEffect
    {
        private Condition Cond;
        private int Probability = 10;

        public ConditionEffect(string stringCondition, int probability)
        {
            init(stringCondition, probability);
        }

        public ConditionEffect(params string[] args)
        {
            if (args.Length == 2)
            {
                int prob = 10;
                int.TryParse(args[1], out prob);
                init(args[0], prob);
            }
            else
            {
                throw new ArgumentException("Condition, probability");
            }
        }

        public override string ToString()
        {
            return $"{(Probability != 100 ? $"{Probability}% chance to apply" : "Apply")} {Cond}.";
        }

        private void init(string stringCondition, int probability)
        {
            Probability = probability;
            if (!Enum.TryParse<Condition>(stringCondition, out Cond))
            {
                throw new ArgumentException("stringCondition");
            }
        }

        protected override int InternalChooseBestTarget(List<ColossoFighter> targets)
        {
            var unaffectedEnemies = targets.Where(s => !s.HasCondition(Cond)).ToList();
            return targets.IndexOf(unaffectedEnemies[Global.random.Next(0, unaffectedEnemies.Count)]);
        }

        protected override bool InternalValidSelection(ColossoFighter user)
        {
            return !user.getEnemies().All(s => s.HasCondition(Cond));
        }

        public override List<string> Apply(ColossoFighter User, ColossoFighter Target)
        {
            List<string> log = new List<string>();
            if (Target.isImmuneToEffects)
            {
                return log;
            }

            if (!Target.IsAlive())
            {
                return log;
            }

            if (Global.random.Next(1, 100) <= Probability)
            {
                Target.AddCondition(Cond);
                log.Add($"{Target.name} gets hit with {Cond.ToString()}!");
            }
            return log;
        }
    }
}