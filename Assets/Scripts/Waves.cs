using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves : MonoBehaviour {
    [SerializeField] private GameObject[] pool;

    private int lowi = 0;
    private int highi = 0;

    public GameObject defaltWeapon {
        get {return _defaultWeapon;}
        private set {_defaultWeapon = value;}
    }
    [SerializeField] private GameObject _defaultWeapon;

    [System.Serializable]
    private class Wave {
        public GameObject[] prizes;
    }
    [SerializeField] private Wave[] waves;

    void Awake() {
        int totPrizes = 0;
        foreach (Wave wave in waves) {
            foreach (GameObject prize in wave.prizes) {
                totPrizes++;
            }
        }
        pool = new GameObject[totPrizes];
    }

    public GameObject[] PickPrizes(int waveNumber) {
        // if wave number is low enough, append new wave to pool
        if (waveNumber < waves.Length) {
            for (int i = 0; i < waves[waveNumber].prizes.Length; i++) {
                pool[highi] = waves[waveNumber].prizes[i];
                highi++;
            }
        }
        // ActivateWeapons(); 
       ShufflePool();
        return new GameObject[] {
            Instantiate(pool[lowi]),
            Instantiate(pool[lowi+1]),
            Instantiate(pool[lowi+2])
        };
    }

    /* Sets all weapons to active since they may have been set inactive */
    private void ActivateWeapons() {
        for (int i = lowi; i < highi; i++) {
            pool[i].SetActive(true);
        }
    }

    public void RemovePrize(GameObject prize) {
        // find the weapon
        int index = lowi;
        for (int i = lowi; i < highi; i++) {
            if (Object.ReferenceEquals(pool[i], prize)) {
                index = i;
                break;
            }
        }

        // swap to front
        Swap(lowi, index);

        // increment lowi
        lowi++;
    }

    private void ShufflePool() {
        for (int i = lowi; i < highi; i++) {
            int randi = Random.Range(lowi, highi-1);
            Swap(i, randi);
        }
    }

    private void Swap(int i, int j) {
        GameObject tmp = pool[i];
        pool[i] = pool[j];
        pool[j] = tmp;
    }
}