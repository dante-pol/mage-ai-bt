using UnityEngine;

namespace Root
{
    public class AgentEscape
    {
        public bool IsSelect = false;

        private AgentMotion _motion;

        public Transform[] _points;

        private Transform _escapePoint;

        public AgentEscape(AgentMotion motion)
        {
            _motion = motion;

            var result = GameObject.FindGameObjectsWithTag("Point");

            _points = new Transform[result.Length];

            for (int i = 0; i < result.Length; i++)
            {
                _points[i] = result[i].transform;
            }
        }

        public void Run()
        {
            _motion.SetTarget(_escapePoint);
        }

        public void ChooseEscapePoint()
        {
            int indexRandom = UnityEngine.Random.Range(0, _points.Length-1);

            _escapePoint = _points[indexRandom];

            IsSelect = true;
        }
    }
}