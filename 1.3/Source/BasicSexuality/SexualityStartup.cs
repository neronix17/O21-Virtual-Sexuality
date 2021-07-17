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
                Log.Message($"Patching BS onto humanlike races...");
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
                        Log.Message($"=> {thingDef.defName} Patched");
                    }
                }
            }
        }
    }
}
