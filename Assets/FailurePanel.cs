using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class FailurePanel : MonoBehaviour
{
    public GameObject imageTaken;
    public GameObject imageStock;
    public GameObject hidePanel;

    //stockimage, use S3
    // stockImageTexture =  Application.dataPath + "Assets/Scenes/bounding corners/coach (1).png";
    // imageStock.RawImage.texture = stockImageTexture;

    //imageTaken
    // void pullImageCaptured(){
    //     RawImage rawImageTaken = imageTaken.GetComponent<RawImage>();
    //     string[] files =  System.IO.Directory.GetFiles(@"c:\", "CapturedImage*");//finds any files that start with "CapturedImage"
    //     foreach (string file in files){
    //         Texture2D newPhoto = new Texture2D (1, 1);
    //         newPhoto.LoadImage(Convert.FromBase64String(PlayerPrefs.GetString(file)));
    //         newPhoto.Apply();
    //         rawImageTaken.texture = newPhoto;
    //     }
    //     // imageTaken.RawImage.texture = Path.Combine(Application.persistentDataPath;, "CapturedImage{0}.jpg");
    // }

    public void closeUIPanel(){
        hidePanel.SetActive(false);
    }

    public void retakeImageTaken(){
        //call invokerepeating process
    }

    public void openIPRISDatabase(){
       //open link to IPRIS, more implementation to be discussed
       //maybe open up an internet browser to website?
    }



    
}
