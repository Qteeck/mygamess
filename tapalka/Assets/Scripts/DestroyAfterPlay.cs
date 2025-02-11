using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    private ParticleSystem  _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (_particleSystem != null && !_particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
    }
}
