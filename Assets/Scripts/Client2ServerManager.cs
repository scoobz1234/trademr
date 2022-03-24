using UnityEngine;
using UnityEngine.Networking;

public class Client2ServerManager : MonoBehaviour
{
    const string OTHER_URL = "http://3.95.141.30/api/predict";
    const string URL = "http://3.95.141.30/api/encoded-predict";
    public string payloadString;
    void Start()
    {

        UnityWebRequest webRequest = new UnityWebRequest(URL, "POST");
        string concatStr = "{\"image\": \"" + payloadString + "\"}";
        byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(concatStr);
        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(encodedPayload);
        webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("cache-control", "no-cache");
        UnityWebRequestAsyncOperation requestHandel = webRequest.SendWebRequest();
        requestHandel.completed += delegate (AsyncOperation pOperation)
        {
            Debug.Log(webRequest.responseCode);
            Debug.Log(webRequest.downloadHandler.text);
        };
    }

    /// <summary>
    /// 
    /// 
    /// {
    /// "image":"base64string"
    /// }
    /// </summary>
    /// <param name="tex"></param>


    void EncodeImageToBase64(Texture2D tex)
    {
        //all texture are now compressed textures in unity...
        //so we have to decompress the texture first.. and then we can convert to
        // a array of bytes, and then we can encode to png which gives us the base 64 encoding..



    }
}
