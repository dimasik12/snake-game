using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {
    // Grow in next movement?
    bool ate = false;  
    // Tail Prefab
    public GameObject tailPrefab;

    private bool _testing = false;

    // Current Movement Direction
    // (by default it moves to the right)
    Vector3 dir = Vector3.right;

	// Use this for initialization
	void Start () {
        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            dir = Vector3.right; // '-up' means 'down'
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -Vector3.forward; // '-right' means 'left'
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            dir = -Vector3.right;
        }

        _testing = true;
	
	}

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();
    void Move ()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }

        // Do we have a Tail?
        if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;
            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_testing)
        {
            Food food = hit.collider.GetComponent<Food>();
            if (food != null)
            {
                // врезались в еду, "съедаем" ее
                //food.Eat();
            }
            else
            {
                // врезались не в еду
                //Application.LoadLevel("GameOver");
                Application.LoadLevel("MainMenu");
            }
            _testing = false;
        }
    }
}
