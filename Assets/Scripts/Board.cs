using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BoardType = System.UInt32;

public class Board : ScriptableObject 
{
    public const int SIZE = 25;  // Number of tiles
    public const BoardType VALID_BIT_MASK = 0x1ffffff;  // The valid bits (25 bits out of 32)

    private BoardType Desert;
    private BoardType Sea;
    private BoardType Forest;
    private BoardType Grass;
    private BoardType Mud;
    private BoardType Mine;
    private BoardType Castle;
    private BoardType Crowns1;
    private BoardType Crowns2;
    private BoardType Crowns3;

    public int ComputeScore(){
        return 0;
    }
}