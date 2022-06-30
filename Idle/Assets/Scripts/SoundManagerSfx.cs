using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSfx : MonoBehaviour
{
    [Header("AUDIO SFX")]
    public static AudioClip kapiOpen;
    public static AudioClip kapiClose;
    

    static AudioSource audioSrc;
    private void Start()
    {
        kapiOpen = Resources.Load<AudioClip>("KapiOpen");
        kapiClose = Resources.Load<AudioClip>("KapiClose");
      

        audioSrc = GetComponent<AudioSource>();
    }
    public static void PlaySfx(string clip)
    {
        switch (clip)
        {
            case "KapiOpen":
                audioSrc.PlayOneShot(kapiOpen);
                Debug.Log("Kapi Sesi");
                break;
            case "KapiClose":
                audioSrc.PlayOneShot(kapiOpen);
                Debug.Log("Kapi Sesi");
                break;
        }
    }
}