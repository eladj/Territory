using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Territory{

// We assume the left tile is always in the origin location.
//   - Rot0: the right tile is on the right
//   - Rot90: right tile is up
//   - Rot180: right tile is left
//   - Rot270: right tile is down
public enum RotationType {Rot0=0, Rot90=1, Rot180=2, Rot270=3};

public class DominoTile : MonoBehaviour
{
    public Tile leftTile;
    public Tile rightTile;
    public int sortIndex;
    public RotationType rotationType = RotationType.Rot0;

    public void Set(Tile _leftTile, Tile _rightTile, int _sortIndex, RotationType _rotationType=RotationType.Rot0){
        leftTile = _leftTile;
        rightTile = _rightTile;
        sortIndex = _sortIndex;
        rotationType = _rotationType;
    }

  public override string ToString()
  {
    return string.Format("[#{0}, Rot={1}]", sortIndex, rotationType.ToString());
  }

  public override bool Equals(object obj)
  {
    return ((obj != null) && (obj is DominoTile) && (this.sortIndex == ((DominoTile)obj).sortIndex));
  }

  public override int GetHashCode()
  {
    return this.sortIndex.GetHashCode();
  }

  public int CompareTo(DominoTile other)
  {
    if (this.sortIndex == other.sortIndex)
      return 0;
    else if (this.sortIndex < other.sortIndex)
      return -1;
    else
      return 1; // this.other > other.other
  }  
}

}