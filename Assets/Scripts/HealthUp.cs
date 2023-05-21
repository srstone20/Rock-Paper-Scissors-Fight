using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : Item
{
    public float increase = 50f;

    public override void ApplyEffect(Fighter f) {
        f.maxHealth += 50;
        f.currentHealth += 50;
    }
}
