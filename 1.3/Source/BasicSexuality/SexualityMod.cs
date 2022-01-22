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
                ColumnWidth = inRect.width / 2f
            };
            listing.Label("Sexuality Weighting");
            listing.GapLine();
            for (int i = 0; i < settings.sexualityWeighted.Count; i++)
            {
                
                int intBufferInt = settings.sexualityWeighted[i].weight;
                string intBufferString = intBufferInt.ToString();
                listing.TextFieldNumericLabeled(SexualityUtils.GetSexualityTranslatable(settings.sexualityWeighted[i].sexuality).Translate(), ref intBufferInt, ref intBufferString, 0, 100);
                settings.sexualityWeighted[i].weight = intBufferInt;
            }
            listing.GapLine();
            listing.Label("Interspecies (Alien Race Compat.)");
            listing.GapLine();
            {
                Rect rectInterspecies = listing.GetRect(18);
                Rect rectInterspeciesLeft = rectInterspecies.LeftHalf().Rounded();
                Rect rectInterspeciesRight = rectInterspecies.RightHalf().Rounded();
                Text.Anchor = TextAnchor.MiddleRight;
                Widgets.Label(rectInterspeciesLeft, "Chance of Interpecies Monster Mash");
                Text.Anchor = TextAnchor.UpperLeft;
                Widgets.TextFieldPercent(inRect, ref settings.interspeciesMonsterMash, ref settings.interspeciesMonsterMash_buffer, 0f, 1f);
            }
            listing.CheckboxEnhanced("Interspecies with Xenophile Trait", "Controls if pawns with the xenophile trait will bang, if true this will override the base chance above.", ref settings.interspeciesWithXenophile);

            base.DoSettingsWindowContents(inRect);
        }
    }
}
