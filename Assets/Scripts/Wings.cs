using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : JumpBind
{
    public int maxAirJumps = 3;
    private int jumps = 0;
    public override void Jump() {
        if (pmove.grounded) {
            jumps = 0;
            return;
        }
        else if (jumps <= maxAirJumps) {
            pmove.TrueJump();
            jumps++;
        }
    }
}
