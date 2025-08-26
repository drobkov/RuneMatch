using System.Collections;
using UnityEngine;

public class GameWorldController : MonoBehaviour
{ public Transform gameWorld; // Ссылка на объект игрового мира
    public float dragSpeed = 0.5f; // Скорость перемещения карты
    public float zoomSpeed = 0.2f; // Скорость масштабирования
    public float minScale = 0.5f;  // Минимальный масштаб
    public float maxScale = 3.0f;  // Максимальный масштаб

    private Vector3 dragOrigin; // Начальная точка для перетаскивания
    private float targetScale;   // Целевой масштаб

    // Настраиваемые границы (можно менять в инспекторе)
    public float minX = -1400f, maxX = 1900f, minY = 800f, maxY = 2124f;
    public float worldWidth = 2048f;
    public float worldHeight = 1624f;

    void Start()
    {
        //targetScale = gameWorld.localScale.x;
        //UpdateBounds();
    }

    public void TargetScale(){
    targetScale = gameWorld.localScale.x;   
    }


    void Update()
    {
        HandleDrag(); // Перетаскивание
        HandleZoom(); // Зум
        ClampCameraPosition(); // Ограничение перемещения

        // Применяем плавный зум
        gameWorld.localScale = new Vector3(targetScale, targetScale, 1f);
    }

    // Обновление границ карты (меняешь параметры вручную в Unity)
    void UpdateBounds()
    {
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float scaledWidth = worldWidth * targetScale;
        float scaledHeight = worldHeight * targetScale;

        //minX = -scaledWidth / 2 + cameraWidth;
        //maxX = scaledWidth / 2 - cameraWidth;
        //minY = -scaledHeight / 2 + cameraHeight;
        //maxY = scaledHeight / 2 - cameraHeight;
    }

    // Ограничение движения карты в пределах бэкграунда
    void ClampCameraPosition()
    {
        float clampedX = Mathf.Clamp(gameWorld.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(gameWorld.position.y, minY, maxY);
        gameWorld.position = new Vector3(clampedX, clampedY, gameWorld.position.z);
    }

    // Перемещение карты (исправлена инверсия!)
    void HandleDrag()
    {
        if (Input.touchCount == 1) // Сенсорное перетаскивание
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = Camera.main.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 difference = Camera.main.ScreenToWorldPoint(touch.position) - dragOrigin;
                gameWorld.position += difference * dragSpeed; // Теперь инверсии нет!
                dragOrigin = Camera.main.ScreenToWorldPoint(touch.position);
            }
        }
        else if (Input.GetMouseButtonDown(0)) // ПК: Захват мыши
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0)) // ПК: Перетаскивание (исправлена инверсия!)
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameWorld.position += difference * dragSpeed; // Теперь двигается в правильную сторону
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    // Зум (Колесо мыши + Сенсорный щипок)
    void HandleZoom()
    {
        // Колесико мыши
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            targetScale -= scroll * zoomSpeed;
            targetScale = Mathf.Clamp(targetScale, minScale, maxScale);
            UpdateBounds();
            ClampCameraPosition();
        }

        // Сенсорный зум (щипок)
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float prevDistance = (touch1.position - touch2.position).magnitude;
            float currentDistance = (touch1.position - touch2.position).magnitude;

            float delta = (currentDistance - prevDistance) * 0.001f;
            targetScale += delta;
            targetScale = Mathf.Clamp(targetScale, minScale, maxScale);

            UpdateBounds();
            ClampCameraPosition();
        }
        }
        /// ТЕСТИРОВАНИЕ начинается. 
        /// 
        public void MoveToPosition(Vector3 targetPosition)
{
    StopAllCoroutines(); // Остановить все другие движения
    StartCoroutine(SmoothMove(targetPosition));
}

private IEnumerator SmoothMove(Vector3 targetPosition)
{
    Vector3 startPosition = gameWorld.position;
    float duration = 1f; // Время перемещения
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        gameWorld.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Фиксируем конечное положение
    gameWorld.position = targetPosition;
        //// ТЕСТИРОВАНИЕ  заканчивается. 
    }
    /*
    public Transform gameWorld; // Ссылка на объект игрового мира
    public float dragSpeed = 0.1f; // Скорость перемещения карты
    public float zoomSpeed = 0.1f; // Скорость масштабирования
    public float minScale = 0.1f;  // Минимальный масштаб
    public float maxScale = 3.0f;  // Максимальный масштаб

    private Vector3 dragOrigin; // Начальная точка для перетаскивания
    private float targetScale;   // Целевой масштаб

    void Start()
    {
        
    }

    public void TargetScale(){
    targetScale = gameWorld.localScale.x;   
    }

    void Update()
    {
        HandleDrag(); // Обрабатываем перетаскивание
        HandleZoom(); // Обрабатываем зум

        // Применяем плавный зум
        gameWorld.localScale = new Vector3(targetScale, targetScale, 1f);
    }

    // Обработка перетаскивания (для ПК и мобильных устройств)
    void HandleDrag()
    {
        if (Input.touchCount == 1) // Один палец на экране
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = Camera.main.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchDelta = Camera.main.ScreenToWorldPoint(touch.position) - dragOrigin;
                gameWorld.position += touchDelta * dragSpeed;
                dragOrigin = Camera.main.ScreenToWorldPoint(touch.position); // Обновляем точку отсчета
            }
        }
        else if (Input.GetMouseButtonDown(0)) // Для ПК (левая кнопка мыши)
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0)) // Для ПК (когда держим кнопку мыши)
        {
            Vector3 mouseDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragOrigin;
            gameWorld.position += mouseDelta * dragSpeed;
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Обновляем точку отсчета
        }
    }

    // Обработка зума с колесика мыши и жестов на мобильных устройствах
    void HandleZoom()
    {
        // Мышь (колесо прокрутки)
        float scroll = Input.mouseScrollDelta.y;
        //Debug.Log(scroll);
        if (scroll != 0f)
        {
            targetScale -= scroll * zoomSpeed;
        }

        // Мобильные устройства (щипок)
        if (Input.touchCount == 2) // Два пальца на экране
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            float prevTouchDistance = (touchZero.position - touchOne.position).magnitude;
            float currentTouchDistance = (touchZero.position - touchOne.position).magnitude;

            float touchDelta = currentTouchDistance - prevTouchDistance;
            targetScale += touchDelta * zoomSpeed * 0.01f; // Множитель для чувствительности
        }

        // Ограничиваем масштаб
        targetScale = Mathf.Clamp(targetScale, minScale, maxScale);
    } */
}
