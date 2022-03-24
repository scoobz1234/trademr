using UnityEngine;

public class AIToggleHideAppBar : MonoBehaviour
{
    public LabelManager label;
    public GameObject ourHide;
    public GameObject ourShow;

    public void hide()
    {
        label.hide();
        ourHide.SetActive(false); //hides hide button
        ourShow.SetActive(true); //shows show button
    }
    public void show()
    {
        label.show();
        ourHide.SetActive(true); //shows hide button
        ourShow.SetActive(false); //hides show button
    }
}
