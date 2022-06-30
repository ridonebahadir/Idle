using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSfx : MonoBehaviour
{
    [Header("AUDIO SFX")]
    public static AudioClip kapiOpen;
    public static AudioClip kapiClose;
    public static AudioClip alma;
    public static AudioClip verme;
    

    static AudioSource audioSrc;
    private void Start()
    {
        kapiOpen = Resources.Load<AudioClip>("KapiOpen");
        kapiClose = Resources.Load<AudioClip>("KapiClose");
        alma = Resources.Load<AudioClip>("Alma");
        verme = Resources.Load<AudioClip>("Verme");
      
        
        audioSrc = GetComponent<AudioSource>();
    }
  
   public static IEnumerator Play(string clip,float invokeTime)
    {
        yield return new WaitForSeconds(invokeTime);
        switch (clip)
        {
            case "KapiOpen":
                audioSrc.PlayOneShot(kapiOpen);
                Debug.Log("Kapi Sesi");
                break;
            case "KapiClose":
                audioSrc.PlayOneShot(kapiClose);
                Debug.Log("Kapi Sesi");
                break;
            case "Alma":
                audioSrc.PlayOneShot(alma);
                Debug.Log("Alma Sesi");
                break;
            case "Verme":
                audioSrc.PlayOneShot(verme);
                Debug.Log("Verme Sesi");
                break;
        }
    }
}