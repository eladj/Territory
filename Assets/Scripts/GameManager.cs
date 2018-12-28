using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Territory{

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;       // Static instance of GameManager which allows it to be accessed by any other script.
    public GameObject playerPrefab;
    public GameObject DeckGO;

    private Deck deck;                       // Store a reference to our Deck which will set up the DominoTiles.
    private int numOfPlayers;

    // Players map location on the board
    private List<Vector3> playersMapPosition;

    // The 4 cards selection position
    private Vector3 cardsSelectionPosition;

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
        
        // Get a component reference to the Deck script
        deck = DeckGO.GetComponent<Deck>();
        
        //Call the InitGame function to initialize the first level 
        // InitGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        numOfPlayers = 4;
        playersMapPosition = new List<Vector3>();;
        playersMapPosition.Add(new Vector3(0f, -10f, 0f));
        playersMapPosition.Add(new Vector3(-10f, 0f, 0f));
        playersMapPosition.Add(new Vector3(0f, 10f, 0f));
        playersMapPosition.Add(new Vector3(10f, 0f, 0f));
        cardsSelectionPosition = new Vector3(0f, 0f, 0f);
        InitGame();
        PlayTurn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayTurn(){
        // Draw new cards from deck
        List<DominoTile> new_cards = deck.DrawCards(numOfPlayers);

        // Sort them from small to large
        new_cards.Sort((x, y) => x.CompareTo(y));
        
        // Place cards on center
        for (int n=0; n < new_cards.Count; n++){
            new_cards[n].gameObject.SetActive(true);
            new_cards[n].gameObject.transform.parent = transform;
            new_cards[n].gameObject.transform.localPosition = new Vector3(0, -2*n, 0f);
        }
    }

    private void InitGame(){
        // Create new DominoTile
        for (int player_number=0; player_number < numOfPlayers; player_number++){
            GameObject go_Player = Instantiate(playerPrefab) as GameObject;
            Player player = go_Player.GetComponent<Player>();
            go_Player.name = "Player" + player_number.ToString();
            player.playerColor = (PlayerColor) player_number;
            player.score = 0;
            go_Player.transform.position = playersMapPosition[player_number];
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