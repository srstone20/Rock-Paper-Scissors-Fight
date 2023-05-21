using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementv2 : MonoBehaviour
{
    public int speed = 8;
    public int jumpForce = 40;
    public float legSpeed = 1f;

    private Vector3 lastPos;
    private int legCheck = 0;
    private int legCount = 0;
    //private Vector3 rightL;
    //private Vector3 leftL;
    private int rightRotate = 0;
    private int leftRotate = 0;
    private bool moving = false;

    Vector2 moveBody;
    Rigidbody rb;
    GameController gc;

    private bool _grounded = true;
    public bool grounded {
        get {return _grounded;}
        set {_grounded = value;}
    }

    private GroundCheck grndCheck;
    public JumpBind jb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grndCheck = transform.Find("groundCheck").GetComponent<GroundCheck>();

        gc = GameObject.Find("Table").GetComponent<GameController>();
        gc.NotifyPlayerReady(gameObject);
    }

    void Jump()
    {
        print("JUMPING! " + grounded);
        if (jb != null) jb.Jump();
        if (!grounded) {return;}
        TrueJump();
    }

    public void TrueJump() {
        print("Force: " + jumpForce);
        rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
    }

    void Update()
    {
        Vector2 m = new Vector2(moveBody.x * speed, moveBody.y * 0) * Time.deltaTime;
        transform.Translate(m, Space.World);

        //legs
        Transform rightLeg = transform.GetChild(0).GetChild(5).transform;
        Transform leftLeg = transform.GetChild(0).GetChild(6).transform;
        
        if (transform.position != lastPos)
        {
            if (!moving)
            {
                bool moving = true;
                if (legCount <= 90 && legCheck == 0)
                {
                    legCount++;
                    rightLeg.transform.Rotate(0, 0, -1);
                    leftLeg.transform.Rotate(0, 0, 1);
                }
                else
                {
                    legCheck = 1;
                }
                if (legCount >= -90 && legCheck == 1)
                {
                    legCount--;
                    rightLeg.transform.Rotate(0, 0, 1);
                    leftLeg.transform.Rotate(0, 0, -1);
                }
                else
                {
                    legCheck = 0;
                }
            }
        }
        else
        {
            bool moving = false;
            legCount = 0;
            rightLeg.transform.rotation = Quaternion.Euler(0,0,0);
            leftLeg.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        lastPos = transform.position;
    }

    void OnMove(InputValue value)
    {
        moveBody = value.Get<Vector2>();
    }

    void OnJump()
    {
        Jump();
    }

    void OnRock()
    {
        gc.SetChoice(gameObject ,Choice.Rock);
    }

    void OnPaper()
    {
        gc.SetChoice(gameObject, Choice.Paper);
    }

    void OnScissors()
    {
        gc.SetChoice(gameObject, Choice.Scissors);
    }
}
