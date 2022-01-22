using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace VirtualSexuality
{

    [HarmonyPatch(typeof(Pawn_RelationsTracker), "SecondaryLovinChanceFactor", null)]
    public static class Pawn_RelationsTracker_SecondaryLovinChanceFactor
    {
        public static bool Prefix(Pawn otherPawn, ref float __result, ref Pawn_RelationsTracker __instance)
        {
            Pawn pawn = __instance.pawn;

            float result;
            if (pawn.def != otherPawn.def || pawn == otherPawn)
            {
                result = 0f;
            }
            else
            {
                Comp_Sexuality pawnSexuality = pawn.TryGetComp<Comp_Sexuality>();
                if (pawnSexuality.sexuality == Sexuality.Asexual)
                {
                    result =  0f;
                }

                else if (pawn.gender != otherPawn.gender)
                {
                    if (pawn.gender == Gender.None || pawn.gender == Gender.None)
                    {
                        if(pawnSexuality.sexuality != Sexuality.Pansexual)
                        {
                            result = 0f;
                        }
                    }
                    else
                    {
                        if(pawnSexuality.sexuality != Sexuality.Straight
                            && pawnSexuality.sexuality != Sexuality.Pansexual
                            && pawnSexuality.sexuality != Sexuality.Bisexual)
                        {
                            result = 0f;
                        }
                    }
                }
                else if (pawn.gender == otherPawn.gender)
                {
                    if (pawnSexuality.sexuality != Sexuality.Gay
                        && pawnSexuality.sexuality != Sexuality.Pansexual
                        && pawnSexuality.sexuality != Sexuality.Bisexual)
                    {
                        result = 0f;
                    }
                }

                float pawnAge = pawn.ageTracker.AgeBiologicalYearsFloat;
                float targetAge = otherPawn.ageTracker.AgeBiologicalYearsFloat;
                if (pawnAge < 18f || targetAge < 18f)
                {
                    result = 0f;
                }
                else
                {
                    float ageFactor = CalculateAgeFactor(pawnAge, targetAge);

                    float prettiness = 0f;
                    if (otherPawn.RaceProps.Humanlike)
                    {
                        prettiness = otherPawn.GetStatValue(StatDefOf.PawnBeauty, true);
                    }

                    float prettinessFactor = 1f;
                    if (prettiness < 0f)
                    {
                        prettinessFactor = 0.3f;
                    }
                    else
                    {
                        if (prettiness > 0f)
                        {
                            prettinessFactor = 2.3f;
                        }
                    }

                    float final = ageFactor * Mathf.InverseLerp(16f, 18f, pawnAge) * Mathf.InverseLerp(16f, 18f, targetAge) * prettinessFactor;
                    result = final;
                }

                __result = result;
                return false;
            }

            __result = result;
            return true;
        }

        public static float CalculateAgeFactor(float pawnAge, float targetAge)
        {
            float min = (pawnAge / 2.0f) + 7.0f;
            float max = pawnAge + 10.0f;
            return GenMath.FlatHill(0.2f, min, Mathf.Lerp(min, pawnAge, 0.6f), Mathf.Lerp(pawnAge, max, 0.4f), max, 0.2f, targetAge);
        }
    }
}
