using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GroupsManager : MonoBehaviour
{
    public string sendLivesUrl = "https://runematch.ru/server/send_lives.php";  // URL для передачи жизней
    private string apiKey = "codesecret";  // API-ключ

    // Метод для отправки жизней
    public void SendLives(string playerId, string targetPlayerId, int lives)
    {
        StartCoroutine(SendLivesRequest(playerId, targetPlayerId, lives));
    }

    private IEnumerator SendLivesRequest(string playerId, string targetPlayerId, int lives)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerId", playerId);
        form.AddField("targetPlayerId", targetPlayerId);
        form.AddField("groupId", "1");  // Замените на текущий ID группы! ПЕРЕДЕЛАТЬ! 
        form.AddField("lives", lives);

        using (UnityWebRequest www = UnityWebRequest.Post(sendLivesUrl, form))
        {
            www.SetRequestHeader("Authorization", apiKey);  // Ваш API-ключ
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            www.SetRequestHeader("Api", apiKey);  // Ваш API-ключ

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка передачи жизней: " + www.error);
            }
            else
            {
                Debug.Log("Жизни успешно переданы!");
            }
        }
    }
}
