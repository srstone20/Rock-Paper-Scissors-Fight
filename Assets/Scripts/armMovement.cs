using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class armMovement : MonoBehaviour
{
    public int speed = 1;
    Vector2 arms;
    
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(new Vector2(arms.x * speed, arms.y * speed), ForceMode.Impulse);
    }

    void OnArms(InputValue value)
    {
        arms = value.Get<Vector2>();
    }
}
