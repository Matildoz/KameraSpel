using UnityEngine;

public class GameStates : MonoBehaviour
{
    public Camera camera;
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

    public void TurnOnCamera()
    {
        currentState = PlayStates.shooting;
        camera.enabled = true;
    }
}
