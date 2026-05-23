using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [SerializeField] List<Sprite> photos;
    [SerializeField] Image[] images;
    [SerializeField] int imageRes = 100;

    [SerializeField] Image cameraFlash;
    [SerializeField] float flashDuration = 0.2f;
    [SerializeField] Camera camera;
    public RenderTexture renderCaptureTexture;
    Sprite photo;
    Texture2D screenCapture;
    int photosTaken;
    void Start()
    {
       camera.targetTexture = renderCaptureTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakePhoto(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            StartCoroutine(CapturePhotoWithRenderTexture());
        }
     
    }
   /* public IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();
       
        
        Rect areaToRead = new Rect(0,0, Screen.width, Screen.height);
        photosTaken++;
        screenCapture.ReadPixels(areaToRead, 0, 0, false);
        screenCapture.Apply();
        TextureToPhoto();
        AddPhotoToGallery();       
        
    }
   */
    public IEnumerator CameraFlash()
    {
        cameraFlash.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        cameraFlash.enabled = false;
    }
    public IEnumerator CapturePhotoWithRenderTexture()
    {
        yield return new WaitForEndOfFrame();
        screenCapture = new Texture2D(renderCaptureTexture.width, renderCaptureTexture.height,TextureFormat.RGBA64,false);
        RenderTexture.active =renderCaptureTexture;
        screenCapture.ReadPixels(new Rect(0,0,renderCaptureTexture.width, renderCaptureTexture.height), 0,0);
        screenCapture.Apply();
       
        StartCoroutine(CameraFlash());       
        TextureToPhoto();
        AddPhotoToGallery();       

    }

    public void TextureToPhoto()
    {
        photo = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, renderCaptureTexture.width,renderCaptureTexture.height), new Vector2(0.5f, 0.5f), imageRes);
     
        
    }
    
    public void AddPhotoToGallery()
    {
        photos.Add(photo);
        for(int i = 0; i < images.Length; i++)
        {
            if(images[i].sprite == null)
            {
                images[i].sprite = photo;
                return;
            }
        }
    }
    public void SellPhoto(int index)
    {
        Debug.Log(index);
        if (index >= photos.Count)
        {
            //You're trying to delete a photo that doesn't exist
            return;
        }
        photos.RemoveAt(index);
        images[index].sprite = null;
    }
}
