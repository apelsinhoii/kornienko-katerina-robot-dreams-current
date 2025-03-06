using UnityEngine;

public class CameraRigController : MonoBehaviour
{
    public Transform target;   // Персонаж
    public Transform yaw;      // Об'єкт для повороту по горизонталі
    public Transform pitch;    // Об'єкт для повороту по вертикалі
    public float distance = 5.0f;
    public float sensitivity = 3.0f;
    public float rotationSpeed = 10.0f; // Швидкість повороту персонажа
    public float minPitch = -30f;
    public float maxPitch = 60f;

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    void Update()
    {
        if (target == null) return;

        // Отримуємо рух мишки або правого стіка
        currentYaw += Input.GetAxis("Mouse X") * sensitivity;
        currentPitch -= Input.GetAxis("Mouse Y") * sensitivity;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch); // Обмежуємо нахил

        // Оновлюємо повороти камери
        yaw.rotation = Quaternion.Euler(0, currentYaw, 0);
        pitch.localRotation = Quaternion.Euler(currentPitch, 0, 0);

        // Камера залишається на заданій відстані позаду
        transform.position = target.position - yaw.forward * distance;

        // 🔥 Плавний поворот персонажа у напрямку камери
        target.rotation = Quaternion.Slerp(target.rotation, Quaternion.Euler(0, currentYaw, 0), Time.deltaTime * rotationSpeed);
    }
}