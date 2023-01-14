using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBehaviour : MonoBehaviour
{
    public Renderer SleepRenderer;
    public GameObject PlayerObject;

    public AudioSource MetalSound;
    public AudioSource SleepSound;

    private bool _isSleeping;

    void Awake()
    {
        _isSleeping = false;
        ShowObjects();
        PlaySounds();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            ToggleSleep();
        }
    }

    void ToggleSleep()
    {
        if(_isSleeping)
        {
            _isSleeping = false;
        }
        else
        {
            _isSleeping = true;
        }

        ShowObjects();
        PlaySounds();
        EnergyManager.instance.IsSleeping = _isSleeping;
    }

    void ShowObjects()
    {
        PlayerObject.SetActive(!_isSleeping);
        SleepRenderer.enabled = _isSleeping;
    }

    void PlaySounds()
    {
        MetalSound.mute = _isSleeping;
        SleepSound.mute = !_isSleeping;
    }
}
