using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This class handles the fighter interactions with the environment and the fighter's internal state
*/
public class Fighter : MonoBehaviour
{
    [Header("Inscribed")]
    public float maxHealth = 100f;
    public bool testMode = false;
    public string weaponTag;
    public float damageMult = 1;
    public GameObject defaultWeapon;

    [Header("Dynamic")]
    public float currentHealth;

    public AudioSource hurt1;
    public AudioSource hurt2;
    public AudioSource hurt3;
    public AudioSource hurt4;
    public AudioSource hurt5;
    public AudioSource hurt6;
    public AudioSource hurt7;
    public AudioSource hurt8;
    public AudioSource hurt9;

    public AudioSource lose1;

    SoftJointLimit swing = new SoftJointLimit();
    SoftJointLimit twist = new SoftJointLimit();
    public AudioSource lose2;
    public AudioSource lose3;
    public AudioSource lose4;
    public AudioSource lose5;
    public AudioSource lose6;
    public AudioSource lose7;
    public AudioSource lose8;
    public AudioSource lose9;

    private GameController gc;
    private HealthBar healthBar;
    private Rigidbody rb;
    private Vector3 startPosition;
    private bool hasDied = false; // prevents called GC multiple times
    private static int playerCount = 0;
    private GameObject weapon;

    void Awake()
    {
        if (!testMode) {
            gc = GameObject.Find("Table").GetComponent<GameController>();
            gc.NotifyPlayerReady(this.gameObject);
            startPosition = gc.RequestStartPosition(this.gameObject);
        }

        healthBar = GetComponentInChildren<HealthBar>();
        rb = GetComponent<Rigidbody>();

        playerCount++;
        if (playerCount == 1)
        {
            transform.Rotate(0, -180, 0);
        }

        ReadyPlayerForRPS();
    }

    public void ReadyPlayerForRPS() {
        transform.position = startPosition;
    }

    public void ReceiveWeapon(GameObject weapon) {
        if (gameObject.name == "Fighter 1")
        {
            if(weapon.name == "Wooden Shield")
            {
                weapon.transform.Rotate(new Vector3(0,0,0));
            }
            else
            {
                weapon.transform.Rotate(new Vector3(0, -90, -90));
            }
        }
        else
        {
            if (weapon.name == "Wooden Shield")
            {
                weapon.transform.Rotate(new Vector3(0, 180, 0));
            }
            else
            {
                weapon.transform.Rotate(new Vector3(0, -90, 90));
            }
        }

        // set tags
        weapon.gameObject.layer = gameObject.layer;
        int i = 0;
        Transform child = weapon.transform.GetChild(i);
        try
        {
            while (child != null)
            {
                child.gameObject.layer = gameObject.layer;
                i++;
                child = weapon.transform.GetChild(i); // this line throws an error
            }
        }
        catch (UnityException e)
        {
            // Catch out of bounds exception and ignore
        }


        // weaponJoint.connectedBody = hand;
        // weaponJoint.connectedAnchor = new Vector3(0, 0, 0);

        // weapon.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public void ReadyPlayerForFight() {
        print(gameObject.name  + " ready");
        currentHealth = maxHealth;
        hasDied = false;
        healthBar.SetMaxHealth(maxHealth);
        transform.position = startPosition;
        if(weapon == null)
        {
            ReceivePrize(Instantiate(defaultWeapon));
        }
    }

    public void ReceivePrize(GameObject prize) {
        print(prize.name);

        // if an prize is an item (health-up etc)
        Item item = prize.GetComponent<Item>();
        if (item != null) {
            item.ApplyEffect(this);
            return;
        }

        // if prize is a jump-bound item
        JumpBind jb = prize.GetComponent<JumpBind>();
        if (jb != null) {
            GetComponent<PlayerMovementv2>().jb = jb;
            jb.pmove = GetComponent<PlayerMovementv2>();
            if (prize.GetComponent<DoubleJump>() != null) {
                print("put on boots");
                PutOnBoots(jb.gameObject);
            }
            else if (prize.GetComponent<Wings>() != null) {
                WearWings(jb.gameObject);
            }
            return;
        }

        // if already have a weapon, destroy and replace it
        if (weapon != null) {
            Destroy(this.weapon);
        }
        weapon = prize;

        weapon.GetComponent<Weapon>().damage *= damageMult;

        /* Give the Weapon to the player. Put in their hand. */
       
        twist.limit = 70;
        twist.contactDistance = 0;
        twist.bounciness = 0;

        swing.limit = 3;
        swing.contactDistance = 0;
        swing.bounciness = 0;

        Rigidbody rightHand = transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody>();
        Transform hand = transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;

        weapon.transform.parent = hand;
        weapon.transform.position = hand.position;
        weapon.transform.rotation = hand.rotation;
        weapon.AddComponent<CharacterJoint>();
        CharacterJoint handjoint = weapon.GetComponent<CharacterJoint>();
        handjoint.anchor = new Vector3(0,0,0);
        handjoint.swing1Limit = swing;
        handjoint.swing2Limit = swing;
        handjoint.lowTwistLimit = twist;
        handjoint.highTwistLimit = twist;
        handjoint.connectedBody = rightHand;

        // set tags
        weapon.gameObject.layer = gameObject.layer;
        int i = 0;
        Transform child = weapon.transform.GetChild(i);
        try
        {
            while (child != null)
            {
                child.gameObject.layer = gameObject.layer;
                i++;
                child = weapon.transform.GetChild(i); // this line throws an error
            }
        }
        catch (UnityException e)
        {
            // Catch out of bounds exception and ignore
        }

        // weaponJoint.connectedBody = hand;
        // weaponJoint.connectedAnchor = new Vector3(0, 0, 0);

        // weapon.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public void Hurt(float damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) {
            Die();
        }
    }

    public void HurtAndKnockback(float damage, Vector3 force) {
        print("KNOCKBACK APPLIED");
        print(force);
        Hurt(damage);
        rb.AddForce(force);
    }

    public void TakeDamage(float damage) {
        Hurt(damage);

        int randomSound = Random.Range(1, 9);
        switch (randomSound)
        {
            case 9:
                hurt9.Play();
                break;
            case 8:
                hurt8.Play();
                break;
            case 7:
                hurt7.Play();
                break;
            case 6:
                hurt6.Play();
                break;
            case 5:
                hurt5.Play();
                break;
            case 4:
                hurt4.Play();
                break;
            case 3:
                hurt3.Play();
                break;
            case 2:
                hurt2.Play();
                break;
            case 1:
                hurt1.Play();
                break;
            default:
                hurt9.Play();
                break;
        }
    }

    void Die() {
        if (!hasDied && !testMode) {
            gc.NotifyPlayerDied(this.gameObject);
        }
        hasDied = true;

        int randomSound = Random.Range(1, 9);
        switch (randomSound)
        {
            case 9:
                lose9.Play();
                break;
            case 8:
                lose8.Play();
                break;
            case 7:
                lose7.Play();
                break;
            case 6:
                lose6.Play();
                break;
            case 5:
                lose5.Play();
                break;
            case 4:
                lose4.Play();
                break;
            case 3:
                lose3.Play();
                break;
            case 2:
                lose2.Play();
                break;
            case 1:
                lose1.Play();
                break;
            default:
                lose9.Play();
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "blade")
        {
            // print("enter");
            TakeDamage(20);
        }
    }

    private void PutOnBoots(GameObject boots) {
        Transform lfoot = transform.Find("body").Find("fulllLeg").Find("lFoot");
        Transform rfoot = transform.Find("body").Find("fullrLeg").Find("rFoot");
        Transform lshoe = boots.transform.Find("LeftShoe");
        Transform rshoe = boots.transform.Find("RightShoe");

        lshoe.SetParent(lfoot);
        lshoe.localPosition = new Vector3(0.1f, 0, 0);
        rshoe.SetParent(rfoot);
        rshoe.localPosition = new Vector3(0.1f, 0, 0);

        lshoe.Rotate(0, 120, 0);
        rshoe.Rotate(0, 12, 0);
    }

    private void WearWings(GameObject wings) {
        Transform body = transform.Find("body");
        wings.transform.SetParent(body);
        wings.transform.localPosition = new Vector3(0, -0.7f, 0);
        wings.transform.rotation = Quaternion.identity;
        wings.transform.Rotate(new Vector3(0, 90, 0));
    }
}
