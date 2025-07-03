using UnityEngine;
using Fusion;
using System.Collections.Generic;
using System.Collections;


public class PlayersSpawner : MonoBehaviour
{
    [SerializeField] private List<NetworkPrefabRef> player_prefabs;
    [SerializeField] private List<Transform> player_spawn_positions;


    public bool ArePlayersReady
    {
        get
        {
            foreach ( var player in spawned_prefabs_dictionary )
            {
                if ( !player.Value.GetComponent<PlayerData> ().DataReady )
                    return false;
            }

            return true;
        }
    }


    private Dictionary<PlayerRef , NetworkObject> spawned_prefabs_dictionary = new Dictionary<PlayerRef , NetworkObject> ();


    private IEnumerator Start ()
    {
        yield return new WaitUntil ( () => NetworkManager.Instance.IsGameReady );

        SpawnCharacters ();
    }


    public void SpawnCharacters ()
    {
        if ( !NetworkManager.Instance.runner.IsServer )
            return;

        var players = NetworkManager.Instance.joined_players;

        for ( int i = 0 ; i < players.Count ; i++ )
        {
            NetworkObject playerObj = NetworkManager.Instance.runner.Spawn ( player_prefabs [ i ] , player_spawn_positions [ i ].position , player_spawn_positions [ i ].rotation , players [ i ] );

            spawned_prefabs_dictionary.Add ( players [ i ] , playerObj );
        }
    }

}