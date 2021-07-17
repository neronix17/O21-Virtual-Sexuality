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
    public class CompProperties_Sexuality : CompProperties
    {
        public CompProperties_Sexuality()
        {
            compClass = typeof(Comp_Sexuality);
        }
    }
}
