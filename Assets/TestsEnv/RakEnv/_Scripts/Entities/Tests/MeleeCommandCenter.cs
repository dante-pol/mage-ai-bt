using Root.Core.Entities.Agents.Range;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Root.Tests
{

    public class MeleeCommandCenter
    {
        public bool IsOneMelee
        {
            get
            {
                if (_allMelee.Count == 1) return true;
                else return false;
            }
        }

        private readonly MeleeAgentFactory _meleeFactory;
        private readonly MonoBehaviour _coroutineRunner;

        private float _timeBetweenSpawn;
       
        private List<MeleeAgent> _allMelee;
        private List<Transform> _spawnPointsForPeriod;

        public MeleeCommandCenter(MonoBehaviour coroutineRunner)
        {
            _timeBetweenSpawn = 15;

            _coroutineRunner = coroutineRunner;
        }

        public void RunPeriodsSpawn()
        {
            _spawnPointsForPeriod = GameObject.FindGameObjectsWithTag("Melee").ToArray().Select((obj) => obj.transform).ToList();

            _coroutineRunner.StartCoroutine(Spawning());
        }


        public void RunSingleSpawn()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("MeleeS").ToArray().Select((obj) => obj.transform);

            foreach (var spawnPoint in spawnPoints)
            {
                var melee = _meleeFactory.Create(spawnPoint.position, spawnPoint.rotation);

                _allMelee.Add(melee as MeleeAgent);
            }
        }

        private IEnumerator Spawning()
        {
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    foreach (var spawnPoint in _spawnPointsForPeriod)
                    {
                        var melee = _meleeFactory.Create(spawnPoint.position, spawnPoint.rotation);

                        _allMelee.Add(melee as MeleeAgent);

                        yield return null;
                    }
                }

                yield return new WaitForSeconds(_timeBetweenSpawn);
            }
        }

    }

}