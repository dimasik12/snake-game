﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static float speed = 3;
    Transform current;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    public GameObject HeadSnake;
    Tail tail, tailTail;
    private int lengthTail = 0;
    private Vector3 rotate;
    public Quaternion rot = Quaternion.identity;
    public GameObject BodyTail;
    private int numberFood;
    public int bonus = 1;
    public Text text;
    public float valueSpeed;

    public int points = 10;

    public AudioClip clip;

    // скорость вращения 60 градусов в секунду по умолчанию
    public float rotationSpeed = 60;

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
        rotate = transform.forward;
        // записывается CharacterController в локальную переменную
        _controller = GetComponent<CharacterController>();

        _controller.Move(rotate * speed * Time.deltaTime/* * vertical*/);
        // создание хвост
        // current - текущая цель элемента хвоста, начинаем с головы
         current = transform;      
 
    }

    public void Update()    {
        
        if (SelectCamera.typeCam == true)
        { 
            // значение горизонтальной оси ввода
            float horizontal = Input.GetAxis("Horizontal");

            // вращение трансформ вокруг оси Z 
            transform.Rotate(0, rotationSpeed * Time.deltaTime * horizontal, 0);            

            // движение выполняем с помощью контроллера в сторону, куда смотрит трансформ игрока
            // двигаем змею постоянно
            _controller.Move(transform.forward * speed * Time.deltaTime/* * vertical*/);
        }

        // если выбрана камера с видом сверху
        if (SelectCamera.typeCam == false)
        {
            _controller.Move(rotate * speed * Time.deltaTime/* * vertical*/);
            // проверка на нажатие клавишь WSAD и стрелок
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rot.eulerAngles = new Vector3(0, 270, 0);
                HeadSnake.transform.rotation = rot;
                rotate = transform.forward;

            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                rot.eulerAngles = new Vector3(0, 90, 0);
                HeadSnake.transform.rotation = rot;
                rotate = transform.forward;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rot.eulerAngles = new Vector3(0, 180, 0);
                HeadSnake.transform.rotation = rot;
                rotate = transform.forward;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rot.eulerAngles = new Vector3(0, 0, 0);
                HeadSnake.transform.rotation = rot;
                rotate = transform.forward;
            }
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        audio.PlayOneShot(clip);
        // какая цыфры была сьедена
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
                default:
                    numberFood = 11;
                    break;
            }           

            if (numberFood == int.Parse(Game.numberCombination[0].ToString()))
                bonus++;
            else
                bonus = 1;
            
            // прибавление очков с учетом бонуса
            Game.points += points * bonus;
            text.text = "Бонус: х "+bonus.ToString();     
           
        // если змея врезалась в еду
        if (numberFood < 11)
        {

            for (int i = 0; i < 10; i++)
            {
                if (int.Parse((Game.numberCombination[i]).ToString()) == numberFood)
                {
                    // удаление цыфры из комбинации
                    Game.numberCombination = Game.numberCombination.Remove(i, 1);
                    break;
                }
            }    

            // длина хвоста
            lengthTail++;
         
            // прибавление 1 элемента хвоста
            if (lengthTail == 1)
            {
                GameObject TailEnd = (GameObject)Instantiate(Resources.Load("Prefabs/snake_tail2"));                          
                tailTail = TailEnd.AddComponent<Tail>(); 

                Vector3 pos = tailTail.transform.GetChild(2).position;
                Vector3 pos1 = tailTail.transform.GetChild(1).position;
                Vector3 pos2 = tailTail.transform.GetChild(0).position;  
                pos1.x -= 0.31f;
                pos2.x += 0.31f;
                pos.z += 1.5f;
                pos1.z += 1.5f;
                pos2.z += 1.5f;
                tailTail.transform.GetChild(2).position = pos;
                tailTail.transform.GetChild(1).position = pos2;
                tailTail.transform.GetChild(0).position = pos1;

                tailTail.transform.rotation = transform.rotation;
                tailTail.target = current.transform;
                tailTail.targetDistance = 0.1f;
            }
            // прибавление второго элемента хвоста
            else if (lengthTail == 2)
            {
                GameObject BodyTail = (GameObject)Instantiate(Resources.Load("Prefabs/snake_center2"));
                tail = BodyTail.AddComponent<Tail>();              

                // помещаем "хвост" за "хозяина"
                tail.transform.position = current.transform.position - current.transform.forward * 2;
                // ориентация элемента тела как ориентация хозяина
                tail.transform.rotation = transform.rotation;
                // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
                tail.target = current.transform;
                // дистанция между элементами хвоста
                tail.targetDistance = 0.1f;
                current = tail.transform;

                // ориентация конца хвоста как ориентация хозяина
                tailTail.transform.rotation = transform.rotation;
                // указываем на хозяина
                tailTail.target = current.transform;
                // дистанция между элементом тела и конца хвоста
                tailTail.targetDistance = 1.5f;              
            }
            // прибавление всех остальных кусков тела
            else
            {
                GameObject BodyTail = (GameObject)Instantiate(Resources.Load("Prefabs/snake_center2"));
                tail = BodyTail.AddComponent<Tail>();                

                // помещаем "хвост" за "хозяина"
                tail.transform.position = current.transform.position - current.transform.forward * 2;              
                // ориентация как ориентация хозяина
                tail.transform.rotation = transform.rotation;
                // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
                tail.target = current.transform;
                // дистанция между элементами хвоста - 2 единицы
                tail.targetDistance = 1.5f;
                current = tail.transform;

                tailTail.transform.position = current.transform.position - current.transform.forward * 2;
                // ориентация конца хвоста как хозяина
                tailTail.transform.rotation = transform.rotation;
                // указываем на хозяина
                tailTail.target = current.transform;
                // дистанция между элементом тела и конца хвоста
                tailTail.targetDistance = 1.5f;               
            }         
            // увеличение скорости после поедания еды            
            speed = speed + valueSpeed; ;
        }
        // если змея наткнулась не на еду
        else
        {
            // если есть все жизни, то - 1-я
            if (Life1 != null)
            {                
                Destroy(Life1);
                StartPositionSnake(SelectIntricacy.intricacy);             
            }
            // - 2-я жизнь
            else if (Life2 != null)
            {
                Destroy(Life2);
                StartPositionSnake(SelectIntricacy.intricacy); 
            }
            // - 3-я жизнь
            else if (Life3 != null)
            {
                Destroy(Life3);
                StartPositionSnake(SelectIntricacy.intricacy);
            }
            // если жизни законцились, то загрузка сцены "Game over"
            else
            {
                speed = 3;
                Application.LoadLevel("GameOver");
            }
        }
    }

    private void StartPositionSnake(string intricacyLab)
    {
        if (intricacyLab == "easy")
            HeadSnake.transform.position = new Vector3(-7.37f, 0.77f, -0.11f);
        else if (intricacyLab == "average")
            HeadSnake.transform.position = new Vector3(-14f, 0.77f, 14f);
        else if (intricacyLab == "complex")
            HeadSnake.transform.position = new Vector3(-6f, 0.77f, 6f);
        else if (intricacyLab == "profi")
            HeadSnake.transform.position = new Vector3(0f, 0.77f, 0f);
    }

    public void RightButton()
    {
        rot.eulerAngles = new Vector3(0, 0, 0);
        HeadSnake.transform.rotation = rot;
        rotate = transform.forward;
    }

    public void LeftButton()
    {
        rot.eulerAngles = new Vector3(0, 180, 0);
        HeadSnake.transform.rotation = rot;
        rotate = transform.forward;
    }

    public void UpButton()
    {
        rot.eulerAngles = new Vector3(0, 270, 0);
        HeadSnake.transform.rotation = rot;
        rotate = transform.forward;
    }

    public void DownButton()
    {
        rot.eulerAngles = new Vector3(0, 90, 0);
        HeadSnake.transform.rotation = rot;
        rotate = transform.forward;
    }                    
}