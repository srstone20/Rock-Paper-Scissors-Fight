using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JumpBind : Prize
{
    [HideInInspector] public PlayerMovementv2 pmove;
    public void SetJumpBind(PlayerMovementv2 pmove) {
        this.pmove = pmove;
    }
    public abstract void Jump();
}
