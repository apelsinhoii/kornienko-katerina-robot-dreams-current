using UnityEngine;

public class ShootParticles : MonoBehaviour
{
    public ParticleSystem shootEffect;           
    public ParticleSystem hitEffectPrefab;       
    public Transform muzzlePoint;              
    public float maxDistance = 100f;             

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
 
            if (shootEffect != null)
            {
                shootEffect.Emit(5);
            }

            Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                Debug.Log("Hit: " + hit.collider.name);

                if (hitEffectPrefab != null)
                {
                    ParticleSystem hitEffect = Instantiate(
                        hitEffectPrefab,
                        hit.point,
                        Quaternion.LookRotation(hit.normal)
                    );
                    hitEffect.Emit(10);
                    Destroy(hitEffect.gameObject, 2f);
                }
            }
        }
    }
}
