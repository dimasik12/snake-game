using UnityEngine;

public class Food : MonoBehaviour
{
    // функция создания новой еды
    public static void GenerateNewFood(string typeFood)
    {
        // создаем экземпляр еды, предварительно загружая префаб из ресурсов
        GameObject food = (GameObject)Instantiate(Resources.Load(typeFood, typeof(GameObject)));

        // цикл подбора положения еды
        while (true)
        {
            // ставим еду в рандомное место
            if (SelectIntricacy.intricacy == "profi")
                food.transform.position = new Vector3(Random.Range(-30, 30), 1, Random.Range(-30, 30));
            else
            food.transform.position = new Vector3(Random.Range(-35, 35), 1, Random.Range(-35, 35));
            // получаем размер ее колайдера в мировых координатах
            Bounds foodBounds = food.collider.bounds;

            bool intersects = false;

            // Проверяем со всеми колайдерами кроме колайдера самой еды           
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