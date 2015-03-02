using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    // материал стен
    public Material wallMaterial;

    // набранные очки
    public static int points;

    // количество стен в уровне
    public int countWals = 10;

    private string _pointsString;
    private int _lastPonts = -1;
 
    public static string numberCombination;
    private int randomNumber;
    public Text text;    

    // генерируем уровень при загрузке сцены
    public void Awake()
    {
        switch (SelectIntricacy.intricacy)
        {
            case "easy":
            GameObject easy = (GameObject)Instantiate(Resources.Load("Prefabs/Lab1", typeof(GameObject)));
            easy.transform.position = new Vector3(0, 0, 0);
            break;
            case "average":
            GameObject average = (GameObject)Instantiate(Resources.Load("Prefabs/Lab2", typeof(GameObject)));
            average.transform.position = new Vector3(0, 0, 0);            
            break;
            case "complex":
            GameObject complex = (GameObject)Instantiate(Resources.Load("Prefabs/Lab3", typeof(GameObject)));
            complex.transform.position = new Vector3(0, 0, 0);
            break;
            case "profi":
            GameObject profi = (GameObject)Instantiate(Resources.Load("Prefabs/Lab4 1", typeof(GameObject)));
            profi.transform.position = new Vector3(0, 0, 0);
            break;
        }
        //GameObject intricacy = (GameObject)Instantiate(Resources.Load("Prefabs/Lab2_fbx", typeof(GameObject)));
        //intricacy.transform.position = new Vector3(0, 0, 0);  

        //text = GetComponent<Text>();
        // обнуляем очки
        points = 0;

        //задаем начальную комбинацию
        for (int i = 0; i < 10; i++)
        {
            randomNumber = Random.Range(0, 9);
            switch(randomNumber)
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
            numberCombination += randomNumber.ToString();
            Debug.Log(numberCombination);
        }
        //text.text = numberCombination;
        // генерируем уровень
        //GenerateLevel();

        // ставим первую еду
        //for (int i = 0; i < 10; i++)
            //Food.GenerateNewFood("Prefabs/Food");
    }

    public void Update()
    {
        // обновление отображаемого текста очков только при их изменении
        if (_lastPonts == points) return;

        _lastPonts = points;
        // форматируем очки в формате четырех цифр, начинающихся с нулей
        _pointsString = "Score: " + points.ToString("0000");

        text.text = numberCombination;
    }


    // отрисовка набранных очков
    public void OnGUI()
    {
        GUI.color = Color.yellow;
        GUI.Label(new Rect(20, 20, 200, 20), _pointsString ?? "");
    }

    // функция генерации уровня
    private void GenerateLevel()
    {
        for (int i = 0; i < countWals; i++)
        {
            // создаем куб
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // называем его "Wall"
            wall.name = "Wall";
            // увеличиваем его габариты
            wall.transform.localScale = new Vector3(2, 2, 2);

            // расставляем его так, чтобы координаты были не в центре игрового поля
            var pos = new Vector3(Random.Range(-25, 25), 0, Random.Range(-49, 49));
            while (Mathf.Abs(pos.x) < 10 || Mathf.Abs(pos.z) < 10)
            {
                pos = new Vector3(Random.Range(-25, 25), 0, Random.Range(-49, 49));
            }
            wall.transform.position = pos;
            // и назначаем материал
            wall.renderer.material = wallMaterial;

            wall.collider.isTrigger = true;
        }

    }  
        
}