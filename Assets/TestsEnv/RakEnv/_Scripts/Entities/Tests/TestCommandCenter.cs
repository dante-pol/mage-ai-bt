using Root.Core.Entities.Agents.Range;
using System.Linq;
using UnityEngine;

namespace Root.Tests
{
    public class TestCommandCenter : MonoBehaviour
    {
        private RangeAgentFactory _rangeFactory;
        private MeleeAgentFactory _meleeFactory;

        public bool IsOneMelee { get; private set; }

        private void Awake()
        {
            _rangeFactory = new RangeAgentFactory();

            _meleeFactory = new MeleeAgentFactory(this);

            StartingSpawnMelee();
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
                _rangeFactory.Create(spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}