using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;
using O21Toolbox;

namespace VirtualSexuality
{
    public class SexualityMod : Mod
    {
        public static SexualityMod mod;
        public static SexualitySettings settings;

        public SexualityMod(ModContentPack pack) : base(pack)
        {
            mod = this;
            settings = GetSettings<SexualitySettings>();

            Log.Message("VS :: Virtual Sexuality :: Initialising...");

            try
            {
                new Harmony("neronix17.virtualSexuality.rimworld").PatchAll();
            }
            catch (Exception e)
            {
                Log.Error("Failed to complete sexuality patches! Virtual Sexuality will probably have issues!");
            }
        }

        public override string SettingsCategory() => "Virtual Sexuality";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard()
            {
                //ColumnWidth = inRect.width / 2f
            };
            listing.Begin(inRect);
            listing.Label("Sexuality Weighting");
            listing.GapLine();
            for (int i = 0; i < settings.sexualityWeighted.Count; i++)
            {
                listing.AddLabeledSlider(SexualityUtils.GetSexualityTranslatable(settings.sexualityWeighted[i].sexuality).Translate() + ": " + settings.sexualityWeighted[i].weight, ref settings.sexualityWeighted[i].weight, 0.0f, 1.0f, "Min: 0", "Max: 100", 0.01f);
            }
            listing.GapLine();
            listing.Label("Interspecies (Alien Race Compat.)");
            listing.GapLine();
            {
                listing.AddLabeledSlider("Chance of Interpecies Monster Mash: " + settings.interspeciesMonsterMash.ToStringPercent(), ref settings.interspeciesMonsterMash, 0.0f, 1.0f, "Min: 0%", "Max: 100%", 0.01f);
                listing.CheckboxEnhanced("Interspecies with Xenophile Trait", "If enabled, only pawns with the xenophile trait will attempt interspecies romance, the chance above is still used as a modifier to this.", ref settings.interspeciesWithXenophile);
            }
            listing.End();

            base.DoSettingsWindowContents(inRect);
        }
    }
}
