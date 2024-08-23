using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : MonoBehaviour
{
    public ParticleSystem prefab;
    public int poolSize = 10;

    private List<ParticleSystem> particles;

    // Start is called before the first frame update
    void Start()
    {
        Health.pp = this;

        particles = new List<ParticleSystem>();
        for(int i = 0; i < poolSize; i++)
        {
            ParticleSystem particle = Instantiate(prefab, transform);
            particle.Stop();
            particles.Add(particle);
        }
    }

    public ParticleSystem GetParticle()
    {
        for(int i = 0; i<particles.Count; i++)
        {
            if (!particles[i].isPlaying)
            {
                return particles[i];
            }
        }

        //if we don't have an available particle, instantiate a new one
        ParticleSystem particle = Instantiate(prefab, transform);
        particles.Add(particle);
        return particle;
    }
}
