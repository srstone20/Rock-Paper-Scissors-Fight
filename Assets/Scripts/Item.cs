using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Prize
{
    public abstract void ApplyEffect(Fighter f);
}
