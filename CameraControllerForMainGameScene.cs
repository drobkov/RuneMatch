using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 1.0f; // скорость перетаскивания камеры
    private Vector3 dragOrigin; // начальная точка перетаскивания

    public float minX = -503.11f; // минимальная граница по оси X
    public float maxX = 503.11f; // максимальная граница по оси X
    public float minY = -507.0f; // минимальная граница по оси Y
    public float maxY = 507.0f; // максимальная граница по оси Y

    void Update()
    {
        // Проверяем нажатие на левую кнопку мыши
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        // Проверяем, удерживается ли левая кнопка мыши
        if (!Input.GetMouseButton(0)) return;

        Vector3 difference = Input.mousePosition - dragOrigin;
        Vector3 move = new Vector3(-difference.x * dragSpeed, -difference.y * dragSpeed, 0);
        transform.Translate(move, Space.World);

        // Ограничение области перемещения камеры
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = clampedPosition;

        dragOrigin = Input.mousePosition;
    }
}
