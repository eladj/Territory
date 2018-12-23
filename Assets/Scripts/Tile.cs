using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType {Desert, Sea, Forest, Grass, Mud, Mine, Castle};

public class Tile : MonoBehaviour
{
    public TerrainType terrainType;
    public int numCrowns;

    public void Set(TerrainType _terrainType, int _numCrowns){
        terrainType = _terrainType;
        numCrowns = _numCrowns;
        SetGraphics();
    }

    private void SetGraphics(){
        // Load a terrain sprite from Resources
        if (terrainType != TerrainType.Castle){
            Sprite terrain_sprite = Resources.Load<Sprite>("Sprites/" + terrainType.ToString()) as Sprite;

            // Get the child graphics GameObject
            GameObject go_terrain = transform.Find("TerrainVisual").gameObject;

            // Replace it's material with the loaded one
            go_terrain.GetComponent<SpriteRenderer>().sprite = terrain_sprite;

            // Put the crowns sprite
            if (numCrowns > 0){
                GameObject go_crowns = transform.Find("CrownsVisual").gameObject;
                Sprite crowns_sprite = Resources.Load<Sprite>("Sprites/Crowns" + numCrowns.ToString()) as Sprite;
                go_crowns.GetComponent<SpriteRenderer>().sprite = crowns_sprite;
            }
        } else {
            // TODO - Load the castle according to the player color, which we get from the Tile parent (if exist)
            // Other option is that the Player or Kingdom class will handle this drawing.
        }
    }
}
