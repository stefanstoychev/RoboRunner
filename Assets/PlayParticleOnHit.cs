using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleOnHit : MonoBehaviour {

    public ParticleSystem[] childrenParticleSytems;

    bool disabledRelevantPSEmissions = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlaySystem();
        }
    }

    private void PlaySystem()
    {
        disabledRelevantPSEmissions = false;
    }



    // Initialization:
    void Start()
    {
        //childrenParticleSytems = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        // Process each child's particle system and disable its emission module.
        // For each child, we disable all emission modules of its children.
        if (!disabledRelevantPSEmissions)
        {
            foreach (ParticleSystem childPS in childrenParticleSytems)
            {
                // Get the emission module of the current child particle system [childPS].
                ParticleSystem.EmissionModule childPSEmissionModule = childPS.emission;
                // Disable the child's emission module.
                childPSEmissionModule.enabled = false;

                // Get all particle systems from the children of the current child [childPS].
                ParticleSystem[] grandchildrenParticleSystems = childPS.GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem grandchildPS in grandchildrenParticleSystems)
                {
                    // Get the emission module from the particle system of the current grand child.
                    ParticleSystem.EmissionModule grandchildPSEmissionModule = grandchildPS.emission;
                    // Disable the grandchild's mission module.
                    grandchildPSEmissionModule.enabled = false;
                }
            }

            disabledRelevantPSEmissions = true;
        }
    }
}
