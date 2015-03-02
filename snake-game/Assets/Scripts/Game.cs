using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    // набранные очки
    public static int points;

    private string _pointsString;
    private int _lastPonts = -1;
    
    // сгенерированные цыфры
    public static string numberCombination;
    private int _randomNumber;
    public Text text;

    private LabGeneration labGeneration = LabGeneration.Instance;

    // генерируется уровень при загрузке сцены, который выбрал пользователь
    public void Awake()
    {
        switch (SelectIntricacy.intricacy)
        {
            case "easy":
            labGeneration.createMap("Prefabs/Lab1");
            break;
            case "average":
            labGeneration.createMap("Prefabs/Lab2");         
            break;
            case "complex":
            labGeneration.createMap("Prefabs/Lab3");
            break;
            case "profi":
            labGeneration.createMap("Prefabs/Lab4 1");
            break;
        }       
              
        // обнуляется очки
        points = 0;

        //задается начальную комбинацию и соответственно комбинации генерируются первые цыфры
        for (int i = 0; i < 10; i++)
        {
            _randomNumber = Random.Range(0, 9);
            switch(_randomNumber)
            {
                case 0:
                    Food.GenerateNewFood("Prefabs/0");
                    break;
                case 1:
                    Food.GenerateNewFood("Prefabs/1");
                    break;
                case 2:
                    Food.GenerateNewFood("Prefabs/2");
                    break;
                case 3:
                    Food.GenerateNewFood("Prefabs/3");
                    break;
                case 4:
                    Food.GenerateNewFood("Prefabs/4");
                    break;
                case 5:
                    Food.GenerateNewFood("Prefabs/5");
                    break;
                case 6:
                    Food.GenerateNewFood("Prefabs/6");
                    break;
                case 7:
                    Food.GenerateNewFood("Prefabs/7");
                    break;
                case 8:
                    Food.GenerateNewFood("Prefabs/8");
                    break;
                case 9:
                    Food.GenerateNewFood("Prefabs/9");
                    break;
            }            
            numberCombination += _randomNumber.ToString();
            Debug.Log(numberCombination);
        }        
    }

    public void Update()
    {
        // обновление отображаемого текста очков только при их изменении
        if (_lastPonts == points) return;

        _lastPonts = points;
        // форматируется очки 
        _pointsString = "Score: " + points.ToString("0000");

        //записуется сгенерированная комбинация
        text.text = "Комбинация: " + numberCombination; 
    }


    // отрисовка набранных очков
    public void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(20, 20, 200, 20), _pointsString ?? "");
    }              
}