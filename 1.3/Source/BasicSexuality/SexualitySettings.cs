using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace VirtualSexuality
{
    public class SexualitySettings : ModSettings
    {
        // Sexuality Settings
        public List<SexualityWeight> sexualityWeighted = new List<SexualityWeight>();

        // HAR Compat.
        public float interspeciesMonsterMash = 0.00f;
        public string interspeciesMonsterMash_buffer;
        public bool interspeciesWithXenophile = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look<SexualityWeight>(ref sexualityWeighted, "sexualityWeighted", LookMode.Deep);

            Scribe_Values.Look(ref interspeciesMonsterMash, "interspeciesMonsterMash", 0.00f);
            Scribe_Values.Look(ref interspeciesWithXenophile, "interspeciesWithXenophile", true);
        }
    }

    public class SexualityWeight : IExposable
    {
        public Sexuality sexuality;
        public float weight;

        public SexualityWeight()
        {
        }

        public SexualityWeight(Sexuality sexuality, float weight)
        {
            this.sexuality = sexuality;
            this.weight = weight;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref sexuality, "sexuality", Sexuality.Straight);
            Scribe_Values.Look(ref weight, "weight", 0.2f);

        }
    }
}
