using System.Collections;
using UnityEngine;

public class RepositionContent : MonoBehaviour
{

    public Transform anchor;

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            StartCoroutine(RepositionContentCoroutine());
        }
    }

    IEnumerator RepositionContentCoroutine()
    {
        yield return new WaitForEndOfFrame();
        transform.position = anchor.position;
    }


}
