using UnityEngine;

public class PreserveChildScale : MonoBehaviour
{
    public float PreserveChild = 1;
    public GameObject parent;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(
            PreserveChild / parent.transform.localScale.x,
            PreserveChild / parent.transform.localScale.y,
            PreserveChild / parent.transform.localScale.z
            );
    }
}