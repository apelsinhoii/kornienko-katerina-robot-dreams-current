using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float liftSpeed = 2f; 
    [SerializeField] private float maxHeight = 10f; 

    private float startY;

    void Start()
    {
        startY = transform.position.y; 
    }

    void Update()
    {
        if (transform.position.y < startY + maxHeight)
        {
            transform.position += Vector3.up * liftSpeed * Time.deltaTime;
        }
    }
}
