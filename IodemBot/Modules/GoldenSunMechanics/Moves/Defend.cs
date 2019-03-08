﻿using IodemBot.Modules.ColossoBattles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IodemBot.Modules.GoldenSunMechanics
{
    public class Defend : Move
    {
        public Defend() : base("Defend", "<:Defend:536919830507552768>", Target.self, 1, new List<EffectImage>())
        {
            hasPriority = true;
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override void InternalChooseBestTarget(ColossoFighter User)
        {
            
        }

        public override bool InternalValidSelection(ColossoFighter User)
        {
            return Global.random.Next(0, 2) == 0;
        }

        protected override List<string> InternalUse(ColossoFighter User)
        {
            User.defensiveMult *= 0.5;
            return new List<string>();
        }
    }
}
