using UnityEngine;
using UnityEngine.InputSystem;


public class OpenNotebook : MonoBehaviour
{
    [SerializeField] GameObject photoBook;
    bool photobookOn = false;
    void ShowPhotos()
    {
        photoBook.SetActive(true);
        photobookOn = true;
    }
    void HidePhotos()
    {
        photoBook.SetActive(false);
        photobookOn = false;
    }

    public void OnPhotobookOnOff(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!photobookOn)
            {
                ShowPhotos();
            }
            else
            {
               HidePhotos();
            }
        }
    }
}
