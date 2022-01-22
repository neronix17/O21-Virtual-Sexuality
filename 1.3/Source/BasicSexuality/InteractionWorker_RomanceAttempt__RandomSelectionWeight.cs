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
    [HarmonyPatch(typeof(InteractionWorker_RomanceAttempt), "RandomSelectionWeight")]
    public static class InteractionWorker_RomanceAttempt__RandomSelectionWeight
    {
        private static MethodInfo _method_calculate = AccessTools.Method(typeof(InteractionWorker_RomanceAttempt__RandomSelectionWeight), "CalculateAttractionFactor");

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> il = instructions.ToList();

            // Find: return 1.15f * num5 * num6 * num7 * num4 * num8;
            // num5 (slot 4) = 1.0f (gendered attraction)
            // num8 (slot 7) = CalculateAttractionFactor() (gayed attraction)
            for (int i = il.Count - 1; i > 0; --i)
            {
                // This is the load 1.15f; afterwards we write over two of the variables.
                if (il[i].opcode == OpCodes.Ldc_R4 && (float)il[i].operand == 1.15f)
                {
                    il.InsertRange(++i, new CodeInstruction[]
                    {
                        // Remove gendered attraction.
                        new CodeInstruction(OpCodes.Ldc_R4, 1.0f),
                        new CodeInstruction(OpCodes.Stloc_S, 4),

                        // Remove weird gay attraction thing.
                        new CodeInstruction(OpCodes.Ldarg_1),
                        new CodeInstruction(OpCodes.Ldarg_2),
                        new CodeInstruction(OpCodes.Call, _method_calculate),
                        new CodeInstruction(OpCodes.Stloc_S, 7),
                    });

                    return il.AsEnumerable();
                }
            }

            throw new Exception("Could not locate the correct instruction to patch - a mod incompatibility or game update broke it.");
        }

        public static float CalculateAttractionFactor(Pawn initiator, Pawn receipient)
        {
            bool initiator_into_it = false;
            bool receipient_into_it = false;

            Comp_Sexuality initiatorSexuality = initiator.TryGetComp<Comp_Sexuality>();
            Comp_Sexuality receipientSexuality = receipient.TryGetComp<Comp_Sexuality>();

            if(initiatorSexuality.sexuality == Sexuality.Asexual)
            {
                return 0f;
            }

            else if(initiator.gender != receipient.gender)
            {
                if (initiator.gender == Gender.None || initiator.gender == Gender.None)
                {
                    initiator_into_it = initiatorSexuality.sexuality == Sexuality.Pansexual;
                    receipient_into_it = receipientSexuality.sexuality == Sexuality.Pansexual;
                }
                else
                {
                    initiator_into_it = initiatorSexuality.sexuality == Sexuality.Straight
                        || initiatorSexuality.sexuality == Sexuality.Pansexual
                        || initiatorSexuality.sexuality == Sexuality.Bisexual;
                    receipient_into_it = receipientSexuality.sexuality == Sexuality.Straight
                        || receipientSexuality.sexuality == Sexuality.Pansexual
                        || receipientSexuality.sexuality == Sexuality.Bisexual;
                }
            }
            else if(initiator.gender == receipient.gender)
            {
                initiator_into_it = initiatorSexuality.sexuality == Sexuality.Gay
                    || initiatorSexuality.sexuality == Sexuality.Pansexual
                    || initiatorSexuality.sexuality == Sexuality.Bisexual;
                receipient_into_it = receipientSexuality.sexuality == Sexuality.Gay
                    || receipientSexuality.sexuality == Sexuality.Pansexual
                    || receipientSexuality.sexuality == Sexuality.Bisexual;
            }

            return ((initiator_into_it && receipient_into_it) ? 1.0f : 0.01f) * SexualityUtils.InterspeciesModifier(initiator, receipient);
        }
    }
}
