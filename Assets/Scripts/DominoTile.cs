using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoTile : MonoBehaviour
{
    public Tile leftTile;
    public Tile rightTile;
    public int sortIndex;

    public void Set(Tile _leftTile, Tile _rightTile, int _sortIndex){
        leftTile = _leftTile;
        rightTile = _rightTile;
        sortIndex = _sortIndex;
    }
}
