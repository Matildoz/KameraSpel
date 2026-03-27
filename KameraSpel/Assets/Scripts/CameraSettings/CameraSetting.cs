using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "CameraSetting", menuName = "Scriptable Objects/CameraSetting")]
public class CameraSetting : ScriptableObject
{
    public CurrentCamera currentCamera;
    [Range(0f, 1f)]
    public float aberrationStrength;
    public float minimumZoom;
    public float maximumZoom;
    public int focalLength;
    public int focusDistance;
    public VolumeProfile volumeProfile;
    public DepthOfField dof;

} 
