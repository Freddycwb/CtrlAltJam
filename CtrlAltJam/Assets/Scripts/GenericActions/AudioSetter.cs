using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetter : MonoBehaviour
{
    [SerializeField] private Instantiator instantiator;
    [SerializeField] private AudioClip[] clips;

    [SerializeField] private Vector2 minMaxPitch;

    private void Start()
    {
        instantiator.objCreated += SetAudio;
    }

    private void SetAudio(GameObject value)
    {
        if (value.GetComponent<AudioSource>() == null)
        {
            return;
        }
        AudioSource source = value.GetComponent<AudioSource>();
        AudioClip currentAudio = clips[Random.Range(0, clips.Length)];
        source.clip = currentAudio;
        source.pitch = Random.Range(minMaxPitch.x, minMaxPitch.y);
        source.Play();
        if (value.GetComponent<Destroyer>() != null)
        {
            value.GetComponent<Destroyer>().Delete(currentAudio.length);
        }
    }
}
