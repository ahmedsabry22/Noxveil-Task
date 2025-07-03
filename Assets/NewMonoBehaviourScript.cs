using Fusion;
using UnityEngine;
using static Unity.Collections.Unicode;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 10;

    private CharacterController controller;
    private Vector3 currentMovement;


    private void Awake ()
    {
        controller = GetComponent<CharacterController> ();
    }


    public void Update ()
    {
        float vert = Input.GetAxis ( "Horizontal" );
        Vector3 inputDirection = new Vector3 ( 0 , 0 , vert );
        //Vector3 worldDirection = transform.TransformDirection ( inputDirection );
        Vector3 worldDirection = transform.TransformDirection ( inputDirection ).normalized;
        //worldDirection.Normalize ();

        currentMovement.x = 0;
        currentMovement.y = 0;
        currentMovement.z = worldDirection.z;        // * moveSpeed;

        //controller.Move ( currentMovement * moveSpeed * Runner.DeltaTime );
        controller.Move ( worldDirection * moveSpeed * Time.deltaTime );
    }
}