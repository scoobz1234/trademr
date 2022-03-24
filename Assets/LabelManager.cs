using UnityEngine;

public class LabelManager : MonoBehaviour
{
    //Handles the hiding the label clones for AI Toggle
    public GameObject labelObject;

    public void hide()
    {
        labelObject.SetActive(false);
    }
    public void show()
    {
        labelObject.SetActive(true);
    }
}
