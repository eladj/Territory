using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Territory{

public class Move {
    public int dominoTileSortIndex;
    // {
    //     get{
    //         return dominoTileSortIndex;
    //     }
    //     set{
    //         if ((value >= 0) && (value < 48)){
    //             dominoTileSortIndex = value;
    //         } else {
    //             throw new System.ArgumentException("dominoTileIndex needs to be between 0 to 47");
    //         }
    //     }
    // }
    public RotationType rotationType;
    public PlayerColor playerColor;
    public BitArray2D position = new BitArray2D(cols: Board.NUM_COLS_REPR, rows: Board.NUM_ROWS_REPR);
}

}