using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public string leaderboardUrl = "https://runematch.ru/server/getleaderboard.php"; // URL API
    public GameObject leaderboardEntryPrefab; // Префаб для отображения одного игрока
    public Transform leaderboardContainer; // Контейнер, куда будут добавляться префабы
    
    private string apiKey = "codesecret"; // API-ключ

    

    // Метод для загрузки лидерборда
    public void LoadLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }

    private IEnumerator GetLeaderboard()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(leaderboardUrl))
        {
            // Устанавливаем заголовки для запроса
            www.SetRequestHeader("Authorization", apiKey);  // Ваш API-ключ
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            // Отправляем запрос и ждем ответа
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка загрузки лидеров: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;

                try
                {
                    LeaderboardResponse data = JsonUtility.FromJson<LeaderboardResponse>(json);

                    if (data != null && data.success)
                    {
                        // Очищаем контейнер перед загрузкой новых данных
                        foreach (Transform child in leaderboardContainer)
                        {
                            Destroy(child.gameObject);
                        }
                        
                        int i = 1; // Счетчик позиций
                        // Для каждого игрока создаем экземпляр префаба и заполняем его данными
                        foreach (var entry in data.leaderboard)
                        {
                            // Создаем новый экземпляр префаба
                            GameObject leaderboardEntry = Instantiate(leaderboardEntryPrefab, leaderboardContainer);

                            // Получаем компоненты для имени и уровня
                            
                            TMP_Text nameText = leaderboardEntry.transform.Find("NameText").GetComponent<TMP_Text>();
                            TMP_Text levelText = leaderboardEntry.transform.Find("LevelText").GetComponent<TMP_Text>();
                            TMP_Text positionText = leaderboardEntry.transform.Find("PositionText").GetComponent<TMP_Text>();
                            // Заполняем текст соответствующими данными
                            nameText.text = entry.name;
                            levelText.text = "" + entry.level;
                            positionText.text = "" + i; // Номер позиции
                            i++; // Увеличиваем счетчик
                        }
                    }
                    else
                    {
                        Debug.LogError("Ошибка в статусе ответа от сервера: " + data.success);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Ошибка при разборе JSON: " + ex.Message);
                }
            }
        }
    }
}

[System.Serializable]
public class LeaderboardResponse
{
    public bool success; // Статус ответа
    public LeaderboardEntry[] leaderboard; // Массив лидеров
}

[System.Serializable]
public class LeaderboardEntry
{
    public string name;  // Имя игрока
    public int level;    // Уровень игрока
}
