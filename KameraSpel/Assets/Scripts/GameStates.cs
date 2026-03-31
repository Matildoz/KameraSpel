using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStates : MonoBehaviour
{
    
    public Camera camera;
    [SerializeField] Image cameraOverlay;
    [SerializeField] Player player;
    public enum PlayStates {walking,shooting}
    public PlayStates currentState;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTurnOnOffCamera(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (currentState == PlayStates.walking)
            {
                TurnOnCamera();
            }
            else
            {
                TurnOffCamera();
            }
        }
        
    }
     void TurnOnCamera()
     {
        currentState = PlayStates.shooting;
        cameraOverlay.enabled = true;
        camera.enabled = true;
     }
    void TurnOffCamera()
    {
        currentState = PlayStates.walking;
        cameraOverlay.enabled = false;
     
    }
}
