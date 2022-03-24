using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class CustomVisionAnalyser : MonoBehaviour
{
    /// <summary>
    /// Unique instance of this class
    /// </summary>
    public static CustomVisionAnalyser Instance;

    /// <summary>
    /// Insert your prediction key here
    /// </summary>
    private string predictionKey = "f993cfda92fd4b568446d86a2d41754f";

    /// <summary>
    /// Insert your prediction endpoint here
    /// </summary>
    private string predictionEndpoint = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/c8ad6628-fe12-4161-9bee-ad94841a300d/detect/iterations/Iteration4/image";

    /// <summary>
    /// Bite array of the image to submit for analysis
    /// </summary>
    [HideInInspector] public byte[] imageBytes;

    /// <summary>
    /// Initializes this class
    /// </summary>
    private void Awake()
    {
        // Allows this instance to behave like a singleton
        Instance = this;
    }
    //check for note on 8.
    /// <summary>
    /// Call the Computer Vision Service to submit the image.
    /// </summary>
    public IEnumerator AnalyseLastImageCaptured(string imagePath)
    {
        Debug.Log("TRADEMR: " + "Analyzing...");

        WWWForm webForm = new WWWForm();

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(predictionEndpoint, webForm))
        {
            // Gets a byte array out of the saved image
            imageBytes = GetImageAsByteArray(imagePath);

            unityWebRequest.SetRequestHeader("Content-Type", "application/octet-stream");
            unityWebRequest.SetRequestHeader("Prediction-Key", predictionKey);

            // The upload handler will help uploading the byte array with the request
            unityWebRequest.uploadHandler = new UploadHandlerRaw(imageBytes);
            unityWebRequest.uploadHandler.contentType = "application/octet-stream";

            // The download handler will help receiving the analysis from Azure
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            // Send the request
            yield return unityWebRequest.SendWebRequest();

            string jsonResponse = unityWebRequest.downloadHandler.text;//try @() around value, check print

            Debug.Log("TRADEMR: " + "response: " + jsonResponse);

            // Create a texture. Texture size does not matter, since
            // LoadImage will replace with the incoming image size.
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(imageBytes);

            // The response will be in JSON format, therefore it needs to be deserialized
            AnalysisRootObject analysisRootObject = new AnalysisRootObject();
            analysisRootObject = JsonConvert.DeserializeObject<AnalysisRootObject>(jsonResponse.ToString());

            SceneOrganiser.Instance.FinaliseLabel(analysisRootObject);
    
        }
    }

    /// <summary>
    /// Returns the contents of the specified image file as a byte array.
    /// </summary>
    static byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);

        BinaryReader binaryReader = new BinaryReader(fileStream);

        return binaryReader.ReadBytes((int)fileStream.Length);
    }


}
