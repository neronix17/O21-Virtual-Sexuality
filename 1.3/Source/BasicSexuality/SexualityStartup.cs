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
    [StaticConstructorOnStartup]
    public static class SexualityStartup
    {
        static SexualityStartup()
        {
            PatchDefs();
        }

        public static void PatchDefs()
        {
            if (Prefs.DevMode)
            {
                Log.Message($"VS :: Virtual Sexuality :: Patching VS onto humanlike races...");
            }

            foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
            {
                if (thingDef?.race?.Humanlike ?? false)
                {
                    if (thingDef.comps == null)
                    {
                        thingDef.comps = new List<CompProperties>();
                    }
                    thingDef.comps.Add(new CompProperties_Sexuality());
                    if (Prefs.DevMode)
                    {
                        Log.Message($"VS :: {thingDef.defName} :: Sexuality Patched");
                    }
                }
            }

            SexualitySettings settings = SexualityMod.settings;
            if (settings.sexualityWeighted.NullOrEmpty())
            {
                settings.sexualityWeighted = new List<SexualityWeight>();
                settings.sexualityWeighted.Add(new SexualityWeight(Sexuality.Straight, 1f));
                settings.sexualityWeighted.Add(new SexualityWeight(Sexuality.Gay, 0.2f));
                settings.sexualityWeighted.Add(new SexualityWeight(Sexuality.Bisexual, 0.2f));
                settings.sexualityWeighted.Add(new SexualityWeight(Sexuality.Pansexual, 0.2f));
                settings.sexualityWeighted.Add(new SexualityWeight(Sexuality.Asexual, 0.05f));
            }
        }
    }
}
