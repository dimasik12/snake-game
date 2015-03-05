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

   // Dictionary<string,string> dictionary = new Dictionary<string,string>();  попробовать реализовать хеш мап в с шарпе
        
     //       dictionary.Add("easy", "Prefabs/Lab1");
        
    

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
            Food.GenerateNewFood("Prefabs/" + _randomNumber);           
            numberCombination += _randomNumber.ToString();
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