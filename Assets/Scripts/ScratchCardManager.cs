using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchCardManager : MonoBehaviour
{
    public Image[] scratchCards;

    // Start is called before the first frame update
    void Start()
    {
        scratchCards[0].GetComponent<ScratchCard>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
