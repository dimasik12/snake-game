using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// скрипту игрока необходим на объекте компонент CharacterController
// с помощью этого компонента будет выполняться движение
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // скорость перемещения - 6 единиц в секунду по умолчанию
    // в редакторе можно поменять
    public float speed = 3;
    Transform current;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    public GameObject HeadSnake;
    Tail tail, tailTail;
    //List<Tail> tail;
    private int lengthTail = 0;
    private Vector3 rotate;
    public Quaternion rot = Quaternion.identity;
    public GameObject BodyTail;
    private int numberFood;
    public int bonus = 1;
    public Text text;
    public float valueSpeed;

    public int points = 10;

    // аналогично скорость вращения 60 градусов в секунду по умолчанию
    public float rotationSpeed = 60;

    // локальная переменная для хранения ссылки на компонент CharacterController
    private CharacterController _controller;

    public void Start()
    {
        if (SelectIntricacy.intricacy == "easy")
        {
            HeadSnake.transform.position = new Vector3(-7.37f, 0.77f, -0.11f);
            valueSpeed = 0.1f;
        }
        else if (SelectIntricacy.intricacy == "average")
        {
            HeadSnake.transform.position = new Vector3(-14f, 0.77f, 14f);
            valueSpeed = 0.2f;       
        }
        else if (SelectIntricacy.intricacy == "complex")
        {
            HeadSnake.transform.position = new Vector3(-6f, 0.77f, 6f);
            valueSpeed = 0.3f;
        }
        else if (SelectIntricacy.intricacy == "profi")
        {
            HeadSnake.transform.position = new Vector3(0f, 0.77f, 0f);
            valueSpeed = 0.4f;
        }
        // получаем компонент CharacterController и 
        // записываем его в локальную переменную
        _controller = GetComponent<CharacterController>();

        _controller.Move(rotate * speed * Time.deltaTime/* * vertical*/);
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

        if (SelectCamera.typeCam == true)
        {
            // получаем значение вертикальной оси ввода
            /* float vertical = Input.GetAxis("Vertical"); */

            // получаем значение горизонтальной оси ввода
            float horizontal = Input.GetAxis("Horizontal");

            // вращаем трансформ вокруг оси Y 
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * horizontal);

            _testing = true; // маленкий хинт, для того, чтобы не обрабатывать несколько коллизий за кадр

            // движение выполняем с помощью контроллера в сторону, куда смотрит трансформ игрока
            // двигаем змею постоянно
            _controller.Move(-transform.right * speed * Time.deltaTime/* * vertical*/);
        }

        if (SelectCamera.typeCam == false)
        {
            _controller.Move(rotate * speed * Time.deltaTime/* * vertical*/);
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rot.eulerAngles = new Vector3(0, 0, 0);
                HeadSnake.transform.rotation = rot;
                rotate = -transform.right;

            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                rot.eulerAngles = new Vector3(0, 180, 0);
                HeadSnake.transform.rotation = rot;
                rotate = -transform.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rotate = -transform.right;
                rot.eulerAngles = new Vector3(0, 270, 0);
                HeadSnake.transform.rotation = rot;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rotate = -transform.right;
                rot.eulerAngles = new Vector3(0, 90, 0);
                HeadSnake.transform.rotation = rot;
            }
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);

        string nameFood = col.gameObject.name;
         switch (nameFood)
            {
                case "0(Clone)":
                    numberFood = 0;
                    break;
                case "1(Clone)":
                    numberFood = 1;
                    break;
                case "2(Clone)":
                    numberFood = 2;
                    break;
                case "3(Clone)":
                    numberFood = 3;
                    break;
                case "4(Clone)":
                    numberFood = 4;
                    break;
                case "5(Clone)":
                    numberFood = 5;
                    break;
                case "6(Clone)":
                    numberFood = 6;
                    break;
                case "7(Clone)":
                    numberFood = 7;
                    break;
                case "8(Clone)":
                    numberFood = 8;
                    break;
                case "9(Clone)":
                    numberFood = 9;
                    break;
            }
            Debug.Log(numberFood);

            if (numberFood == int.Parse(Game.numberCombination[0].ToString()))
                bonus++;
            else
                bonus = 1;

            Game.points += points * bonus;
            text.text = bonus.ToString();


           




        Food food = col.collider.GetComponent<Food>();

        switch (nameFood)
        {
            case "0(Clone)":
                numberFood = 0;
                break;
            case "1(Clone)":
                numberFood = 1;
                break;
            case "2(Clone)":
                numberFood = 2;
                break;
            case "3(Clone)":
                numberFood = 3;
                break;
            case "4(Clone)":
                numberFood = 4;
                break;
            case "5(Clone)":
                numberFood = 5;
                break;
            case "6(Clone)":
                numberFood = 6;
                break;
            case "7(Clone)":
                numberFood = 7;
                break;
            case "8(Clone)":
                numberFood = 8;
                break;
            case "9(Clone)":
                numberFood = 9;
                break;
            default:
                numberFood = 11;
                break;
        }

        if (numberFood < 11)
        {

            for (int i = 0; i < 10; i++)
            {
                //Debug.Log(int.Parse((Game.numberCombination[i]).ToString()));
                //Debug.Log(numberFood);
                if (int.Parse((Game.numberCombination[i]).ToString()) == numberFood)
                {
                    Debug.Log(int.Parse((Game.numberCombination[i]).ToString()));
                    Game.numberCombination = Game.numberCombination.Remove(i, 1);
                    break;
                }
            }


            //Game.points += 10;
            //Destroy(gameObject);
            //food.Eat();      

            // создаем примитив куб и добавляем ему компонент Tail
            lengthTail++;
            //tail = (GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Tail>());
         
            if (lengthTail == 1)
            {
                GameObject TailEnd = (GameObject)Instantiate(Resources.Load("Prefabs/EndTail"));                          
                tailTail = TailEnd.AddComponent<Tail>();
                //tailTail = (GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Tail>());                
                //tailTail.transform.position = current.transform.position - current.transform.forward * 2; 

                Vector3 pos = tailTail.transform.GetChild(0).position;
                pos.x += 0.31f;
                tailTail.transform.GetChild(0).position = pos;

                tailTail.transform.rotation = transform.rotation;
                tailTail.target = current.transform;
                tailTail.targetDistance = 0.1f;
                //tailTail.transform.Rotate(0, 180, 0);
                //Debug.Log(tailTail);
            }
            else if (lengthTail == 2)
            {
                GameObject BodyTail = (GameObject)Instantiate(Resources.Load("Prefabs/BodyTail2"));
                tail = BodyTail.AddComponent<Tail>();
                //Debug.Log("{1} tail" + tail);
                //Instantiate(Resources.Load("Prefabs/BodyTeil"))<Tail>//(GameObject)Instantiate(Resources.Load("Prefabs/BodyTeil"));
                //GameObject food = (GameObject)Instantiate(Resources.Load("Prefabs/Food", typeof(GameObject)));

                //tail.collider.isTrigger = true;

                // помещаем "хвост" за "хозяина"
                tail.transform.position = current.transform.position - current.transform.forward * 2;
                // ориентация хвоста как ориентация хозяина
                tail.transform.rotation = transform.rotation;
                // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
                tail.target = current.transform;
                // дистанция между элементами хвоста - 2 единицы
                tail.targetDistance = 2.4f;
                current = tail.transform;
                
                tailTail.transform.rotation = transform.rotation;
                tailTail.target = current.transform;
                tailTail.targetDistance = 0.01f;
                
                //Vector3 pos = tailTail.transform.GetChild(0).position;
                //pos.z += 1f;
                //pos.x += 0.05f;
                //tailTail.transform.GetChild(0).position = pos;
                
                
                //tailTail.transform.Rotate(0, 180, 0);
            }
            else
            {
                GameObject BodyTail = (GameObject)Instantiate(Resources.Load("Prefabs/BodyTail2"));
                tail = BodyTail.AddComponent<Tail>();
                //Debug.Log("{1} tail" + tail);
                //Instantiate(Resources.Load("Prefabs/BodyTeil"))<Tail>//(GameObject)Instantiate(Resources.Load("Prefabs/BodyTeil"));
                //GameObject food = (GameObject)Instantiate(Resources.Load("Prefabs/Food", typeof(GameObject)));

                //tail.collider.isTrigger = true;

                // помещаем "хвост" за "хозяина"
                tail.transform.position = current.transform.position - current.transform.forward * 2;              
                // ориентация хвоста как ориентация хозяина
                tail.transform.rotation = transform.rotation;
                // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
                tail.target = current.transform;
                // дистанция между элементами хвоста - 2 единицы
                tail.targetDistance = 2;
                current = tail.transform;


                tailTail.transform.position = current.transform.position - current.transform.forward * 2;
                tailTail.transform.rotation = transform.rotation;
                tailTail.target = current.transform;
                tailTail.targetDistance = 0.1f;
                //tailTail.transform.Rotate(0, 180, 0);
            }
            // удаляем с хвоста колайдер, так как он не нужен
            //Destroy(tail.collider);
            // следующим хозяином будет новосозданный элемент хвоста
            
            Debug.Log(current);

            speed = speed + valueSpeed; ;

            
            
            //current = tailTail.transform;


        }
        else
        {
            //Debug.Log(col.gameObject.name);
            //Debug.Log(lengthTail);
            if (Life1 != null)
            {                
                Destroy(Life1);
                //col.transform.position = new Vector3(0, 0, 0);
                //Destroy(HeadSnake);
                //GameObject snake = (GameObject)Instantiate(Resources.Load("Prefabs/Sphere", typeof(GameObject)));
                if (SelectIntricacy.intricacy == "easy")
                    HeadSnake.transform.position = new Vector3(-7.37f, 0.77f, -0.11f);
                else if (SelectIntricacy.intricacy == "average")
                    HeadSnake.transform.position = new Vector3(-14f, 0.77f, 14f);
                else if (SelectIntricacy.intricacy == "complex")
                    HeadSnake.transform.position = new Vector3(-6f, 0.77f, 6f);
                else if (SelectIntricacy.intricacy == "profi")
                    HeadSnake.transform.position = new Vector3(0f, 0.77f, 0f); 
                //for (int i = 0; i < lengthTail; i++)
                //{
                    //Destroy(tail.gameObject);
                //}
                //CreateTail(lengthTail);
            }
            else if (Life2 != null)
            {
                Destroy(Life2);
                if (SelectIntricacy.intricacy == "easy")
                    HeadSnake.transform.position = new Vector3(-7.37f, 0.77f, -0.11f);
                else if (SelectIntricacy.intricacy == "average")
                    HeadSnake.transform.position = new Vector3(-14f, 0.77f, 14f);
                else if (SelectIntricacy.intricacy == "complex")
                    HeadSnake.transform.position = new Vector3(-6f, 0.77f, 6f);
                else if (SelectIntricacy.intricacy == "profi")
                    HeadSnake.transform.position = new Vector3(0f, 0.77f, 0f);
            }
            else if (Life3 != null)
            {
                Destroy(Life3);
                if (SelectIntricacy.intricacy == "easy")
                    HeadSnake.transform.position = new Vector3(-7.37f, 0.77f, -0.11f);
                else if (SelectIntricacy.intricacy == "average")
                    HeadSnake.transform.position = new Vector3(-14f, 0.77f, 14f);
                else if (SelectIntricacy.intricacy == "complex")
                    HeadSnake.transform.position = new Vector3(-6f, 0.77f, 6f);
                else if (SelectIntricacy.intricacy == "profi")
                    HeadSnake.transform.position = new Vector3(0f, 0.77f, 0f);
            }
            else
            Application.LoadLevel("GameOver");
        }
    }

    public void RightButton()
    {
        rot.eulerAngles = new Vector3(0, 90, 0);
        HeadSnake.transform.rotation = rot;
        rotate = -transform.right;
    }

    public void LeftButton()
    {
        rot.eulerAngles = new Vector3(0, 270, 0);
        HeadSnake.transform.rotation = rot;
        rotate = -transform.right;
    }

    public void UpButton()
    {
        rot.eulerAngles = new Vector3(0, 0, 0);
        HeadSnake.transform.rotation = rot;
        rotate = -transform.right;
    }

    public void DownButton()
    {
        rot.eulerAngles = new Vector3(0, 180, 0);
        HeadSnake.transform.rotation = rot;
        rotate = -transform.right;
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