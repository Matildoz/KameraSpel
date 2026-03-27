using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [SerializeField] Image photoDisplayArea;
    Texture2D screenCapture;
    [SerializeField] int imageRes = 100;
    [SerializeField] int screenToCaptureX;
    [SerializeField] int screenToCaptureY;  
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakePhoto()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height);
        StartCoroutine(CapturePhoto());
    }
    public IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();
        Rect areaToRead = new Rect(0,0, Screen.width, Screen.height);
       
        screenCapture.ReadPixels(areaToRead, 0, 0, false);
        screenCapture.Apply();
        SavePhoto();
        
    }
    void SavePhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f,0.0f,Screen.width,Screen.height),new Vector2(0.5f,0.5f),imageRes);
        photoDisplayArea.sprite = photoSprite;
        byte[] byteArray = screenCapture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "Screenshot", byteArray);
    }
 
}
