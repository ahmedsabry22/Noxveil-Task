using Fusion;
using UnityEngine;


public class Coin : NetworkBehaviour
{
    [Networked] public Vector3 SpawnPosition { get; set; }


    public override void Spawned ()
    {
        transform.position = SpawnPosition;
    }


    private void Update ()
    {
        transform.Rotate ( Vector3.up * 45 * Time.deltaTime );
    }

}