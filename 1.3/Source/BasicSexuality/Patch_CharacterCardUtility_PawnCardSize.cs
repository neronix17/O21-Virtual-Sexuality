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
    [HarmonyPatch(typeof(CharacterCardUtility), "PawnCardSize")]
    public static class Patch_CharacterCardUtility_PawnCardSize
    {
        [HarmonyPostfix]
        public static void Postfix(ref Vector2 __result, Pawn pawn)
        {
            if (pawn.RaceProps.Animal || pawn.TryGetComp<Comp_Sexuality>() == null)
            {
                return;
            }
            __result += new Vector2(0f, 58f);
        }
    }
}
