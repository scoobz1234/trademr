using System.Linq;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PhotoCamera : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    public GameObject Quad;
    Resolution cameraResolution;
    float ratio = 1.0f;
    AudioSource shutterSound;

    void Start()
    {
        shutterSound = GetComponent<AudioSource>() as AudioSource;
        Debug.Log("File path " + Application.persistentDataPath);
        cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        ratio = cameraResolution.height / cameraResolution.width;

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject)
        {
            photoCaptureObject = captureObject;
            Debug.Log("camera ready to take picture");
        });

    }
    public void StopCamera()
    {
        // Deactivate our camera
        photoCaptureObject?.StopPhotoModeAsync(OnStoppedPhotoMode);
    }
    public void TakePicture()
    {
        Debug.Log("Taking Photo");
        CameraParameters cameraParameters = new CameraParameters();
        cameraParameters.hologramOpacity = 0.0f;
        cameraParameters.cameraResolutionWidth = cameraResolution.width;
        cameraParameters.cameraResolutionHeight = cameraResolution.height;
        cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

        // Activate the camera
        if (photoCaptureObject != null)
        {
            Debug.Log("PhotoCaptureObject not null");
            if (shutterSound != null)
            {
                Debug.Log("Playing Sound");
                shutterSound.Play();
            }
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result)
            {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
            });
        }
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into our target texture
        var targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        // Create a gameobject that we can apply our texture to
        //GameObject quad = PhotoPrefab.transform.Find("Quad").gameObject;
        // Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        // quadRenderer.material.mainTexture = targetTexture;
        Quad.GetComponent<Renderer>().material.mainTexture = targetTexture;


    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown our photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }
}
