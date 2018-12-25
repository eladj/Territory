using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace Territory{

public class Deck : MonoBehaviour
{
    public GameObject dominoTilePrefab;

    void Start(){
        GenerateDefaultDeck();
    }

    // Generate the default cards deck
    public void GenerateDefaultDeck(){
        List<TileRow> default_tiles = GetDefaultDominoTiles();
        for (int n=0; n < default_tiles.Count; n++){
            // Create new DominoTile
            GameObject go_DominoTile = Instantiate(dominoTilePrefab) as GameObject;
            go_DominoTile.name = "DominoTile " + (n+1).ToString();
            GameObject go_tileLeft = go_DominoTile.transform.Find("TileLeft").gameObject;
            GameObject go_tileRight = go_DominoTile.transform.Find("TileRight").gameObject;

            // Set the game properties of the tiles
            Tile left_tile = go_tileLeft.GetComponent<Tile>();
            Tile right_tile = go_tileRight.GetComponent<Tile>();
            left_tile.Set(default_tiles[n].left_type, default_tiles[n].left_crowns);
            right_tile.Set(default_tiles[n].right_type, default_tiles[n].right_crowns);

            // Create the DominoTile from the 2 tiles
            go_DominoTile.GetComponent<DominoTile>().Set(left_tile, right_tile, n+1);

            // Make the DominoTile child of Deck
            go_DominoTile.transform.parent = transform;
            go_DominoTile.transform.position = go_DominoTile.transform.position + new Vector3(2*Mathf.Floor(n / 12.0f) - 4, -(n % 12) + 4, 0);

            // Disable the new DominoTile
            // go_DominoTile.SetActive(false);
        }
    }

    public GameObject GetDominoTileByIndex(int index){
        return transform.Find("DominoTile" + index.ToString()).gameObject;
    }

    // Get the default types of domino tiles
    private List<TileRow> GetDefaultDominoTiles(){
        string[] guids = AssetDatabase.FindAssets("DominoTilesCSV", null);
        string csv_filename = AssetDatabase.GUIDToAssetPath(guids[0]);
        Debug.Log("DominoTiles CSV filename: " + csv_filename);
        List<TileRow> result = ReadCSV(csv_filename);
        return result;
    }

    // Reads CSV of tiles data
    private List<TileRow> ReadCSV(string filename){
        List<TileRow> result = new List<TileRow>();
        StreamReader strReader = new StreamReader(filename);
        bool endOfFile = false;
        while(!endOfFile){
            string data_string = strReader.ReadLine();
            if (data_string == null){
                endOfFile = true;
                break;
            }
            // Storing to variable
            var data_values = data_string.Split(',');
            // Debug.Log(data_values[0].ToString() + " " + data_values[1].ToString() + " " + data_values[2].ToString() + " " + data_values[3].ToString());
            TerrainType left_type = (TerrainType) System.Enum.Parse(typeof(TerrainType), data_values[0]);
            int left_crowns = int.Parse(data_values[1]);
            TerrainType right_type = (TerrainType) System.Enum.Parse(typeof(TerrainType), data_values[2]);
            int right_crowns = int.Parse(data_values[3]);
            TileRow row = new TileRow(left_type, left_crowns, right_type, right_crowns);
            result.Add(row);
        }
        return result;
    }

    // Helper class to read the tiles data from the CSV
    private class TileRow{
        public TerrainType left_type;
        public int left_crowns;
        public TerrainType right_type;
        public int right_crowns;

        public TileRow(TerrainType _left_type, int _left_crowns, TerrainType _right_type, int _right_crowns){
            left_type = _left_type;
            left_crowns = _left_crowns;
            right_type = _right_type;
            right_crowns = _right_crowns;
        }      
    }
}

}
