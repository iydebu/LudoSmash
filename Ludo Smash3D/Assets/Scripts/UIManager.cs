using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public FadeEffect fadeEffect;
   
    void Start()
    {
        fadeEffect.FadeIn();
    }
}
