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
    public static class SexualityUtils
    {
        public static void DrawSexualityBio(Pawn pawn, Comp_Sexuality comp, Rect rect)
        {
            float CurY;
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(rect);
            listing.Label(string.Format("<b>{0}</b>", "BS_SexualityHeader".Translate(pawn.LabelShortCap)), -1f, null);
            listing.GapLine();
            CurY = listing.curY;
            listing.End();


            listing = new Listing_Standard()
            {
                ColumnWidth = rect.width / 2f,
            };
            listing.Begin(rect);
            listing.curY = CurY;
            listing.Label("BS_SexualityLabel".Translate() + ": " + GetSexualityTranslatable(comp.sexuality).Translate(), tooltip: (GetSexualityTranslatable(comp.sexuality) + "_Tip").Translate());
            listing.NewColumn();
            listing.curY = CurY;
            listing.Label("BS_GenderLabel".Translate() + ": " + GetGenderTranslatable(comp.genderIdentity).Translate(), tooltip:(GetGenderTranslatable(comp.genderIdentity) + "_Tip").Translate());
            listing.End();
        }

        public static string GetSexualityTranslatable(Sexuality sexuality)
        {
            return ("BS_" + sexuality.ToString());
        }

        public static string GetGenderTranslatable(GenderIdentity genderIdentity)
        {
            return ("BS_" + genderIdentity.ToString());
        }

        public static Sexuality GenerateSexuality()
        {
            return Sexuality.Straight;
        }

        public static GenderIdentity GenerateGenderIdentity()
        {
            return GenderIdentity.Cisgender;
        }
    }
}
