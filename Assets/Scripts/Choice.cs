using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice
{
    public enum choice {
        Rock,
        Paper,
        Scissors,
        None
    }

    public static readonly Choice Rock = new Choice(choice.Rock);
    public static readonly Choice Paper = new Choice(choice.Paper);
    public static readonly Choice Scissors = new Choice(choice.Scissors);
    public static readonly Choice None = new Choice(choice.None);

    private choice c = choice.None;

    public Choice(choice c) {
        this.c = c;
    }

    public override bool Equals(object o) {
        if (o is Choice) {
            if (this == ((Choice)o)) {
                return true;
            }
            return false;
        }
        throw new Exception();
    }

    public static bool operator ==(Choice a, Choice b) {
        if (a.c == b.c) {
            return true;
        }
        return false;
    }

    public static bool operator !=(Choice a, Choice b) {
        return !(a == b);
    }

    public static bool operator >(Choice a, Choice b) {
        if (a == None) {
            return false;
        }
        else if (b == None) {
            return true;
        }
        else if (a == Rock && b == Scissors) {
            return true;
        }
        else if (a == Paper && b == Rock) {
            return true;
        }
        else if (a == Scissors && b == Paper) {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool operator <(Choice a, Choice b) {
        if (a == Rock && b == Paper) {
            return true;
        }
        else if (a == Paper && b == Scissors) {
            return true;
        }
        else if (a == Scissors && b == Rock) {
            return true;
        }
        else if (a == None && b != None) {
            return true;
        }
        else {
            return false;
        }
    }

    public static Choice Random() {
        int r = (int)UnityEngine.Random.Range(0f, 3f);
        if (r == 0) {
            return Rock;
        }
        else if (r == 1) {
            return Paper;
        }
        else if (r == 2) {
            return Scissors;
        }
        else {
            return None;
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        if (c == Rock.c) {
            return "Rock";
        }
        else if (c == Paper.c) {
            return "Paper";
        }
        else if (c == Scissors.c) {
            return "Scissors";
        }
        else {
            return "None";
        }
    }
}
