using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStates : MonoBehaviour
{
    public GameObject cameraObject;
    [SerializeField] Image cameraOverlay;
    [SerializeField] Player player;
    public enum PlayStates {walking,shooting}
    public PlayStates currentState;

    

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
        cameraObject.SetActive(true);
     }
    void TurnOffCamera()
    {
        currentState = PlayStates.walking;      
        cameraObject.SetActive(false);
    }
}
