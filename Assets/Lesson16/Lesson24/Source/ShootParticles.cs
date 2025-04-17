using UnityEngine;

public class ShootParticles : MonoBehaviour
{
    public ParticleSystem particleSystem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particleSystem.Emit(5); 
        }
    }
}

