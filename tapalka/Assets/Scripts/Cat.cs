using UnityEngine;

public class Cat : MonoBehaviour
{
    // Переменные
    [Header("Настройки вращения")]
    [SerializeField] private float _minRotationSpeed = 200f;
    [SerializeField] private float _maxRotationSpeed = 800f;
    private float rotationSpeed;

    [Header("Время жизни редких котов")]
    [SerializeField] private float lifeTime = 1f;

    [Header("Референсы")]
    [SerializeField] private GameObject _destroyEffectPrefabs;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private CatSpawner _spawner;

    [Header("Очки")]
    [SerializeField] private int _points = 5;
    public bool isRare = false;

    // Методы
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _spawner = FindAnyObjectByType<CatSpawner>();
    }

    private void Start()
    {
        rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        
        // Если кот редкий, уничтожаем его через lifeTime секунд
        if (isRare)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        // Проверяем, существует ли объект CatSpawner и активна ли игра
        if (_spawner == null || !_spawner.isGameActive)
            return;

        // Создаем эффект разрушения, если он задан
        if (_destroyEffectPrefabs != null)
        {
            Instantiate(_destroyEffectPrefabs, transform.position, Quaternion.identity);
        }

        // Отключаем спрайт и коллайдер, чтобы кот исчезал мгновенно
        _spriteRenderer.enabled = false;
        _collider.enabled = false;

        // Проверяем, есть ли звук и проигрываем его перед удалением
        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
            Destroy(gameObject, _audioSource.clip.length); // Удаляем кота после завершения звука
        }
        else
        {
            Debug.LogWarning("Отсутствует звук!"); // Выводим предупреждение в консоль
            Destroy(gameObject); // Удаляем кота сразу, если звука нет
        }

        // Добавляем очки
        _spawner.AddScore(_points);
    }
}
