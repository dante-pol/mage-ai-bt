using UnityEngine;

namespace Root
{
    public class AgentCommandCenter : MonoBehaviour
    {
        public Agent FirstAgent;

        public Agent SecondAgent;

        public bool IsOneAgent;

        private void Start()
        {
            IsOneAgent = false;
        }

        public void DeadSecondAgent()
        {
            SecondAgent.gameObject.SetActive(false);

            IsOneAgent = true;

            Debug.Log($"IsOneAgent: {IsOneAgent}");
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();


            // Создаем и настраиваем стиль для меток
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 36 // Увеличенный размер шрифта
            };
            labelStyle.normal.textColor = Color.black;  // Изменяем цвет текста

            if (GUILayout.Button("Dead Second Agent"))
            {
                DeadSecondAgent();
            }

            GUILayout.EndVertical();
        }
    }
}