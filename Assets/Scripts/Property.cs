using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Territory {

public class Property : ScriptableObject {
    public TerrainType terrainType;
    public int numCrowns;
    public BitArray2D bitsMap = new BitArray2D(cols:Board.NUM_COLS_REPR, rows:Board.NUM_ROWS_REPR);

    // Get the score of current group
    public int GetScore(){
        return bitsMap.Sum() * numCrowns;
    }
}

}