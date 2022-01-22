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
            if (sexualityWeighted.NullOrEmpty())
            {
                sexualityWeighted.Add(new SexualityWeight(Sexuality.Straight, 80));
                sexualityWeighted.Add(new SexualityWeight(Sexuality.Gay, 20));
                sexualityWeighted.Add(new SexualityWeight(Sexuality.Bisexual, 20));
                sexualityWeighted.Add(new SexualityWeight(Sexuality.Pansexual, 20));
                sexualityWeighted.Add(new SexualityWeight(Sexuality.Asexual, 5));
            }

            Scribe_Values.Look(ref interspeciesMonsterMash, "interspeciesMonsterMash", 0.00f);
            Scribe_Values.Look(ref interspeciesWithXenophile, "interspeciesWithXenophile", true);
        }
    }

    public class SexualityWeight
    {
        public Sexuality sexuality;
        public int weight;

        public SexualityWeight(Sexuality sexuality, int weight)
        {
            this.sexuality = sexuality;
            this.weight = weight;
        }
    }
}
