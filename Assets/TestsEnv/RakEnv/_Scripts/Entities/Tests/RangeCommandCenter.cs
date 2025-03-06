using Root.Core.Entities.Agents.Range;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Root.Tests
{
    public class RangeCommandCenter
    {
        public int LevelRangeAgent
        {
            get
            {
                return _levelRangeAgent;
            }
            set
            {
                if (_levelRangeAgent == 5)
                    return;

                _levelRangeAgent = value;

                UpdateRangeProgress();
            }

        }

        private readonly RangeAgentFactory _rangeFactory;

        private List<RangeAgent> _ranges;

        private int _levelRangeAgent;

        public RangeCommandCenter(RangeAgentFactory rangeFactory)
        {
            _rangeFactory = rangeFactory;

            _ranges = new List<RangeAgent>();
        }

        public void RunSpawn()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("Range").ToArray().Select((obj) => obj.transform);

            foreach (var spawnPoint in spawnPoints)
            {
                RangeAgent agent = _rangeFactory.Create(spawnPoint.position, spawnPoint.rotation) as RangeAgent;

                agent.DeathEvent += () => { LevelRangeAgent++; };

                _ranges.Add(agent);
            }
        }

        private void UpdateRangeProgress()
        {
            foreach (RangeAgent range in _ranges)
            {
                range.UpdateProgress();
            }
        }

    }
}