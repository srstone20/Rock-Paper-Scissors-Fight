using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Prize
{
    [Header("Inscribed")]
    public float damage;
    public float weight;
    public Vector3 localRotation;

    void Awake() {
        if (transform.Find("RotationCenter") == null) {
            throw new UnityException("Weapon needs a RotationCenter Game Object!");
        }
    }

    void OnTriggerEnter(Collider other) {
        // print("Collision enter");

        /* Non-special collisions */
        if (other.gameObject.layer == LayerMask.NameToLayer("Weapon")) {
            return;
        }

        /* Collision with self */
        if (GetComponentInParent<Fighter>() == other.GetComponentInParent<Fighter>()) {
            return;
        }

        Fighter enemy = other.GetComponentInParent<Fighter>();
        if (enemy != null) {
            // print(GetComponent<Rigidbody>());
            // print(other.attachedRigidbody);
            // tSpeed is VERY VERY low so other vals need to be VERY VERY high
            float tSpeed = GetComponent<Rigidbody>().velocity.magnitude + GetComponent<Rigidbody>().angularVelocity.magnitude;
            // print("SPEED OF WEAPON: " + tSpeed);
            //enemy.HurtAndKnockback(tSpeed * damage, tSpeed * weight * (transform.position - enemy.transform.position).normalized);
        }
    }

    void OnCollisionEnter(Collision col) {
        print("Collision enter");

        /* Non-special collisions */
        if (col.gameObject.layer == LayerMask.NameToLayer("Weapon")) {
            return;
        }

        Fighter enemy = col.gameObject.GetComponentInParent<Fighter>();
        if (enemy != null) {
            float tSpeed = GetComponent<Rigidbody>().velocity.magnitude;
            enemy.HurtAndKnockback(tSpeed * damage, tSpeed * weight * (transform.position - enemy.transform.position).normalized);
        }
    }
}
