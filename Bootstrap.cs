using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameWorldController _gameWorldController;
    [SerializeField] private NavigationBar _navigationBar;
    private void Awake(){
        _gameWorldController.TargetScale(); // Выставляем позицию background на главной.
        _navigationBar.OpenMainScreen(); // При запуске открываем сразу главный экран.
    }
}
