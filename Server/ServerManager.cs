using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ServerManager : MonoBehaviour
{
    private string updateScoreUrl = "https://runematch.ru/server/update_score.php"; // URL API
    private string apiKey = "codesecret"; // API-ключ

    [SerializeField]
    private GameData gameData; // Данные игрока
    [SerializeField] 
    private LeaderboardUI leaderboardUI;

    void Start()
    {
        SendInfoOnServer();

    }
    public void SendInfoOnServer(){
        //SendScore(gameData.saveData.playerId, gameData.saveData.newPlayerName, gameData.GetNextLevel());
        leaderboardUI.LoadLeaderboard();
    }

    // Метод отправки данных на сервер
    public void SendScore(string playerId, string playerName, int level)
    {
        StartCoroutine(SendScoreCoroutine(playerId, playerName, level));
    }

    private IEnumerator SendScoreCoroutine(string playerId, string playerName, int level)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", playerId);  // Добавляем параметры в форму
        form.AddField("name", playerName);
        form.AddField("level", level);

        using (UnityWebRequest www = UnityWebRequest.Post(updateScoreUrl, form))
        {
            // Добавляем заголовок для API-ключа
            www.SetRequestHeader("Authorization", apiKey);  // Ваш API-ключ
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            www.SetRequestHeader("X-Custom-Header", "SomeValue");  // Пример дополнительного заголовка
            www.SetRequestHeader("Api", apiKey);  // Ваш API-ключ
        

            yield return www.SendWebRequest(); // Ожидаем ответа

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending score: " + www.error);
            }
            else
            {
                Debug.Log("Score sent successfully: " + www.downloadHandler.text);
            }
        }
    }
}
