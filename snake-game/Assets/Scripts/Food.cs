using UnityEngine;

public class Food : MonoBehaviour
{
    // количество очков, которое дает еда при съедании
    public int points = 10;

    public void Update()
    {
        // вращаем еду со скоростью 60 градусов в секунду
        transform.Rotate(Vector3.up, 60 * Time.deltaTime);
    }

    public void Eat()
    {
        // прибавляем очки еды к общему числу очков
        Game.points += points;

        // уничтожаем объект еды
        Destroy(gameObject);

        // Генерируем новую еду
        GenerateNewFood();
    }

    // функция создания новой еды
    public static void GenerateNewFood()
    {
        // создаем экземпляр еды, предварительно загружая префаб из ресурсов
        GameObject food = (GameObject)Instantiate(Resources.Load("Prefabs/Food", typeof(GameObject)));

        // цикл подбора положения еды
        while (true)
        {
            // ставим еду в рандомное место
            food.transform.position = new Vector3(Random.Range(-24, 24), 0, Random.Range(-50, 50));
            // получаем размер ее колайдера в мировых координатах
            Bounds foodBounds = food.collider.bounds;

            bool intersects = false;

            // Проверяем со всеми колайдерами кроме колайдера самой еды.
            // Данная фукнция использует габаритные контейнеры колайдеров для
            // сравнения. Если используются сложные колайдеры в уровне, то
            // данное сравнение будет не верным.
            foreach (Collider objectColiider in FindObjectsOfType(typeof(Collider)))
            {
                if (objectColiider != food.collider)
                {
                    // если пересекается, то завершаем цикл, досрочно
                    if (objectColiider.bounds.Intersects(foodBounds))
                    {
                        intersects = true;
                        break;
                    }
                }
            }

            // установили в нужное место, останавливаем цикл установки
            if (!intersects)
            {
                break;
            }
        }
    }
}