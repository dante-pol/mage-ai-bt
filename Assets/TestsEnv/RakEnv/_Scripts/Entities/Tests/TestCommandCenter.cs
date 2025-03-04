using Root.Core.Entities.Agents.Range;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Root.Tests
{
    public class TestCommandCenter : MonoBehaviour
    {
        public List<MeleeAgent> _melees;
        public List<RangeAgent> _ranges;

        private void Awake()
        {
            _ranges = FindObjectsOfType<RangeAgent>().ToList();

            foreach (var range in _ranges)
            {
                range.Construct();
            }
        }
    }
}