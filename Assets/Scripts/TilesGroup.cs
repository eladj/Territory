using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BoardType = System.UInt32;

public class TilesGroup : ScriptableObject {
    // The 5x5 board is represented by the following Bitboard:
    //  20 21 22 23 24
    //  15 16 17 18 19
    //  10 11 12 13 14
    //  05 06 07 08 09
    //  00 01 02 03 04
    public TerrainType terrainType;
    public int numCrowns;
    public BoardType position;

    // Get the score of current group
    public int GetScore(){
        BoardType mask = position & Board.VALID_BIT_MASK;
        uint sum = 0;
        for (int i=0; i < Board.SIZE; i++){
            sum += (position >> 1) & 1;
        }
        return (int) sum * numCrowns;
    }
}