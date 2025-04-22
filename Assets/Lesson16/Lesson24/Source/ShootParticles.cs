using UnityEngine;

public class ShootParticles : MonoBehaviour
{
    public ParticleSystem myParticleSystem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myParticleSystem.Emit(5); 
        }
    }
}

