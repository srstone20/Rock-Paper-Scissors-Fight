using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : JumpBind {
    private bool canDoubleJump = false;
    public override void Jump() {
        print("called Jump!");
        if (pmove.grounded) {
            print("jumped on ground");
            canDoubleJump = true;
            return;
        }
        else if (canDoubleJump) {
            print("d jump!");
            canDoubleJump = false;
            pmove.TrueJump();
        }
    }
}
