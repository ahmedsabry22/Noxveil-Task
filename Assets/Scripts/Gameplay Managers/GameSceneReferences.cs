using UnityEngine;


public class GameSceneReferences : MonoBehaviour
{
    [Header ( "Coins Manager" )]
    public CoinsManager Coins_Manager;


    [Header ( "Player Camera" )]
    public CameraFollowingPlayer Player_Camera;


    [Header ( "Final Score Board" )]
    public FinalScoreBoard Final_Score_Board;


    [Header ( "Last Match Details" )]
    public LastMatchDetails Last_Match_Details;


    [Header ( "Players Spawner" )]
    public PlayersSpawner Players_Spawner;


    [Header ( "Player Stats UI Manager" )]
    public PlayerStatsUIManager Player_Stats_UI_Manager;


    public static GameSceneReferences Instance;


    private void Awake ()
    {
        if ( Instance == null )
        {
            Instance = this;
        }
        else
        {
            Destroy ( gameObject );
        }
    }


}