using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine.EventSystems;


public class PlayerMovement : NetworkBehaviour
{
    public float move_speed = 5f;
    public float rotation_smoothness;


    [Networked] private bool IsRunning { get; set; }


    private NetworkRigidbody3D rigid_body;
    private Animator animator;


    private void Awake ()
    {
        animator = GetComponent<Animator> ();
        rigid_body = GetComponent<NetworkRigidbody3D> ();
    }


    public override void Spawned ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        animator = GetComponent<Animator> ();


        if ( HasInputAuthority )
        {
            GameSceneReferences.Instance.Player_Camera.player_to_follow = this.transform;
        }
    }


    public override void FixedUpdateNetwork ()
    {
        if ( NetworkManager.Instance.IsGameOver )
            return;

        if ( GetInput ( out PlayerInputData inputData ) )
        {
            if ( HasInputAuthority || Runner.IsServer )
            {
                IsRunning = inputData.MovementDirection.y > 0;

                Vector3 moveVector = new Vector3 ( 0 , 0 , inputData.MovementDirection.y );

                if ( inputData.MovementDirection.y > 0 )
                {
                    Vector3 targetPosition = transform.position + transform.forward * inputData.MovementDirection.y * move_speed * Runner.DeltaTime;
                    rigid_body.Rigidbody.MovePosition ( targetPosition );

                    rigid_body.Rigidbody.rotation = Quaternion.Lerp ( rigid_body.Rigidbody.rotation , inputData.CameraRotation , rotation_smoothness * Runner.DeltaTime );
                }
            }
        }
        else
        {
            if ( Object.HasInputAuthority )
            {
                IsRunning = false;
            }
        }
    }


    public override void Render ()
    {
        animator.SetBool ( "IsRunning" , IsRunning );
    }
}