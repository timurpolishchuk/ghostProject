using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffectScript : MonoBehaviour
{
    public Animation anim;
    private void Start()
    {
        InvokeRepeating("PlayAnimationLoop", 2.0f, 4.6f);
    }
    private void PlayAnimationLoop()
    {
        anim.Play();
    }
}
