using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace BasicSexuality
{
    [HarmonyPatch(typeof(CharacterCardUtility), "DrawCharacterCard")]
    public static class Patch_CharacterCardUtility_DrawCharacterCard
    {
        [HarmonyPostfix]
        public static void Postfix(Rect rect, Pawn pawn)
        {
            Comp_Sexuality comp = pawn.TryGetComp<Comp_Sexuality>() ?? null;
            if (comp == null)
            {
                return;
            }
            SexualityUtils.DrawSexualityBio(pawn, comp, new Rect(rect.x, rect.yMax - 58f, rect.width, rect.height));
        }
    }
}
