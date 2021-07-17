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
    public class Comp_Sexuality : ThingComp
    {
        public Sexuality sexuality;

        public GenderIdentity genderIdentity;

        public Comp_Sexuality()
        {
            sexuality = SexualityUtils.GenerateSexuality();
            genderIdentity = SexualityUtils.GenerateGenderIdentity();
        }
    }

    public enum Sexuality
    {
        Straight,
        Gay,
        Bisexual,
        Pansexual,
        Asexual
    }

    public enum GenderIdentity
    {
        Cisgender,
        Transgender,
        Nonbinary
    }
}
