using UnityEngine;
using Fusion;


public class PlayerCollision : NetworkBehaviour
{
    private PlayerData player_data;


    private void Awake ()
    {
        player_data = GetComponent<PlayerData> ();
    }


    public override void Spawned ()
    {

    }


    private void OnTriggerEnter ( Collider other )
    {
        if ( NetworkManager.Instance.IsGameOver )
            return;

        if ( other.TryGetComponent ( out NetworkObject network_object ) )
        {
            var coin = network_object.GetComponent<Coin> ();

            if ( coin != null )
            {
                player_data.IncreaseScore ();
                GameSceneReferences.Instance.Coins_Manager.OnCoinDespawned ( coin , player_data );
            }
        }
    }


}