using System.Collections.Generic;
using UnityEngine;

public class HideObjectsAppBar : MonoBehaviour
{
    public List<GameObject> ourObjects = new List<GameObject>();
    public GameObject ourHide;
    public GameObject ourShow;

    public void hide()
    {
        foreach (GameObject gameObject in ourObjects)
        {
            gameObject.SetActive(false);
        }
        ourHide.SetActive(false); //hides hide button
        ourShow.SetActive(true); //shows show button
    }
    public void show()
    {
        foreach (GameObject gameObject in ourObjects)
        {
            gameObject.SetActive(true);
        }
        ourHide.SetActive(true); //shows hide button
        ourShow.SetActive(false); //hides show button
    }
}
