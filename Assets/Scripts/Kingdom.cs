using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using BoardType = System.UInt32;

public class Kingdom : MonoBehaviour
{
    // We keep a dual representation of the game map to allow both
    // a fast computation of the score and of the valid moves.
    private List<TilesGroup> groups;    // For fast compuatation of score
    private Dictionary<TerrainType, BoardType> map_by_terrain;  // For fast checking of valid moves

    // Return a dictionary with each terrain map and its BitBoard representation
    public Dictionary<TerrainType, BoardType> GetMapByTerrain(){
        Dictionary<TerrainType, BoardType> dict = new Dictionary<TerrainType, BoardType>();
        // Initialize all maps to zero
        foreach(TerrainType terrainType in Enum.GetValues(typeof(TerrainType))){
            dict.Add(terrainType, 0);
        }
        // Aggregate over all groups
        for (int i=0; i < groups.Count; i++){
            dict[groups[i].terrainType] |= groups[i].position;
        }
        return dict;
    }

    public bool IsMoveValid(DominoTile domino_tile, int tile_ind){
        for (int i=0; i < groups.Count; i++){
            TilesGroup group = groups[i];
        }
        // TODO
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
