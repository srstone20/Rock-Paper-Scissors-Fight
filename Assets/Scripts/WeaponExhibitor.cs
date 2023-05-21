using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponExhibitor : MonoBehaviour
{
    [Header("Inscribed")]
    public float rotationRate = 20f;

    private Pedestal rockPedestal;
    private Pedestal paperPedestal;
    private Pedestal scissorsPedestal;

    void Awake() {
        rockPedestal = transform.Find("Rock Pedestal").GetComponent<Pedestal>();
        rockPedestal.rotationRate = rotationRate;

        paperPedestal = transform.Find("Paper Pedestal").GetComponent<Pedestal>();
        paperPedestal.rotationRate = rotationRate;
        
        scissorsPedestal = transform.Find("Scissors Pedestal").GetComponent<Pedestal>();
        scissorsPedestal.rotationRate = rotationRate;

        print(rockPedestal);
        print(paperPedestal);
        print(scissorsPedestal);
    }

    /* Displays weapons on their pedestals*/
    public void ExhibitPrizes(GameObject[] weapons) {
        rockPedestal.ExhibitPrize(weapons[0]);
        paperPedestal.ExhibitPrize(weapons[1]);
        scissorsPedestal.ExhibitPrize(weapons[2]);
    }

    public void StopExhibit() {
        rockPedestal.StopExhibit();
        paperPedestal.StopExhibit();
        scissorsPedestal.StopExhibit();
    }
}
