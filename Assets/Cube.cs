using UnityEngine;
using Fusion;
using UnityEngine.InputSystem.XR;
using Fusion.Addons.Physics;
using UnityEngine.Windows;


public class Cube : NetworkBehaviour
{
    public float moveSpeed = 10;
    public float turnSpeed = 10;

    // Reference to the NetworkRigidbody component, which handles physics synchronization
    private NetworkRigidbody3D rigid_body;


    // Awake is called when the script instance is being loaded.
    private void Awake ()
    {
        // Get the NetworkRigidbody component from this GameObject.
        rigid_body = GetComponent<NetworkRigidbody3D> ();
        if ( rigid_body == null )
        {
            Debug.LogError ( "CarController requires a NetworkRigidbody component!" , this );
        }
    }


    // FixedUpdateNetwork is Fusion's deterministic update loop.
    // All networked physics and state changes should happen here.
    public override void FixedUpdateNetwork ()
    {
        // Try to get the input for the current tick.
        // On the server, this will be the actual input sent by the client.
        // On the client, this will be the predicted input.
        if ( GetInput ( out PlayerInputData inputData ) )
        {


            if ( HasInputAuthority || Runner.IsServer )
            {
                Debug.Log ( "I'm here " + inputData.MovementDirection );

                Vector3 moveVector = new Vector3 ( inputData.MovementDirection.x , 0 , inputData.MovementDirection.y );

                rigid_body.Rigidbody.MovePosition ( transform.position + moveVector * Runner.DeltaTime * moveSpeed );


                //Vector3 currentVelocity = _networkRigidbody.Rigidbody.linearVelocity;
                //Vector3 currentAngularVelocity = _networkRigidbody.Rigidbody.angularVelocity;

                //// --- Apply Movement ---
                //Vector3 desiredForwardVelocity = Vector3.zero;
                //if ( inputData.MovementDirectionNormalized.y > 0 )
                //{
                //    desiredForwardVelocity = transform.forward * moveSpeed;
                //}
                //else if ( inputData.MovementDirectionNormalized.y < 0 )
                //{
                //    desiredForwardVelocity = -transform.forward * moveSpeed;
                //}

                //// Set the horizontal velocity directly. Keep vertical velocity (gravity) if applicable.
                //_networkRigidbody.Rigidbody.linearVelocity = new Vector3 ( desiredForwardVelocity.x , currentVelocity.y , desiredForwardVelocity.z );

                //// --- Apply Turning ---
                //float desiredAngularVelocityY = 0f;
                //if ( inputData.MovementDirectionNormalized.x < 0 )
                //{
                //    desiredAngularVelocityY = -turnSpeed * Mathf.Deg2Rad; // Convert degrees per second to radians per second
                //}
                //else if ( inputData.MovementDirectionNormalized.x > 0 )
                //{
                //    desiredAngularVelocityY = turnSpeed * Mathf.Deg2Rad; // Convert degrees per second to radians per second
                //}

                //// Set the angular velocity directly.
                //_networkRigidbody.Rigidbody.angularVelocity = new Vector3 ( currentAngularVelocity.x , desiredAngularVelocityY , currentAngularVelocity.z );

                //// If no movement or turn input, ensure velocities are zeroed out
                //// This is crucial for immediate stopping.
                //if ( inputData.MovementDirectionNormalized.y == 0 && inputData.MovementDirectionNormalized.x == 0 )
                //{
                //    _networkRigidbody.Rigidbody.linearVelocity = new Vector3 ( 0 , currentVelocity.y , 0 ); // Keep gravity effect
                //    _networkRigidbody.Rigidbody.angularVelocity = Vector3.zero;
                //}
            }
        }
    }
}