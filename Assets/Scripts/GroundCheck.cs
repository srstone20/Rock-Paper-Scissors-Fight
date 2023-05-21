using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    private PlayerMovementv2 mov;
    void Awake() {
        mov = transform.parent.GetComponent<PlayerMovementv2>();
    }
    /* Notify player movement when grounded state changes. */
    /* Depends on physics layers */
    void OnTriggerEnter(Collider other) {
        print("grounded");
        mov.grounded = true;
    }
    void OnTriggerExit(Collider other) {
        print("aired");
        mov.grounded = false;
    }
}
