using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType {Desert, Sea, Forest, Grass, Mud, Mine};

public class Tile : MonoBehaviour
{
    public TerrainType terrainType;
    public int numCrowns;

    public void Set(TerrainType _terrainType, int _numCrowns){
        terrainType = _terrainType;
        numCrowns = _numCrowns;
    }
}
