using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Territory {

public class Board : ScriptableObject 
{
    public const int NUM_ROWS = 5;
    public const int NUM_COLS = 5;
    public const int SIZE = NUM_ROWS * NUM_COLS;  // Number of tiles

    // We allocate twice as large board for the internal representation,
    // while we always keep the castle in the center
    public const int NUM_ROWS_REPR = NUM_ROWS * 2 - 1;
    public const int NUM_COLS_REPR = NUM_COLS * 2 - 1;
    public const int SIZE_REPR = NUM_ROWS_REPR * NUM_COLS_REPR;
    public const int CENTER_REPR_X = NUM_COLS - 1;
    public const int CENTER_REPR_Y = NUM_ROWS - 1;

    // We keep a dual representation of the game map to allow both
    // a fast computation of the score and of the valid moves.
    private List<Property> groups;    // For fast compuatation of score
    private Dictionary<TerrainType, BitArray2D> map_by_terrain;  // For fast checking of valid moves

    // Return a dictionary with each terrain map and its BitBoard representation
    public Dictionary<TerrainType, BitArray2D> GetMapByTerrain(){
        Dictionary<TerrainType, BitArray2D> dict = new Dictionary<TerrainType, BitArray2D>();
        // Initialize all maps to zero
        foreach(TerrainType terrainType in Enum.GetValues(typeof(TerrainType))){
            dict.Add(terrainType, new BitArray2D(cols: NUM_COLS_REPR, rows: NUM_ROWS_REPR));
        }
        // Aggregate over all groups
        for (int i=0; i < groups.Count; i++){
            dict[groups[i].terrainType].Or(groups[i].bitsMap);
        }
        return dict;
    }

    public BitArray2D GetOccupancyMap(){
        BitArray2D result = new BitArray2D(cols: NUM_COLS_REPR, rows: NUM_ROWS_REPR);
        foreach(var item in map_by_terrain){
            result.Or(item.Value);
        }
        return result;
    }

    public bool IsMoveValid(DominoTile domino_tile, int tile_ind){
        for (int i=0; i < groups.Count; i++){
            Property group = groups[i];
        }
        // TODO
        return false;
    }

    public BitArray2D GetOccupancyMapFromGroups(){
        BitArray2D result = new BitArray2D(cols: NUM_COLS_REPR, rows: NUM_ROWS_REPR);
        for (int i=0; i < groups.Count; i++){
            result.Or(groups[i].bitsMap);
        }
        return result;
    }
}

}