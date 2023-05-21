using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Inscribed")]
    public int pointsToWin = 5;
    public bool debug = false;

    [Header("Timers")]
    public float chooseTimer = 5.0f;
    public float extraChooseTime = 1f;
    public float timeFromRPSToFight = 2f;
    public float timeFromDeathToRPS = 3f;

    [Header("References")]
    public Vector3 fighter1StartPos = new Vector3(-20, 5, 0);
    public Vector3 fighter2StartPos = new Vector3(20, 5, 0);
    public Vector3 tableStartPos = new Vector3(-20, 22, 1);
    public Vector3 tableAwayPos = new Vector3(-30, 15, -80);
    public GameObject[] allWeapons;
    public GameObject rockPedestal;
    public GameObject paperPedestal;
    public GameObject scissorsPedestal;

    [Header("Dynamic")]
    static public GameController S;
    public bool alreadyChose = false;
    public bool fightOver = false;

    private GameObject _fighter1;
    private GameObject _fighter2;
    public GameObject fighter1 {
        get {return _fighter1;}
        private set { _fighter1 = value;}
    }
    public GameObject fighter2 {
        get {return _fighter2;}
        private set { _fighter2 = value;}
    }


    public GameObject rockWeapon;
    public GameObject paperWeapon;
    public GameObject scissorsWeapon;

    private CamController cam;
    private Choice fighter1Choice = Choice.None;
    private Choice fighter2Choice = Choice.None;
    private GameObject weaponFighter1; // reference
    private GameObject weaponFighter2;

    private GameObject funTable;
    private GameObject barrier;
    private WeaponExhibitor exhib; 
    private GameUIController ui;
    private Waves waves;

    private int round = 0;

    void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<CamController>();
        ui = GameObject.Find("GameUI").GetComponent<GameUIController>();
        funTable = GameObject.Find("funTable").gameObject;
        exhib = GetComponent<WeaponExhibitor>();
        waves = GetComponent<Waves>();
        barrier = transform.Find("Barrier").gameObject;
    }

    void StartRPS() {
        transform.position = tableStartPos;
        funTable.transform.position = tableAwayPos;
        ui.on = true;
        cam.LookAtTable();
        barrier.SetActive(true);
        fighter1.GetComponent<Fighter>().ReadyPlayerForRPS();
        if (!debug) fighter2.GetComponent<Fighter>().ReadyPlayerForRPS();

        PickPrizes(); // creates and hands off to exhib
        Invoke("StartPick", chooseTimer / 4);
    }

    void StartFight() {
        print("Starting Fight");
        fightOver = false;
        transform.position = tableAwayPos;
        funTable.transform.position = tableStartPos;
        ui.on = false;
        fighter1.GetComponent<Fighter>().ReadyPlayerForFight();
        if (!debug) fighter2.GetComponent<Fighter>().ReadyPlayerForFight();
        cam.LookAtArena();
        barrier.SetActive(false);
        round++;
    }

    void StartPick() {
        // non-placeholder code
        alreadyChose = false;
        ui.TextReady();
        Invoke("ShoutRock", chooseTimer / 4);
    }

    void ShoutRock() {
        //print("Rock!");
        ui.TextRock();
        Invoke("ShoutPaper", chooseTimer / 4);
    }

    void ShoutPaper() {
        //print("Paper!");
        ui.TextPaper();
        Invoke("ShoutScissors", chooseTimer / 4);
    }

    void ShoutScissors() {
        //print("Scissors!");
        ui.TextScissors();
        Invoke("ShoutShoot", chooseTimer / 4);
    }

    void ShoutShoot() {
        print("Shoot!");
        ui.TextShoot();
        Invoke("EvaluateWinner", extraChooseTime);
    }

    void PickPrizes() {
        GameObject[] weaponOptions = waves.PickPrizes(round);
        rockWeapon = weaponOptions[0];
        paperWeapon = weaponOptions[1];
        scissorsWeapon = weaponOptions[2];
        exhib.ExhibitPrizes(weaponOptions);
    }

    void EvaluateWinner() {
        // this prevents from being executed twice in one round
        // once when both players decide
        // twice when the timer runs out
        if (alreadyChose) {
            return;
        }
        alreadyChose = true;

        GameObject winnerWeapon = null;
        GameObject winner = null;
        if (fighter1Choice > fighter2Choice) {
            winner = fighter1;
            if (fighter1Choice == Choice.Rock) {
                winnerWeapon = rockWeapon;
            }
            else if (fighter1Choice == Choice.Paper) {
                winnerWeapon = paperWeapon;
            }
            else if (fighter1Choice == Choice.Scissors) {
                winnerWeapon = scissorsWeapon;
            }
            else {
                throw new Exception("Something went wrong with RPS. Fighter 1 Choice is: " + fighter1Choice + ". Fighter 2 Choice is: " + fighter2Choice);
            }
        }
        else if (fighter2Choice > fighter1Choice) {
            winner = fighter2;
            if (fighter2Choice == Choice.Rock) {
                winnerWeapon = rockWeapon;
            }
            else if (fighter2Choice == Choice.Paper) {
                winnerWeapon = paperWeapon;
            }
            else if (fighter2Choice == Choice.Scissors) {
                winnerWeapon = scissorsWeapon;
            }
            else {
                throw new Exception("Something went wrong with RPS. Fighter 1 Choice is: " + fighter1Choice + ". Fighter 2 Choice is: " + fighter2Choice);
            }
        }
        /* Draw: just keep going until someone wins. Don't replace the weapons on the table. */
        else if (fighter1Choice ==  fighter2Choice) {
            fighter1Choice = Choice.None;
            fighter2Choice = Choice.None;
            Invoke("StartPick", 0);
            return;
        }
        else {
            throw new Exception("Something went wrong with RPS. Fighter 1 Choice is: " + fighter1Choice + ". Fighter 2 Choice is: " + fighter2Choice);
        }

        /* If we get here, a winner has been chosen */
        ui.TextCenterWeapon(winner);
        exhib.StopExhibit();
        
        // set non-winner weapons to be inactive
        // and remove references
        // do NOT destroy them because Waves needs them
        if (rockWeapon != winnerWeapon) {
            // rockWeapon.SetActive(false);
            Destroy(rockWeapon);
            rockWeapon = null;
        }
        if (paperWeapon != winnerWeapon) {
            // paperWeapon.SetActive(false);
            Destroy(paperWeapon);
            paperWeapon = null;
        }
        if (scissorsWeapon != winnerWeapon) {
            // scissorsWeapon.SetActive(false);
            Destroy(scissorsWeapon);
            scissorsWeapon = null;
        }
            
        fighter1Choice = Choice.None;
        fighter2Choice = Choice.None;

        winner.GetComponent<Fighter>().ReceivePrize(winnerWeapon);

        Invoke("StartFight", timeFromRPSToFight);
    }

    private void EndGame(GameObject winner) {
        ui.DisplayEndGameInfo(winner);
        Invoke("EndScene", 2);
    }

    private void EndScene() {
        SceneManager.LoadScene("titleArea");
    }

//== Player and Table Communication ==============//

    public void NotifyPlayerReady(GameObject player)
    {
        // for some reason, one player calls this method twice
        // check for this
        if (player == fighter1 || player == fighter2) {
            return;
        }

        if (player.GetComponent<Fighter>() != null && fighter1 == null) {
            fighter1 = player;
            fighter1.gameObject.name = "Fighter 1";
            fighter1.GetComponent<Fighter>().defaultWeapon = waves.defaltWeapon;
            if (debug) StartRPS();
        }
        else if (player.GetComponent<Fighter>() != null && fighter2 == null) {
            fighter2 = player;
            fighter2.gameObject.name = "Fighter 2";
            fighter2.GetComponent<Fighter>().defaultWeapon = waves.defaltWeapon;
            if (!debug) StartRPS();
        }
        // else {
        //     throw new Exception("Trying to assign another player when player references is full.");
        // }
    }

    /* Returns a Vector3 relative to Table's position. */
    public Vector3 RequestStartPosition(GameObject fighter) {
        if (fighter == fighter1) {
            return fighter1StartPos + transform.position;
        }
        else if (fighter == fighter2) {
            return fighter2StartPos + transform.position;
        }
        else {
            throw new Exception("Cannot return start position for non-referenced fighter.");
        }
    }

    public void SetChoice(GameObject player, Choice choice) {
        if(player == fighter1) {
            fighter1Choice = choice;
        }
        else if (player == fighter2)
        {
            fighter2Choice = choice;
        }
    }

    public void NotifyPlayerDied(GameObject killee) {
        // prevent from calling twice (like when both players die)
        if (fightOver) return;

        fightOver = true;

        GameObject killer;
        if (killee == fighter1) {
            killer = fighter2;
        }
        else if (killee == fighter2) {
            killer = fighter1;
        }
        else {
            throw new Exception("Cannot notify death for non-referenced fighter.");
        }

        /* Display round info and increase tally */
        ui.on = true;
        ui.TextCenterPoint(killer);

        if (ui.numPoints1 >= pointsToWin) {
            EndGame(killer);
        }
        else {
            Invoke("StartRPS", timeFromDeathToRPS);
        }
    }
}
