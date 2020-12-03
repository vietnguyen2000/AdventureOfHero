using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXManager : MonoBehaviour
{
    public enum SoundEffect
    {
        Attack1, Attack2, Attack3,
        Hurt,
        Defense,
        RunL, RunR,
        Jump, Stand,
        Die
    }
    public SoundEffect soundEffect;
    public bool Play;
    private bool isPlayed;
    private AudioClip m_SoundEffect;

    void Update()
    {
        SetEnum();
        if (Play) PlayEffect();
        isPlayed = Play && isPlayed;
    }
    void SetEnum()
    {
        switch (soundEffect)
        {
            case SoundEffect.Attack1:
                m_SoundEffect = SFXManager.Sword[0];
                break;
            case SoundEffect.Attack2:
                m_SoundEffect = SFXManager.Sword[1];
                break;
            case SoundEffect.Attack3:
                m_SoundEffect = SFXManager.Sword[2];
                break;
            case SoundEffect.Hurt:
                int random = Random.Range(0, 4);
                m_SoundEffect = SFXManager.Hurt[random];
                break;
            case SoundEffect.Defense:
                m_SoundEffect = SFXManager.Defense;
                break;
            case SoundEffect.RunL:
                m_SoundEffect = SFXManager.Run[0];
                break;
            case SoundEffect.RunR:
                m_SoundEffect = SFXManager.Run[1];
                break;
            case SoundEffect.Jump:
                m_SoundEffect = SFXManager.Jump;
                break;
            case SoundEffect.Stand:
                m_SoundEffect = SFXManager.Stand;
                break;
            case SoundEffect.Die:
                m_SoundEffect = SFXManager.Die;
                break;
        }
    }
    void PlayEffect()
    {
        if (!isPlayed)
        {
            SFXManager.PlaySoundEffectOneShot(m_SoundEffect);
            isPlayed = true;
        }
    }
}
