using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleActions : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    public void SetParticleLifeTime(float newLifeTime)
    {
        var main = particle.main;
        main.startLifetime = newLifeTime;
    }

    public void SetParticleColor(Color color)
    {
        var main = particle.main;
        main.startColor = color;
    }
}
