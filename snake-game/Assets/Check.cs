using UnityEngine;
using System.Collections;

public class Check : MonoBehaviour
{
    public int points = 10;

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
            Game.points += points;
            Food.GenerateNewFood();          
        }
    }
}


