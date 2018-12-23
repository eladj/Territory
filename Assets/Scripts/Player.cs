using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor {Red, Green, Blue, Yellow};

public class Player : MonoBehaviour
{
    public bool isAI = false;
    public PlayerColor playerColor;
    public int score;
}
