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
    public static AudioClip unlocked;
    public static AudioClip generatorOpen;
    public static AudioClip generatorClose;
    public static AudioClip skate;
    

    static AudioSource audioSrc;
    static AudioSource audioSrc2;
    private void Start()
    {
        kapiOpen = Resources.Load<AudioClip>("KapiOpen");
        kapiClose = Resources.Load<AudioClip>("KapiClose");
        alma = Resources.Load<AudioClip>("Alma");
        verme = Resources.Load<AudioClip>("Verme");
        unlocked = Resources.Load<AudioClip>("Unlocked");
        generatorOpen = Resources.Load<AudioClip>("GeneratorOpen");
        generatorClose = Resources.Load<AudioClip>("GeneratorClose");
        skate = Resources.Load<AudioClip>("Skate");
      
        
        audioSrc = GetComponent<AudioSource>();
        audioSrc2 =transform.GetChild(0).GetComponent<AudioSource>();
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
            case "Unlocked":
                audioSrc.PlayOneShot(unlocked);
                Debug.Log("Unlocked Sesi");
                break;
            case "GeneratorOpen":
                audioSrc.PlayOneShot(generatorOpen);
                Debug.Log("generatorOpen Sesi");
                break;
            case "GeneratorClose":
                audioSrc.PlayOneShot(generatorClose);
                Debug.Log("generatorClose Sesi");
                break;
            case "Skate":

                audioSrc2.clip = skate;
                audioSrc2.Play();
                Debug.Log("skate Sesi");
                break;
            //case "SkateStop":
            //    audioSrc2.Stop();
            //    Debug.Log("skate Sesi durdu");
            //    break;
        }
    }
}