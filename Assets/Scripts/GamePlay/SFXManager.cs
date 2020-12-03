using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static AudioClip Defense, Jump, Stand, Die, Slash, CoinBounce, CoinCollect, PotionCollect;
    public static AudioClip[] Sword, Run, Hurt;
    public static AudioClip ass;
    public static AudioSource audioSource;
    public static Dictionary<AudioClip, float> lastTimePlay;
    void Awake()
    {
        Debug.Log("add soundeffect1!");
        lastTimePlay = new Dictionary<AudioClip, float>();
        audioSource = GetComponent<AudioSource>();
        Sword = new AudioClip[3];
        Run = new AudioClip[2];
        Hurt = new AudioClip[5];
        AddSourceAudio(ref Sword[0], "SoundEffect/DamageSound/sfx_Sword_1");
        AddSourceAudio(ref Sword[1], "SoundEffect/DamageSound/sfx_Sword_2");
        AddSourceAudio(ref Sword[2], "SoundEffect/DamageSound/sfx_Sword_3");
        AddSourceAudio(ref Defense, "SoundEffect/DamageSound/sfx_Defense");
        AddSourceAudio(ref Hurt[0], "SoundEffect/DamageSound/sfx_Hit1");
        AddSourceAudio(ref Hurt[1], "SoundEffect/DamageSound/sfx_Hit2");
        AddSourceAudio(ref Hurt[2], "SoundEffect/DamageSound/sfx_Hit3");
        AddSourceAudio(ref Hurt[3], "SoundEffect/DamageSound/sfx_Hit4");
        AddSourceAudio(ref Hurt[4], "SoundEffect/DamageSound/sfx_Hit5");
        AddSourceAudio(ref Run[0], "SoundEffect/MovementSound/sfx_RunL");
        AddSourceAudio(ref Run[1], "SoundEffect/MovementSound/sfx_RunR");
        AddSourceAudio(ref Jump, "SoundEffect/MovementSound/sfx_Jump");
        AddSourceAudio(ref Stand, "SoundEffect/MovementSound/sfx_Stand");
        AddSourceAudio(ref Die, "SoundEffect/DamageSound/sfx_Die");
        AddSourceAudio(ref Slash, "SoundEffect/DamageSound/sfx_Slash");
        AddSourceAudio(ref CoinBounce, "SoundEffect/Item/sfx_CoinBounce");
        AddSourceAudio(ref CoinCollect, "SoundEffect/Item/sfx_CoinCollect");
        AddSourceAudio(ref PotionCollect, "SoundEffect/Item/sfx_PotionCollect");
    }
    private void Update()
    {
        audioSource.volume = OptionsScript.Volume;
    }
    //Play one shot sound effect dont care about delay
    public static void PlaySoundEffectOneShot(AudioClip SoundEffect)
    {
        audioSource.PlayOneShot(SoundEffect);
        lastTimePlay[SoundEffect] = Time.time;
        
    }
    public static void PlaySoundEffectOneShot(AudioClip SoundEffect,float minTimeBetweenTwoSound)
    {
         if(Time.time - lastTimePlay[SoundEffect] >= minTimeBetweenTwoSound)
        {
            PlaySoundEffectOneShot(SoundEffect);
        }
    }
    public static void PlaySoundEffectOneShot(AudioClip SoundEffect, float minTimeBetweenTwoSound, float timeDelay)
    {
        if(Time.time - lastTimePlay[SoundEffect] >= minTimeBetweenTwoSound)
        {
            //AudioClip root = audioSource.clip;
            audioSource.clip = SoundEffect;
            
            audioSource.PlayDelayed(timeDelay);
            lastTimePlay[SoundEffect] = Time.time;
            audioSource.clip = null;
        }
    }
   
    void AddSourceAudio(ref AudioClip clip, string path)
    {
        clip = Resources.Load<AudioClip>(path);
        lastTimePlay.Add(clip, Time.time);
    }
}
