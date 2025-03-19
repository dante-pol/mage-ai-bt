using System;
using System.Collections;
using UnityEngine;

public class SpellBallExplosion
{
    private readonly MonoBehaviour _coroutineRunner;

    private readonly ParticleSystem _explosionSystem;

    private readonly GameObject _mesh;

    public SpellBallExplosion(ParticleSystem explosionSystem, GameObject mesh, MonoBehaviour coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        
        _explosionSystem = explosionSystem;

        _mesh = mesh;
    }

    public void Reset()
    {
        _mesh.SetActive(true);

        _explosionSystem.gameObject.SetActive(false);
    }

    public void ActiveExplosion(Action callBack)
    {
        _coroutineRunner.StartCoroutine(BlowUp(callBack));
    }

    private IEnumerator BlowUp(Action callBack)
    {
        _mesh.SetActive(false);

        _explosionSystem.gameObject.SetActive(true);

        _explosionSystem.Play();

        yield return new WaitForSeconds(0.75f);

        callBack?.Invoke();
    }
}
