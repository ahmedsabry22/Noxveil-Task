using UnityEngine;


public class CameraFollowingPlayer : MonoBehaviour
{
    public float rotation_speed;
    public float follow_speed;


    [HideInInspector] public Transform player_to_follow;


    private float mouse_x;



    private void LateUpdate ()
    {
        if ( player_to_follow == null || NetworkManager.Instance.IsGameOver )
            return;

        mouse_x = Input.GetAxis ( "Mouse X" );
        transform.position = player_to_follow.position;

        transform.rotation *= Quaternion.Euler ( 0 , mouse_x * rotation_speed * Time.deltaTime , 0 );
    }
}