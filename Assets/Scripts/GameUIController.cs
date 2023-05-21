using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {
    [Header("Inscribed")]
    public string readyText = "Ready!";
    public string rockText = "Rock...";
    public string paperText = "Paper...";
    public string scissorsText = "Scissors...";
    public string shootText = "Shoot!";

    private bool _on = true;
    public bool on {
        get { return _on; }
        set {
            _on = value;
            LoadUI();
        }
    }

    private GameObject textGO;
    private GameObject textBoxGO;
    
    private Text tally1; 
    private Text tally2; 
    private Text textCenter;

    public int numPoints1 = 0;
    public int numPoints2 = 0;

    private GameController gc;

    void Awake() {
        textGO = transform.Find("CenterText").gameObject;
        textBoxGO = transform.Find("CenterTextBox").gameObject;
        textCenter = textGO.GetComponent<Text>();
        tally1 = transform.Find("Fighter1Info").Find("Tally1").gameObject.GetComponent<Text>();
        tally2 = transform.Find("Fighter2Info").Find("Tally2").gameObject.GetComponent<Text>();
        tally1.text = "Fighter 1: " + numPoints1;
        tally2.text = "Fighter 2: " + numPoints2;
        gc = GameObject.Find("Table").GetComponent<GameController>();
    }

    private void LoadUI() {
        if (on) {
            textGO.SetActive(true);
            textBoxGO.SetActive(true);
        }
        else {
            textGO.SetActive(false);
            textBoxGO.SetActive(false);
        }
    }

    public void TextReady() {
        textCenter.text = readyText;
    }

    public void TextRock() {
        textCenter.text = rockText;
    }

    public void TextPaper() {
        textCenter.text = paperText;
    }

    public void TextScissors() {
        textCenter.text = scissorsText;
    }

    public void TextShoot() {
        textCenter.text = shootText;
    }

    public void PromptReady() {
        textCenter.text = "Press any button to get ready.";
    }

    public void TextCenterWeapon(GameObject winner) {
        textCenter.text = winner.name + " won a weapon!";
    }

    public void TextCenterPoint(GameObject winner) {
        textCenter.text = winner.name + " won the round!";
        IncreaseTally(winner);
    }

    public void DisplayEndGameInfo(GameObject winner) {
        textCenter.text = winner.name + " won the game!";
    }

    public void IncreaseTally(GameObject winner) {
        print(winner.name);
        if (Object.ReferenceEquals(winner, gc.fighter1)) {
            numPoints1++;
            tally1.text = "Fighter 1: " + numPoints1;
        }
        else if (Object.ReferenceEquals(winner, gc.fighter2)) {
            numPoints2++;
            tally2.text = "Fighter 2: " + numPoints2;  
        }
        else {
            print("Something went wrong.");
        }
    }
}
