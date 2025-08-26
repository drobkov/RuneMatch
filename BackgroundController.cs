using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float dragSpeed = 0.5f; // скорость перетаскивания объекта
    private Vector2 dragOrigin; // начальная точка перетаскивания

    public float minX = 200.0f; // минимальная граница по оси X
    public float maxX = 390.0f; // максимальная граница по оси X
    public float minY = -35.0f; // минимальная граница по оси Y
    public float maxY = 390.0f; // максимальная граница по оси Y

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

        Vector2 difference = (Vector2)Input.mousePosition - dragOrigin;
        Vector2 move = new Vector2(difference.x * dragSpeed, difference.y * dragSpeed);

        // Перемещаем объект
        transform.position += (Vector3)move;

        // Ограничение области перемещения объекта
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = clampedPosition;

        // Обновляем начальную точку перетаскивания
        dragOrigin = Input.mousePosition;
    }
}


