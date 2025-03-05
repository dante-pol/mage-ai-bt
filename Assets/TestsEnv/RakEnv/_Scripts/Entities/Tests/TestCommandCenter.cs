using Root.Core.Entities.Agents.Range;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Root.Tests
{
    public class TestCommandCenter : MonoBehaviour
    {
        public bool IsOneMelee { get; private set; }

        private RangeAgentFactory _rangeFactory;
        private MeleeAgentFactory _meleeFactory;

        private List<RangeAgent> _ranges;

        public int LevelRangeAgent
        {
            get
            {
                return _levelRangeAgent;
            }
            set
            {
                _levelRangeAgent = value;

                UpdateRangeProgress();
            }

        }

        private void UpdateRangeProgress()
        {
            foreach (RangeAgent range in _ranges)
            {
                range.UpdateProgress();
            }
        }

        private int _levelRangeAgent;

        private void Awake()
        {
            _rangeFactory = new RangeAgentFactory(this);

            _meleeFactory = new MeleeAgentFactory(this);

            _ranges = new List<RangeAgent>();

            StartingSpawnMelee();
            SpawnRange();

        }

        public void SpawnMelee()
        {

        }

        public void StartingSpawnMelee()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("MeleeS").ToArray().Select((obj) => obj.transform);

            foreach (var spawnPoint in spawnPoints)
            {
                _meleeFactory.Create(spawnPoint.position, spawnPoint.rotation);
            }
        }

        public void SpawnRange()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("Range").ToArray().Select((obj) => obj.transform);

            foreach (var spawnPoint in spawnPoints)
            {
                RangeAgent agent = _rangeFactory.Create(spawnPoint.position, spawnPoint.rotation) as RangeAgent;

                agent.DeathEvent += () => { LevelRangeAgent++; };
                
                _ranges.Add(agent);
            }
        }
    }
}