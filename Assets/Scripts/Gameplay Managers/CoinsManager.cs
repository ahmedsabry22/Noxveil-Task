using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class CoinsManager : NetworkBehaviour
{
    public int target_coins_to_win;


    [SerializeField] private NetworkPrefabRef coin_prefab;
    [SerializeField] private Transform [] spawn_positions;



    private readonly List<SpawnedCoinData> spawned_coins = new ();
    private readonly HashSet<int> occupied_spawn_indices = new ();


    public override void Spawned ()
    {
        if ( NetworkManager.Instance.runner.IsServer )
        {
            StartCoroutine ( SpawnCoinsCoroutine () );
        }
    }


    private IEnumerator SpawnCoinsCoroutine ()
    {
        while ( true )
        {
            // if all positions are occupied, just try again until there's a free position. this is to prevent coins from spawning in the same position.
            if ( occupied_spawn_indices.Count >= spawn_positions.Length )
            {
                yield return new WaitForSeconds ( 10 );
                continue;
            }


            // this is to make sure we don't spawn a coin in position already occupied.
            int randomIndex;
            do
            {

                randomIndex = Random.Range ( 0 , spawn_positions.Length );

            } while ( occupied_spawn_indices.Contains ( randomIndex ) );



            Transform spawnPoint = spawn_positions [ randomIndex ];

            // spawn a coin
            var coin = NetworkManager.Instance.runner.Spawn ( coin_prefab , Vector3.zero , Quaternion.identity , null ,
                ( runner , obj ) =>
                {
                    var coinScript = obj.GetComponent<Coin> ();
                    coinScript.SpawnPosition = spawnPoint.position;
                } )
                .GetComponent<Coin> ();

            spawned_coins.Add ( new SpawnedCoinData ( coin , randomIndex ) );
            occupied_spawn_indices.Add ( randomIndex );

            yield return new WaitForSeconds ( 10 );
        }
    }


    public void OnCoinDespawned ( Coin coin , PlayerData player )
    {
        var coin_data = spawned_coins.Find ( c => c.CoinInScene == coin );

        if ( coin_data.CoinInScene != null )
        {

            spawned_coins.Remove ( coin_data );
            occupied_spawn_indices.Remove ( coin_data.SpawnIndex );

            NetworkManager.Instance.runner.Despawn ( coin.Object );
        }
    }


    // --------------------------------------------------------------


    public struct SpawnedCoinData
    {
        public Coin CoinInScene;
        public int SpawnIndex;


        public SpawnedCoinData ( Coin coin , int index )
        {
            CoinInScene = coin;
            SpawnIndex = index;
        }
    }
}
