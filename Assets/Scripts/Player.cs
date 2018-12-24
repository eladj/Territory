using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor {Red=0, Green=1, Blue=2, Yellow=3};

public class Player : MonoBehaviour
{
    public bool isAI = false;
    public PlayerColor playerColor;
    public int score;
}
