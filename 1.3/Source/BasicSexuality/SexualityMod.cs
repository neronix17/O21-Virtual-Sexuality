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

namespace BasicSexuality
{
    public class SexualityMod : Mod
    {
        public static SexualityMod mod;
        public static SexualitySettings settings;

        public SexualityMod(ModContentPack pack) : base(pack)
        {
            mod = this;
            settings = GetSettings<SexualitySettings>();

            try
            {
                new Harmony("neronix17.basicsexuality.rimworld").PatchAll();
            }
            catch (Exception e)
            {
                Log.Error("Failed to initiate BS patches! Basic Sexuality will probably have issues!");
            }
        }

        public override string SettingsCategory() => "Basic Sexuality";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard()
            {
                ColumnWidth = inRect.width / 2f
            };
            listing.Label("Interspecies (Alien Race Compat.)");
            listing.GapLine(); 
            Rect rect = listing.GetRect(18);
            Rect rect2 = rect.LeftHalf().Rounded();
            Rect rect3 = rect.RightHalf().Rounded();
            Text.Anchor = TextAnchor.MiddleRight;
            Widgets.Label(rect2, "Chance of Interpecies Monster Mash");
            Text.Anchor = TextAnchor.UpperLeft;
            Widgets.TextFieldPercent(inRect, ref settings.interspeciesMonsterMash, ref settings.interspeciesMonsterMash_buffer, 0f, 1f);
            listing.CheckboxEnhanced("Interspecies with Xenophile Trait", "Controls if pawns with the xenophile trait will bang, if true this will override the base chance above.", ref settings.interspeciesWithXenophile);

            base.DoSettingsWindowContents(inRect);
        }
    }
}
