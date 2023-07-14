using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static private SoundManager myInstance = null;
    static public SoundManager Instance
    {
        get 
        { 
            return myInstance; 
        }
    }
    private void Awake()
    {
        myInstance = this;
    }



    [Header("BGM")]
    [SerializeField] AudioSource audio_bgm;
    public void PlayAudio_BGM() => audio_bgm.Play();
    public void StopAudio_BGM() => audio_bgm.Stop();

    [Header("Etc")]

    [SerializeField] AudioSource audio_Etc_End;
    public void PlayAudio_Etc_End() => audio_Etc_End.Play();


    [Header("System")]
    [SerializeField] AudioSource audio_Destroy;
    public void PlayAudio_Destroy() => audio_Destroy.Play();
    [SerializeField] AudioSource audio_Select;
    public void PlayAudio_Select() => audio_Select.Play();
    [SerializeField] AudioSource audio_Error;
    public void PlayAudio_Error() => audio_Error.Play();
    [SerializeField] AudioSource audio_Effect_SlotEnter;
    public void PlayAudio_Drop() => audio_Effect_SlotEnter.Play();


    [Header("Effect")]
    [Header("Oven")]
    [SerializeField] AudioSource audio_Effect_OvenEnter;
    public void PlayAudio_Effect_OvenEnter() => audio_Effect_OvenEnter.Play();

    [SerializeField] AudioSource audio_Effect_OvenOvercook;
    public void PlayAudio_Effect_OvenOvercook() => audio_Effect_OvenOvercook.Play();

    [SerializeField] AudioSource audio_Effect_OvenSuccess;
    public void PlayAudio_Effect_OvenSuccess() => audio_Effect_OvenSuccess.Play();



    [Header("Merchant")]
    [SerializeField] AudioSource audio_Effect_Merchant_Angry;
    public void PlayAudio_Effect_MerchantAngry() => audio_Effect_Merchant_Angry.Play();

    [SerializeField] AudioSource audio_Effect_Merchant_Happy;
    public void PlayAudio_Effect_MerchantHappy() => audio_Effect_Merchant_Happy.Play();
    [SerializeField] AudioSource[] audio_Effect_Merchant_Enter;
    public void PlayAudio_Effect_MerchantEnter() => audio_Effect_Merchant_Enter[UnityEngine.Random.Range(0, audio_Effect_Merchant_Enter.Length)].Play();




}
