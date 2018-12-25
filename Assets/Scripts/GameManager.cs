using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Territory{

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;       // Static instance of GameManager which allows it to be accessed by any other script.
    public GameObject playerPrefab;

    private BoardManager boardScript;        // Store a reference to our BoardManager which will set up the maps of each player.
    private Deck deck;                       // Store a reference to our Deck which will set up the DominoTiles.
    private int numOfPlayers;

    // Players map location on the board
    private List<Vector3> playersMapLocation = new List<Vector3>();

    void Awake(){
        // Check if instance already exists
        if (instance == null)
            // if not, set instance to this
            instance = this;
        // If instance already exists and it's not this:
        else if (instance != this)
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
        // Get a component reference to the Deck and BoardManager scripts
        deck = GetComponent<Deck>();
        boardScript = GetComponent<BoardManager>();
        
        //Call the InitGame function to initialize the first level 
        // InitGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        numOfPlayers = 4;
        playersMapLocation.Add(new Vector3(0f, -10f, 0f));
        playersMapLocation.Add(new Vector3(-10f, 0f, 0f));
        playersMapLocation.Add(new Vector3(0f, 10f, 0f));
        playersMapLocation.Add(new Vector3(10f, 0f, 0f));
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitGame(){
        // Create new DominoTile
        for (int player_number=0; player_number < numOfPlayers; player_number++){
            GameObject go_Player = Instantiate(playerPrefab) as GameObject;
            Player player = go_Player.GetComponent<Player>();
            go_Player.name = "Player" + player_number.ToString();
            player.playerColor = (PlayerColor) player_number;
            player.score = 0;
            go_Player.transform.position = playersMapLocation[player_number];
        }
    }

    private void MakeMove(Move move){
        // Move the domino tile GameObject to the relevant player
        GameObject dominoTileGO = deck.GetDominoTileByIndex(move.dominoTileIndex);
        // dominoTileGO.transform.parent = 
        // move.dominoTileIndex
    }
}

}