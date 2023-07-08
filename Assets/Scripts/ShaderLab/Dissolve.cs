using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private float dissolveTime = 0.2f;
    private float dissolveTex = 0;

    private Renderer dissolver;

    private void Awake()
    {
        dissolver = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DissolveObject();
    }

    private void DissolveObject()
    {
        dissolver.material.SetFloat("_Dissolve", dissolveTex);
        dissolveTex += 0.1f;
    }

    IEnumerator DissolveGradual()
    {
        yield return new WaitForSeconds(3);
    }
}
