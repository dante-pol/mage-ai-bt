using UnityEngine;

namespace Root
{
    public class MeleeEscape
    {
        public bool IsSelect = false;

        public bool HasEscape { get; set; }

        public Transform[] _points;

        private Transform _escapePoint;

        public MeleeEscape()
        {
            var result = GameObject.FindGameObjectsWithTag("Point");

            _points = new Transform[result.Length];

            for (int i = 0; i < result.Length; i++)
            {
                _points[i] = result[i].transform;
            }

            HasEscape = false;
        }

        public Transform GetEscapePoint() 
            => _escapePoint;

        public void ChooseEscapePoint()
        {
            int indexRandom = Random.Range(0, _points.Length-1);

            _escapePoint = _points[indexRandom];

            IsSelect = true;
        }

        public void Reset()
        {
            IsSelect = false;
        }
    }
}