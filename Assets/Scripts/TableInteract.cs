using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteract : MonoBehaviour
{
    private GameObject weapon;

    public void ReceiveWeapon(GameObject weapon) {
        if (this.weapon != null) {
            Destroy(this.weapon);
        }
        this.weapon = weapon;

        Transform hand = transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
        weapon.transform.parent = hand;
        weapon.transform.position = hand.position;
        print("Weapon received!");
    }
}
