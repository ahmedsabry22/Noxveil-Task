using UnityEngine;


public class LoadingIcon : MonoBehaviour
{
    public float rotation_speed;


    private void Update ()
    {
        transform.Rotate ( 0 , 0 , -rotation_speed * Time.deltaTime );
    }
}
