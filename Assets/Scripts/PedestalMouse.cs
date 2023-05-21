using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalMouse : MonoBehaviour
{
    public GameObject halo;
    public GameObject table;
    private GameController gc;
    public enum choice { // this is a separate enum than Choice.choice!
        Rock, Paper, Scissors
    }
    public choice pedestalThrow;
    private Choice realChoice;

    private void Awake() {
        if (pedestalThrow == choice.Rock) {
            realChoice = Choice.Rock;
        }
        else if (pedestalThrow == choice.Paper) {
            realChoice = Choice.Paper;
        }
        else {
            realChoice = Choice.Scissors;
        }
        gc = table.GetComponent<GameController>();
    }

    void OnMouseEnter() {
        halo.SetActive(true);
    }

    void OnMouseExit() {
        halo.SetActive(false);
    }

    //void OnMouseDown() {
    //    gc.SetChoice(realChoice);
    //}
}
