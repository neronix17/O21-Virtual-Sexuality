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
    public static class SexualityUtils
    {
        public static void DrawSexualityBio(Pawn pawn, Comp_Sexuality comp, Rect rect)
        {
            float CurY;
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(rect);
            listing.Label(string.Format("<b>{0}</b>", "VS_SexualityHeader".Translate(pawn.LabelShortCap)), -1f, null);
            listing.GapLine();
            CurY = listing.curY;
            listing.End();


            listing = new Listing_Standard()
            {
                ColumnWidth = rect.width / 2f,
            };
            listing.Begin(rect);
            listing.curY = CurY;
            listing.Label("VS_SexualityLabel".Translate() + ": " + GetSexualityTranslatable(comp.sexuality).Translate(), tooltip: (GetSexualityTranslatable(comp.sexuality) + "_Tip").Translate());
            listing.NewColumn();
            listing.curY = CurY;
            //listing.Label("VS_GenderLabel".Translate() + ": " + GetGenderTranslatable(comp.genderIdentity).Translate(), tooltip:(GetGenderTranslatable(comp.genderIdentity) + "_Tip").Translate());
            listing.End();
        }

        public static string GetSexualityTranslatable(Sexuality sexuality)
        {
            return ("VS_" + sexuality.ToString());
        }

        public static string GetGenderTranslatable(GenderIdentity genderIdentity)
        {
            return ("VS_" + genderIdentity.ToString());
        }

        public static Sexuality GenerateSexuality()
        {
            SexualityWeight result = null;
            SexualityMod.settings.sexualityWeighted.TryRandomElementByWeight(sw => sw.weight, out result);
            if (result != null)
            {
                return result.sexuality;
            }
            else
            {
                Log.Warning("Could not select sexuality by weight. Defaulting to breeder.");
                return Sexuality.Straight;
            }
        }

        public static GenderIdentity GenerateGenderIdentity()
        {
            return GenderIdentity.Cisgender;
        }

        public static float InterspeciesModifier(Pawn initiator, Pawn target)
        {
            if(initiator.def == target.def)
            {
                return 1f;
            }
            float interspeciesModifier = 1f;
            if (initiator.def != target.def)
            {
                if (SexualityMod.settings.interspeciesWithXenophile)
                {
                    if (initiator.story.traits.DegreeOfTrait(AlienRace.AlienDefOf.Xenophobia) == -1)
                    {
                        if (target.story.traits.DegreeOfTrait(AlienRace.AlienDefOf.Xenophobia) == -1)
                        {
                            interspeciesModifier = 1f;
                        }
                        else
                        {
                            interspeciesModifier = 0.5f;
                        }
                    }
                }
            }
            return interspeciesModifier *= SexualityMod.settings.interspeciesMonsterMash;
        }
    }
}
