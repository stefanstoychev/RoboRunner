using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleOnHit : MonoBehaviour {

    ParticleSystem system
    {
        get
        {
            if (_CachedSystem == null)
                _CachedSystem = GetComponentInChildren<ParticleSystem>();
            return _CachedSystem;
        }
    }

    private ParticleSystem _CachedSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlaySystem();
        }
    }

    private void PlaySystem()
    {
        system.Play(true);
    }
}
