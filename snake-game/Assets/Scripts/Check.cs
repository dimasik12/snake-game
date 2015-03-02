using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    public Text text;
    private int randomNumber;

    

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {        
        Debug.Log(col.gameObject.name);
        Food food = col.collider.GetComponent<Food>();
        if (col != null)
        {            
            Destroy(gameObject);
            

           
            }

            randomNumber = Random.Range(0, 9);
            switch (randomNumber)
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
            Game.numberCombination += randomNumber.ToString(); ; 
            

            //Food.GenerateNewFood("Prefabs/Food");          
        
    }
}


