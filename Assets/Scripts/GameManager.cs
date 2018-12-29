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

    private List<DominoTile> previousTurnCards;
    private List<DominoTile> nextTurnCards;
    private List<Player> players;
    // private List<PlayerColor> playersPlayOrder;
    
    // Flag for the to state when all players have finished to select cards
    private bool isTurnFinished;
    private bool isFirstTurn;

    private List<Move> moves;
    private List<Move> previousMoves;
    private List<Move> nextMoves;

    // Players map location on the board
    private List<Vector3> playersMapPosition;

    // The 4 cards selection position
    private Vector3 previousCardsPosition;
    private Vector3 nextCardsPosition;

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
        previousCardsPosition = new Vector3(-1.5f, 0f, 0f);
        nextCardsPosition = new Vector3(1.5f, 0f, 0f);
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect mouse button click
        if (Input.GetMouseButtonDown (0)) {    
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                if(hit.collider.tag == "DominoTile") {                         
                    DominoTile dtile = hit.collider.GetComponent<DominoTile>();
                    Debug.Log("Mouse clicked domino tile: " + dtile.ToString());

                    if (nextTurnCards.Contains(dtile)){
                        Debug.Log("DominoTile in nextTurnCards");
                        // Mark that this domino tile is the next for this player
                        int card_ind = nextTurnCards.IndexOf(dtile);

                        Debug.Log("Selected card index: " + card_ind.ToString());
                        Debug.Log("nextMoves Count = " + nextMoves.Count.ToString());
                        Debug.Log("isFirstTurn = " + isFirstTurn.ToString());
                        nextMoves[card_ind].playerColor = previousMoves[0].playerColor;

                        if (!isFirstTurn){
                            // Move previous domino tile to player - TODO !!!
                            previousMoves[0].rotationType = RotationType.Rot0;
                            previousMoves[0].position.Set(col: 1, row: 1, true);
                            MakeMove(previousMoves[0]);
                            
                            // Add move to full moves list
                            moves.Add(previousMoves[0]);
                        }

                        // Remove the previous move
                        previousMoves.RemoveAt(0);

                        // Check if all players have selected their cards
                        if (previousMoves.Count == 0){
                            isTurnFinished = true;
                        }
                    }
                }
            }
        }

        if (isTurnFinished){
            AdvanceTurn();
        }
    }

    void AdvanceTurn(){
        // Move next turn cards and moves to previous
        if (!isFirstTurn){
            previousMoves = nextMoves;
            previousTurnCards = nextTurnCards;

            // Place previous cards on position
            for (int n=0; n < nextTurnCards.Count; n++){
                previousTurnCards[n].gameObject.transform.localPosition = previousCardsPosition + new Vector3(0, -1.2f*n, 0f);
            }
        }

        // Draw new cards from deck
        nextTurnCards.Clear();
        nextTurnCards = deck.DrawCards(numOfPlayers);

        // Sort them from small to large
        nextTurnCards.Sort((x, y) => x.CompareTo(y));
        
        // Place cards on center
        for (int n=0; n < nextTurnCards.Count; n++){
            nextTurnCards[n].gameObject.SetActive(true);
            nextTurnCards[n].gameObject.transform.parent = transform;
            nextTurnCards[n].gameObject.transform.localPosition = nextCardsPosition + new Vector3(0, -1.2f*n, 0f);
        }

        // Initialize next moves
        nextMoves.Clear();
        for (int n=0; n < nextTurnCards.Count; n++){
            Move tmp = new Move();
            tmp.dominoTileSortIndex = nextTurnCards[n].sortIndex;
            nextMoves.Add(tmp);
        }

        // Re-intialize the flag to state that the turn has not finished
        isTurnFinished = false;

        // Remove the flag which indicates the first turn
        if (isFirstTurn){
            isFirstTurn = false;
        }
    }

    private void InitGame(){
        // Initialize all lists
        previousTurnCards = new List<DominoTile>();
        nextTurnCards = new List<DominoTile>();
        players = new List<Player>();
        previousMoves = new List<Move>();
        nextMoves = new List<Move>();

        // Initialize the players GameObjects and properties
        for (int player_number=0; player_number < numOfPlayers; player_number++){
            GameObject go_Player = Instantiate(playerPrefab) as GameObject;
            Player player = go_Player.GetComponent<Player>();
            go_Player.name = "Player" + player_number.ToString();
            player.playerColor = (PlayerColor) player_number;
            player.score = 0;
            go_Player.transform.position = playersMapPosition[player_number];
            players.Add(player);
            
            // Initialize previous moves for the first turn
            Move tmp_move = new Move();
            tmp_move.playerColor = (PlayerColor) player_number;
            previousMoves.Add(tmp_move);
        }

        // Initiate the first turn
        AdvanceTurn();

        // Flag that we are in the first turn
        isFirstTurn = true;
    }

    private void MakeMove(Move move){
        // Find the DominoTile index in the previous cards list
        int prev_ind = previousTurnCards.FindIndex(p => p.sortIndex == move.dominoTileSortIndex);

        // Find player index
        int player_ind = players.FindIndex(p => p.playerColor == move.playerColor);

        // Move the domino tile GameObject to the relevant player
        DominoTile tile_obj = previousTurnCards[prev_ind];
        tile_obj.gameObject.transform.parent = players[player_ind].transform;

        // Update the Board object of the player - TODO !!

        // TODO - move tile graphics to place !!!
        tile_obj.gameObject.transform.localPosition = new Vector3(-1f, -1f, 0f);
    }
}

}