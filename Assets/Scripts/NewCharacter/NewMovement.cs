using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    private GameObject character;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal") * moveSpeed;
        character.transform.Translate(new Vector3(move, 0, 0));
    }
}
