using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Glitch : MonoBehaviour
{
    public float glitchProb;

    private Renderer holoGlitch;

    private WaitForSeconds loopWait = new WaitForSeconds(1f);
    private WaitForSeconds loopDur = new WaitForSeconds(0.1f);

    // Start is called before the first frame update
    void Awake()
    {
        holoGlitch = GetComponent<Renderer>();

        transform.DORotate(new Vector3(0, 360, 0), 10, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1).SetRelative();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            float glitchTest = Random.Range(0, 1);

            if (glitchTest < glitchProb)
                StartCoroutine(GlitchStart());

            yield return loopWait;
        }
    }

    IEnumerator GlitchStart()
    {
        loopDur = new WaitForSeconds(Random.Range(0.05f, 0.25f));
        holoGlitch.material.SetFloat("_Distance", 0.15f);
        holoGlitch.material.SetFloat("_Amount", 1);
        holoGlitch.material.SetFloat("_Amplitude", Random.Range(20, 50));
        holoGlitch.material.SetFloat("_Speed", Random.Range(1, 5));
        yield return loopDur;
        holoGlitch.material.SetFloat("_Amount", 0);
    }
}
