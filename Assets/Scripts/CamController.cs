using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject tableViewpoint;
    public GameObject arenaViewpoint;

    public void Awake() {
        LookAtTable();
    }

    public void LookAtTable() {
        print("Looing At Table");
        transform.position = tableViewpoint.transform.position;
        transform.rotation = tableViewpoint.transform.rotation;
    }

    public void LookAtArena() {
        print("Looking at Arena");
        transform.position = arenaViewpoint.transform.position;
        transform.rotation = arenaViewpoint.transform.rotation;
    }
}
