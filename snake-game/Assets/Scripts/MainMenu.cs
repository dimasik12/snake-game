using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Размер меню
    public Vector2 menuSize = new Vector2(500, 300);

    // минимальная высота кнопки
    public float buttonMinHeight = 60f;

    // шрифт заголовка
    public Font captionFont;

    // шрифт кнопок
    public Font buttonFont;

    // тексты меню
    public string mainMenuText = "Main menu";
    public string startButtonText = "Start game";
    public string exitButtonText = "Exit";

    public void OnGUI()
    {
        // рассчитываем прямоугольник по центру экрана с заданным размером
        Rect rect = new Rect(
            Screen.width / 2f - menuSize.x / 2,
            Screen.height / 2f - menuSize.y / 2,
            menuSize.x,
            menuSize.y);

        // область меню
        GUILayout.BeginArea(rect, GUI.skin.textArea);
        {
            // создаем стиль заголовка на основе стиля label стандартного скина
            GUIStyle captionStyle = new GUIStyle(GUI.skin.label);
            // устанавливаем стилю заголовка шрифт captionFont
            captionStyle.font = captionFont;
            // Рассположение текста по центру
            captionStyle.alignment = TextAnchor.MiddleCenter;

            // текст заголовка
            GUILayout.Label(mainMenuText, captionStyle);

            // создаем стиль кнопки на основе стиля button стандартного скина
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            // устанавливаем стилю кнопки шрифт buttonFont
            buttonStyle.font = buttonFont;
            // отступы кнопок от краев
            buttonStyle.margin = new RectOffset(20, 20, 3, 3);


            // FlexibleSpace - автоматически рассчитанное место, необходимое для 
            // заполнения пустого пространства между элементами управления

            GUILayout.FlexibleSpace(); // динамическоем пространство между заголовком и кнопкой старт

            // отрисовка кнопки Start и обработка ее нажатия
            if (GUILayout.Button(startButtonText, buttonStyle, GUILayout.MinHeight(buttonMinHeight)))
            {
                // загрузка сцены с именем Level
                Application.LoadLevel("Game");
            }

            GUILayout.FlexibleSpace(); // динамическоем пространство между кнопками

            // отрисовка кнопки Exit и обработка ее нажатия
            if (GUILayout.Button(exitButtonText, buttonStyle, GUILayout.MinHeight(buttonMinHeight)))
            {
                // выход
                Application.Quit();
            }

            GUILayout.FlexibleSpace(); // динамическое пространство между кнопкой Exit и низом области меню

        }
        GUILayout.EndArea();

    }
}