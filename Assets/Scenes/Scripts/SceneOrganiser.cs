using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SceneOrganiser : MonoBehaviour
{
    /// <summary>
    /// Allows this class to behave like a singleton
    /// </summary>
    public static SceneOrganiser Instance;

    /// <summary>
    /// The cursor object attached to the Main Camera
    /// </summary>
    internal GameObject cursor;

    /// <summary>
    /// The label used to display the analysis on the objects in the real world
    /// </summary>
    public GameObject label;

    //parent label
    //public GameObject parentLabel;

    /// <summary>
    /// Reference to the last Label positioned
    /// </summary>
    internal Transform lastLabelPlaced = default;

    /// <summary>
    /// Reference to the last Label positioned
    /// </summary>
    internal TextMesh lastLabelPlacedText;

    /// <summary>
    /// Current threshold accepted for displaying the label
    /// Reduce this value to display the recognition more often
    /// </summary>
    internal float probabilityThreshold = 0.4f;//0.8f

    //For displaying label in TextMeshPro
    public TextMeshPro textDisplay;

    //adding for updated spatial awareness from old spatialmapping method
    internal int physicsLayer = 31; // The layer to use for spatial mapping collisions
    internal static int PhysicsRaycastMask; // Used by the GazeCursor as a property with the Raycast call

    /// <summary>
    /// Called on initialization
    /// </summary>
    private void Awake()
    {
        // Use this class instance as singleton
        Instance = this;

        // Add the ImageCapture class to this Gameobject
        gameObject.AddComponent<ImageCapture>();

        // Add the CustomVisionAnalyser class to this Gameobject
        gameObject.AddComponent<CustomVisionAnalyser>();

        // Add the CustomVisionObjects class to this Gameobject
        gameObject.AddComponent<CustomVisionObjects>();

    }

    /// <summary>
    /// Instantiate a Label in the appropriate location relative to the Main Camera.
    /// </summary>
    public void PlaceAnalysisLabel(string labelText)
    {
        lastLabelPlaced = Instantiate(label.transform, cursor.transform.position, transform.rotation); //former cursor location : new Vector3(0.3014f, 0.0452f, 0.396f)
        lastLabelPlacedText = lastLabelPlaced.GetComponent<TextMesh>();
        lastLabelPlacedText.text = labelText + "\nTMK: 01-1234";
        lastLabelPlaced.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
    }

    /// <summary>
    /// Set the Tags as Text of the last label created. 
    /// </summary>
    public void FinaliseLabel(AnalysisRootObject analysisObject)
    {
        Debug.Log("TRADEMR: FinaliseLabel...");
        PhysicsRaycastMask = 1 << physicsLayer; // define the mask

        List<double> probList = new List<double>();
        for (int i = 0; i < (analysisObject.predictions).Count; i++)
        {
            probList.Add(analysisObject.predictions[0].probability);
        }
        var maxProbIndex = probList.IndexOf(probList.Max());
        var maxProbTag = analysisObject.predictions[maxProbIndex].tagName;

        var maxProb = analysisObject.predictions[maxProbIndex].probability;
        Debug.Log("TRADEMR: max probability" + maxProb);
        if (maxProb >= probabilityThreshold)
        {
            Debug.Log("TRADEMR: Met Probability Threshold... creating label...");
            PlaceAnalysisLabel(maxProbTag);
            //lastLabelPlacedText.text = maxProbTag + " TMK: 01-1234"; //use as placeholder TMK number
            textDisplay.text = maxProbTag + " TMK: 01-1234"; //second view if label appears too small
            Debug.Log("TRADEMR: label: " + lastLabelPlaced);            
        }
        else
        {
            Debug.Log("TRADEMR: Did not meet threshold...");
        }

        // Reset the color of the cursor
        cursor.GetComponent<Renderer>().material.color = Color.green;

        // Stop the analysis process
        ImageCapture.Instance.ResetImageCapture();
    }

    /// <summary>
    /// This method hosts a series of calculations to determine the position 
    /// of the Bounding Box on the quad created in the real world
    /// by using the Bounding Box received back alongside the Best Prediction
    /// </summary>
    public Vector3 CalculateBoundingBoxPosition(Bounds b, BoundingBox boundingBox)
    {
        double centerFromLeft = boundingBox.left + (boundingBox.width / 2);
        double centerFromTop = boundingBox.top + (boundingBox.height / 2);

        double quadWidth = b.size.normalized.x;
        double quadHeight = b.size.normalized.y;

        double normalisedPos_X = (quadWidth * centerFromLeft) - (quadWidth / 2);
        double normalisedPos_Y = (quadHeight * centerFromTop) - (quadHeight / 2);

        return new Vector3((float)normalisedPos_X, (float)normalisedPos_Y, 0);
    }

}
