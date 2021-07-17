using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace BasicSexuality
{
    public class SexualitySettings : ModSettings
    {
        public float interspeciesMonsterMash = 0.00f;
        public string interspeciesMonsterMash_buffer;
        public bool interspeciesWithXenophile = true;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref interspeciesMonsterMash, "interspeciesMonsterMash", 0.00f);
            Scribe_Values.Look(ref interspeciesWithXenophile, "interspeciesWithXenophile", true);
        }
    }
}
