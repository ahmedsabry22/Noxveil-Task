using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{

    public GameObject panel_searching_players;
    public ushort max_players_in_room = 2;




    public bool IsGameReady
    {
        get
        {
            return ( Instance != null && joined_players.Count == max_players_in_room && runner.IsRunning && is_game_scene_ready );
        }
    }


    public bool IsGameOver { get; private set; }


    [HideInInspector] public NetworkRunner runner;


    [HideInInspector] public List<PlayerRef> joined_players = new ();


    [SerializeField] private NicknameHolder nickname_holder;
    [SerializeField] private TextMeshProUGUI text_players_joined_number;


    // we store players by their rank in this list after the game is finished
    private List<PlayerData> players_ranked = new ();

    private bool is_game_scene_ready;


    private float match_start_time;
    private float match_end_time;


    public static NetworkManager Instance;


    private void Awake ()
    {
        if ( Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad ( gameObject );
        }
        else
        {
            Destroy ( gameObject );
        }
    }


    public async void StartMatch ()
    {
        panel_searching_players.SetActive ( true );

        runner = gameObject.AddComponent<NetworkRunner> ();
        runner.ProvideInput = true;
        runner.AddCallbacks ( this );

        var scene_manager = runner.gameObject.AddComponent<NetworkSceneManagerDefault> ();

        nickname_holder.SetNickname ();

        var result = await runner.StartGame ( new StartGameArgs ()
        {
            GameMode = GameMode.AutoHostOrClient ,
            SceneManager = scene_manager ,
            Scene = SceneRef.FromIndex ( 0 ) ,
        } );

        if ( !result.Ok )
        {
            Debug.LogError ( $"StartGame failed: {result.ShutdownReason}" );
        }
    }


    public void StartMatchTimer ()
    {
        match_start_time = Time.time;
    }


    public void EndMatch ( string winner )
    {
        IsGameOver = true;
        match_end_time = Time.time;
        players_ranked = PlayerData.AllPlayersData.OrderByDescending ( p => p.Score ).ToList ();

        var players_ranked_info = new List<LastMatchDetails.PlayerInfo> ();

        for ( ushort i = 0 ; i < players_ranked.Count ; i++ )
        {
            players_ranked_info.Add ( new LastMatchDetails.PlayerInfo ()
            {
                Nickname = players_ranked [ i ].NickName.ToString () ,
                Rank = ( ushort ) ( i + 1 ) ,
                Score = ( ushort ) players_ranked [ i ].Score
            } );
        }

        GameSceneReferences.Instance.Last_Match_Details.WriteMatchInfoToFile ( runner.SessionInfo.Name , max_players_in_room , winner , ( int ) ( match_end_time - match_start_time ) + " seconds" , players_ranked_info );
    }


    public void OnPlayerJoined ( NetworkRunner runner , PlayerRef player )
    {
        text_players_joined_number.text = $" {runner.ActivePlayers.Count ()}/{max_players_in_room}";

        if ( !joined_players.Contains ( player ) )
        {
            joined_players.Add ( player );
        }

        if ( runner.IsServer && runner.ActivePlayers.Count () == 2 )
        {
            if ( runner.IsSceneAuthority )
            {
                runner.LoadScene ( SceneRef.FromIndex ( 1 ) , LoadSceneMode.Single );
            }
        }
        else
        {
            if ( panel_searching_players != null )
            {
                panel_searching_players.SetActive ( true );
            }
        }
    }


    public void OnSceneLoadStart ( NetworkRunner runner )
    {
    }


    public void OnSceneLoadDone ( NetworkRunner runner )
    {
        is_game_scene_ready = SceneManager.GetActiveScene ().name == "1 - Game";
    }


    public void OnInput ( NetworkRunner runner , NetworkInput input )
    {
        PlayerInputData data = new PlayerInputData ();

        data.MovementDirection = new Vector2 ( Input.GetAxis ( "Horizontal" ) , Input.GetAxis ( "Vertical" ) );

        if ( GameSceneReferences.Instance != null )
        {
            data.CameraRotation = GameSceneReferences.Instance.Player_Camera.transform.rotation;
        }

        input.Set ( data );
    }


    public void OnConnectedToServer ( NetworkRunner runner ) { }
    public void OnDisconnectedFromServer ( NetworkRunner runner ) { }
    public void OnShutdown ( NetworkRunner runner , ShutdownReason shutdownReason ) { }
    public void OnConnectRequest ( NetworkRunner runner , NetworkRunnerCallbackArgs.ConnectRequest request , byte [] token ) { }
    public void OnConnectFailed ( NetworkRunner runner , NetAddress remoteAddress , NetConnectFailedReason reason ) { }
    public void OnInputMissing ( NetworkRunner runner , PlayerRef player , NetworkInput input ) { }
    public void OnReliableDataReceived ( NetworkRunner runner , PlayerRef player , ArraySegment<byte> data ) { }
    public void OnUserSimulationMessage ( NetworkRunner runner , SimulationMessagePtr message ) { }
    public void OnSessionListUpdated ( NetworkRunner runner , List<SessionInfo> sessionList ) { }
    public void OnHostMigration ( NetworkRunner runner , HostMigrationToken hostMigrationToken ) { }
    public void OnObjectExitAOI ( NetworkRunner runner , NetworkObject obj , PlayerRef player ) { }
    public void OnObjectEnterAOI ( NetworkRunner runner , NetworkObject obj , PlayerRef player ) { }
    public void OnPlayerLeft ( NetworkRunner runner , PlayerRef player ) { }
    public void OnDisconnectedFromServer ( NetworkRunner runner , NetDisconnectReason reason ) { }
    public void OnReliableDataReceived ( NetworkRunner runner , PlayerRef player , ReliableKey key , ArraySegment<byte> data ) { }
    public void OnReliableDataProgress ( NetworkRunner runner , PlayerRef player , ReliableKey key , float progress ) { }
    public void OnCustomAuthenticationResponse ( NetworkRunner runner , Dictionary<string , object> data ) { }
}
