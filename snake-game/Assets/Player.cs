using UnityEngine;
using System.Collections.Generic;

// скрипту игрока необходим на объекте компонент CharacterController
// с помощью этого компонента будет выполняться движение
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // скорость перемещения - 6 единиц в секунду по умолчанию
    // в редакторе можно поменять
    public float speed = 6;
    Transform current;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    public GameObject HeadSnake;
    Tail tail;
    //List<Tail> tail;
    private int lengthTail = 0;
    private Vector3 rotate;
    public Quaternion rot = Quaternion.identity;

    // аналогично скорость вращения 60 градусов в секунду по умолчанию
    public float rotationSpeed = 60;

    // локальная переменная для хранения ссылки на компонент CharacterController
    private CharacterController _controller;

    public void Start()
    {
        // получаем компонент CharacterController и 
        // записываем его в локальную переменную
        _controller = GetComponent<CharacterController>();

        rotate = transform.forward;
        // создаем хвост
        // current - текущая цель элемента хвоста, начинаем с головы
       current = transform;
       
 /*
        for (int i = 0; i < 3; i++)
        {
            // создаем примитив куб и добавляем ему компонент Tail
            Tail tail = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Tail>();
            // помещаем "хвост" за "хозяина"
            tail.transform.position = current.transform.position - current.transform.forward * 2;
            // ориентация хвоста как ориентация хозяина
            tail.transform.rotation = transform.rotation;
            // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
            tail.target = current.transform;
            // дистанция между элементами хвоста - 2 единицы
            tail.targetDistance = 2;
            // удаляем с хвоста колайдер, так как он не нужен
            Destroy(tail.collider);
            // следующим хозяином будет новосозданный элемент хвоста
            current = tail.transform;
        }
         */
    }

    private bool _testing = false;

    public void Update()
    {
        /* 
         * Гибкий способ - использовать оси
         * Unity имеет набор предустоновленных осей, которые можно использовать
         * следующий код будет работать как на клавиатуре (стрелки и WSAD), так и на геймпаде
         */

        // получаем значение вертикальной оси ввода
        /* float vertical = Input.GetAxis("Vertical"); */

        // получаем значение горизонтальной оси ввода
        float horizontal = Input.GetAxis("Horizontal");
     
        // вращаем трансформ вокруг оси Y 
        //transform.Rotate(0, rotationSpeed * Time.deltaTime * horizontal, 0);

        _testing = true; // маленкий хинт, для того, чтобы не обрабатывать несколько коллизий за кадр

        // движение выполняем с помощью контроллера в сторону, куда смотрит трансформ игрока
        // двигаем змею постоянно
        _controller.Move(rotate * speed * Time.deltaTime/* * vertical*/);
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rot.eulerAngles = new Vector3(0, 270, 0);
            HeadSnake.transform.rotation = rot;
            rotate = transform.forward;
                        
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rot.eulerAngles = new Vector3(0, 90, 0);
            HeadSnake.transform.rotation = rot;
            rotate = transform.forward;           
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotate = transform.forward;
            rot.eulerAngles = new Vector3(0, 180, 0);
            HeadSnake.transform.rotation = rot;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotate = transform.forward;
            rot.eulerAngles = new Vector3(0, 0, 0);
            HeadSnake.transform.rotation = rot;
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        Food food = col.collider.GetComponent<Food>();
        if (col.name.StartsWith("Food(Clone)"))
        {
            //Game.points += 10;
            //Destroy(gameObject);
            //food.Eat();      

            // создаем примитив куб и добавляем ему компонент Tail
            lengthTail++;
            tail = (GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Tail>());

            tail.collider.isTrigger = true;

            // помещаем "хвост" за "хозяина"
            tail.transform.position = current.transform.position - current.transform.forward * 2;
            // ориентация хвоста как ориентация хозяина
            tail.transform.rotation = transform.rotation;
            // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
            tail.target = current.transform;
            // дистанция между элементами хвоста - 2 единицы
            tail.targetDistance = 2;
            // удаляем с хвоста колайдер, так как он не нужен
            //Destroy(tail.collider);
            // следующим хозяином будет новосозданный элемент хвоста
            current = tail.transform;

            speed = speed + 0.1f;

        }
        else
        {
            Debug.Log(col.gameObject.name);
            Debug.Log(lengthTail);
            if (Life1 != null)
            {                
                Destroy(Life1);
                //col.transform.position = new Vector3(0, 0, 0);
                //Destroy(HeadSnake);
                //GameObject snake = (GameObject)Instantiate(Resources.Load("Prefabs/Sphere", typeof(GameObject)));
                HeadSnake.transform.position = new Vector3(0, 0, 0);  
                //for (int i = 0; i < lengthTail; i++)
                //{
                    //Destroy(tail.gameObject);
                //}
                //CreateTail(lengthTail);
            }
            else if (Life2 != null)
            {
                Destroy(Life2);
                HeadSnake.transform.position = new Vector3(0, 0, 0);
            }
            else if (Life3 != null)
            {
                Destroy(Life3);
                HeadSnake.transform.position = new Vector3(0, 0, 0);
            }
            else
            Application.LoadLevel("GameOver");
        }
    }
    /*
    void CreateTail(int lengthTail)
    {
        List<Tail> tail = new List<Tail>();       
        for (int i = 0; i < lengthTail; i++)
        {
            // создаем примитив куб и добавляем ему компонент Tail
            tail.Add(GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Tail>());
            // помещаем "хвост" за "хозяина"
            tail[i].transform.position = current.transform.position - current.transform.forward * 2;
            // ориентация хвоста как ориентация хозяина
            tail[i].transform.rotation = transform.rotation;
            // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
            tail[i].target = current.transform;
            // дистанция между элементами хвоста - 2 единицы
            tail[i].targetDistance = 2;
            // удаляем с хвоста колайдер, так как он не нужен
            Destroy(tail[i].collider);
            // следующим хозяином будет новосозданный элемент хвоста
            current = tail[i].transform;
        }
    }
     */ 
   
    /*
    // В данную функцию будут передаваться все объекты, с которыми
    // CharacterController вступает в столкновения
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {       
        if (_testing)
        {            
            Food food = hit.controller.GetComponent<Food>();
            if (hit.gameObject.name == "Food(Clone)")
            {
                // врезались в еду, "съедаем" ее
                //food.Eat();

                // создаем примитив куб и добавляем ему компонент Tail
                
                Tail tail = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Tail>();
                
                // помещаем "хвост" за "хозяина"
                tail.transform.position = current.transform.position - current.transform.forward * 2;
                // ориентация хвоста как ориентация хозяина
                tail.transform.rotation = transform.rotation;
                // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
                tail.target = current.transform;
                // дистанция между элементами хвоста - 2 единицы
                tail.targetDistance = 2;
                // удаляем с хвоста колайдер, так как он не нужен
                Destroy(tail.collider);
                // следующим хозяином будет новосозданный элемент хвоста
                current = tail.transform;
                
            }
            else
            {
                Debug.Log(hit.gameObject.name);
                // врезались не в еду
                //Application.LoadLevel("GameOver");
                Application.LoadLevel("MainMenu");
            }
            _testing = false;
        }
      
    }*/
}