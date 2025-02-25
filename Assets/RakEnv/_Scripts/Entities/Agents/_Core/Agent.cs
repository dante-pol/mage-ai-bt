using UnityEngine;
using UnityEngine.AI;

namespace Root
{


    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        public bool IsLife { get; private set; }

        public bool IsZombi {  get; private set; }

        public float Mana = 100;

        public float HeatPoint = 100;

        public AgentMotion Motion;

        public EntitiesBroker EntitiesBroker;

        public AgentEyes Eyes;

        private Brain _brain;

        public AgentAnimator Animator;

        private void Awake()
        {
            IsLife = true;

            IsZombi = false;

            var agent = GetComponent<NavMeshAgent>();

            var animator = transform.GetChild(0).GetComponent<Animator>();

            Motion = new AgentMotion(agent);

            Eyes = new AgentEyes(transform);

            Animator = new AgentAnimator(animator);

            Eyes.SetTarget(EntitiesBroker.Player);

            _brain = new Brain(this);
        }

        private void Update()
        {
            Motion.Update();

            Eyes.Update();

            _brain.Update();

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

            // Применяем стиль при отрисовке меток
            GUILayout.Label($"Heat Point: {HeatPoint}", labelStyle);
            GUILayout.Label($"Mana: {Mana}", labelStyle);

            if (GUILayout.Button("Up Heat Point"))
            {
                HeatPoint = 100;
            }

            if (GUILayout.Button("Low Heat Point"))
            {
                HeatPoint = 19;
            }

            if (GUILayout.Button("UP Mana"))
            {
                Mana = 100;
            }

            if (GUILayout.Button("Low Mana"))
            {
                Mana = 69;
            }

            if (GUILayout.Button("Spawn Agent"))
            {
                transform.position = Vector3.zero;
            }

            GUILayout.EndVertical();
        }
    }

    public class AgentAttacker
    {

    }
}