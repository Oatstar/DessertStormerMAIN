using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource shootSplat;
    public AudioSource hitSplat;
    public AudioSource walkSound;
    // Start is called before the first frame update
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void PlayShootSplat()
    {
        shootSplat.Play();
    }

    public void PlayHitSplat()
    {
        hitSplat.Play();
    }

    public void PlayWalkSound()
    {
        if(!walkSound.isPlaying)
            walkSound.Play();
    }
}
