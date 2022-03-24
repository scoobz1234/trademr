using UnityEngine;

public class HideObjectAppBar : MonoBehaviour
{
    public GameObject ourObject;
    public GameObject ourHide;
    public GameObject ourShow;

    public void hide()
    {
        ourObject.SetActive(false);
        ourHide.SetActive(false); //hides hide button
        ourShow.SetActive(true); //shows show button
    }
    public void show()
    {
        ourObject.SetActive(true);
        ourHide.SetActive(true); //shows hide button
        ourShow.SetActive(false); //hides show button
    }
}
