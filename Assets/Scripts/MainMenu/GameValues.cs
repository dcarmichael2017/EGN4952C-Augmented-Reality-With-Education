using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    public enum Difficulties { Easy, Medium, Hard };

    public static Difficulties currentDifficulty = Difficulties.Easy; //default easy

    public static string currentUser = "";

}
