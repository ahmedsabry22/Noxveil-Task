using Fusion;
using UnityEngine;


public struct PlayerInputData : INetworkInput
{

    public Vector3 MovementDirection;

    public Quaternion CameraRotation;

}