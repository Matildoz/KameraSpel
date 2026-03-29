using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [SerializeField] Image photoDisplayArea;
    Sprite photo;
    Texture2D screenCapture;
    [SerializeField] List<Sprite> photos;
    [SerializeField] List<Image> images;
    [SerializeField] int imageRes = 100;
    [SerializeField] int screenToCaptureX;
    [SerializeField] int screenToCaptureY;
    int photosTaken;
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
        photosTaken++;
        screenCapture.ReadPixels(areaToRead, 0, 0, false);
        screenCapture.Apply();
        TextureToPhoto(screenCapture);
        SavePhoto();
        
    }
    void SavePhoto()
    {
        
      
        byte[] byteArray = screenCapture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "Screenshot", byteArray);
    }
    public Sprite TextureToPhoto(Texture2D photoCapture)
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, Screen.width, Screen.height), new Vector2(0.5f, 0.5f), imageRes);
        photos.Add(photoSprite);
        photoDisplayArea.sprite = photoSprite;
        return photoSprite;
    }
    
    public void AddPhotoToGallery(Sprite photo)
    {
        photos.Add(photo);
        images[0].sprite = photo;
    }
}
