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
                if (pawn.story != null && pawn.story.traits != null)
                {
                    if (pawn.story.traits.HasTrait(TraitDefOf.Asexual))
                    {
                        result = 0f;
                    }

                    if (!pawn.story.traits.HasTrait(TraitDefOf.Bisexual))
                    {
                        if (pawn.story.traits.HasTrait(TraitDefOf.Gay))
                        {
                            if (otherPawn.gender != pawn.gender)
                            {
                                result = 0f;
                            }
                        }
                        else
                        {
                            if (otherPawn.gender == pawn.gender)
                            {
                                result = 0f;
                            }
                        }
                    }
                }

                float myAge = pawn.ageTracker.AgeBiologicalYearsFloat;
                float otherPawnAge = otherPawn.ageTracker.AgeBiologicalYearsFloat;
                if (myAge < 18f || otherPawnAge < 18f)
                {
                    result = 0f;
                }
                else
                {
                    float ageFactor = 1f;
                    if (pawn.gender == Gender.Male)
                    {
                        ageFactor = GenMath.FlatHill(0.2f, myAge - 30f, myAge - 10f, myAge + 3f, myAge + 10f, 0.2f, otherPawnAge);
                    }
                    else
                    {
                        if (pawn.gender == Gender.Female)
                        {
                            ageFactor = GenMath.FlatHill(0.2f, myAge - 10f, myAge - 3f, myAge + 10f, myAge + 30f, 0.2f,
                                otherPawnAge);
                        }
                    }

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

                    float final = ageFactor * Mathf.InverseLerp(16f, 18f, myAge) * Mathf.InverseLerp(16f, 18f, otherPawnAge) * prettinessFactor;
                    result = final;
                }

                __result = result;
                return false;
            }

            __result = result;
            return true;
        }
    }
}
