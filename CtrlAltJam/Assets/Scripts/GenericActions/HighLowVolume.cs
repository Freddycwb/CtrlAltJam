using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLowVolume : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private float transitionSpeed;
    private float volume;

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        StopCoroutine(VolumeRoutine());
        StartCoroutine(VolumeRoutine());
    }

    IEnumerator VolumeRoutine()
    {
        if (volume > source.volume)
        {
            for (float i = source.volume; i < volume; i += Time.deltaTime)
            {
                source.volume = i;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            for (float i = source.volume; i > volume; i -= Time.deltaTime)
            {
                source.volume = i;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
