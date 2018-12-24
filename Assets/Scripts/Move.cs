using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : ScriptableObject {
    // A move needs 16 bits to be stored
    // bit   0-5: Tile number (from 1 to 48)
    // bit   6-7: Rotation type (0, 90, 180, 270)
    // bit  8-13: Location (from 0 to 48, to support 5x5 and 7x7)
    // bit 14-15: Player number (0, 1, 2, 3)
    private System.UInt16 data;

    public int GetDominoTileIndex(){
        return (int) data & 0b111111;
    }

    public RotationType GetRotationType(){
        int tmp = (int) (data >> 6) & 0b11;
        RotationType rotationType = (RotationType)tmp;
        return rotationType;
    }

    public int GetLocation(){
        return (int) (data >> 8) & 0b111111;
    }    

    public PlayerColor GetPlayerColor(){
        int tmp = (int) (data >> 14) & 0b11;
        PlayerColor playerColor = (PlayerColor)tmp;
        return playerColor;
    }    

    public void SetDominoTileIndex(int tile_index){
        data &= 0b1111_1111_1100_0000;
        data |= (System.UInt16) (tile_index & 0b111111);
    }

    public void SetRotationType(RotationType rotation_type){
        data &= 0b1111_1111_0011_1111;
        data |= (System.UInt16) ((int)rotation_type << 6);
    }

    public void SetLocation(int location){
        data &= 0b1100_0000_1111_1111;
        data |= (System.UInt16) ((location & 0b111111) << 8);
    }    

    public void SetPlayerColor(PlayerColor player_color){
        data &= 0b0011_1111_1111_1111;
        data |= (System.UInt16) ((int)player_color << 14);
    }        
}