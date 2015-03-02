using UnityEngine;
using System.Collections;

public class LoadLevelByTimer : MonoBehaviour
{
    // время до загрузки уровня
    public float delay = 3; 

    public IEnumerator Start()
    {
        // задержка на 3 сек
        yield return new WaitForSeconds(delay);

        // загрузка главного меню
        Application.LoadLevel("SnakeMenu");
    }
}