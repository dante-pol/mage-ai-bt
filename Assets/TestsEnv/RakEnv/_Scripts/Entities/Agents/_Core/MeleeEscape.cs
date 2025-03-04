using UnityEngine;

namespace Root
{
    public class MeleeEscape
    {
        public bool IsSelect = false;

        public bool HasEscape { get; set; }

        private MeleeMotion _motion;

        public Transform[] _points;

        private Transform _escapePoint;

        public MeleeEscape(MeleeMotion motion)
        {
            _motion = motion;

            var result = GameObject.FindGameObjectsWithTag("Point");

            _points = new Transform[result.Length];

            for (int i = 0; i < result.Length; i++)
            {
                _points[i] = result[i].transform;
            }

            HasEscape = false;
        }

        public void Run()
        {
            _motion.SetTarget(_escapePoint);
        }

        public void ChooseEscapePoint()
        {
            int indexRandom = Random.Range(0, _points.Length-1);

            _escapePoint = _points[indexRandom];

            IsSelect = true;
        }
    }
}