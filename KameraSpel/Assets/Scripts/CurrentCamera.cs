using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System;
using Unity.Cinemachine;
using UnityEngine.InputSystem;


public class CurrentCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] CameraSetting cameraSettings;
    [SerializeField] Volume volume;
    [SerializeField] Camera camera;
    [SerializeField] float zoomSpeed;
    VolumeProfile currentProfile;
    DepthOfField dof;
    [SerializeField] SetFocus setFocus;
    float currentAberrationValue;
    float zoomValue;
    Vector2 ZoomInput;
  
    ChromaticAberration aberration;
    [SerializeField] Slider aberrationSlider;
    [SerializeField] Slider zoomSlider;
    bool chromaticAberrationOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume.profile.TryGet(out aberration);
        volume.profile.TryGet(out dof);
        SetInitialSettings();
        
    }

    // Update is called once per frame
    void Update()
    {        
       // float zoom = currentZoom += zoomValue;

        //Zoom(zoom);

    
    }

  
    public void ChangeAperture(ClampedFloatParameter newAperture)
    {
       dof.aperture = newAperture;
    }

    public void ChangeAberration(float value)
    {
        aberration.intensity.value = value;
    }
    public void ZoomIn(InputAction.CallbackContext input)
    {
        zoomValue += input.ReadValue<float>();
       camera.focalLength = zoomValue;
        
        if (camera.focalLength > cameraSettings.maximumZoom)
        {
            camera.focalLength = cameraSettings.maximumZoom;
        }
        else if (camera.focalLength < cameraSettings.minimumZoom)
        {
            camera.focalLength = cameraSettings.minimumZoom;
        }
    }
    public void ZoomOut(InputAction.CallbackContext input)
    {
        zoomValue -= input.ReadValue<float>();
        camera.focalLength = zoomValue;
        if (camera.focalLength > cameraSettings.maximumZoom)
        {
            camera.focalLength = cameraSettings.maximumZoom;
        }
        else if (camera.focalLength < cameraSettings.minimumZoom)
        {
            camera.focalLength = cameraSettings.minimumZoom;
        }
    }
    public void Zoom(float value)
    {
        camera.focalLength = value;
        if(camera.focalLength > cameraSettings.maximumZoom)
        {
            camera.focalLength = cameraSettings.maximumZoom;
        }
        else if( camera.focalLength < cameraSettings.minimumZoom)
        {
            camera.focalLength = cameraSettings.minimumZoom;
        }
    }
    public void SetInitialSettings()
    {
        aberration.intensity.value = cameraSettings.aberrationStrength;
        zoomSlider.minValue = cameraSettings.minimumZoom;
        zoomSlider.maxValue = cameraSettings.maximumZoom;
        currentProfile = cameraSettings.volumeProfile;
        camera.focalLength = cameraSettings.focalLength;
        setFocus.focusDistance = cameraSettings.focusDistance;
     
    }
    public void ChromaticaberrationOnOff()
    {
        if (chromaticAberrationOn)
        {
            ChangeAberration(0);
           
            chromaticAberrationOn = false;
        }
        else
        {
            ChangeAberration(1);
           
            chromaticAberrationOn = true;
        }
    }
    
}
