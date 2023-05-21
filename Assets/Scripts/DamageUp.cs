using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : Item
{
    [Tooltip("Increase is how much to multiply current attack stat by.")]
    public float increase = 1.1f;

    public override void ApplyEffect(Fighter f) {
        f.damageMult *= increase; //
    }
}
