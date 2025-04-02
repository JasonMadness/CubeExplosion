using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private LayerMask _cubeLayer;
    [SerializeField] private Transform[] _startingSpawnPoints;
    [SerializeField] private Exploder _exploder;

    private const int LeftMouseButton = 0;

    private GameSettings _settings = new();

    private void Start()
    {
        foreach (Transform spawnPoint in _startingSpawnPoints)
        {
            Cube cube = _cubeFactory.GetFromPool();
            cube.transform.position = spawnPoint.position;
            cube.Initialize(_settings.StartingCubeScale, _settings.StartingCubeChildChance);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _cubeLayer))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cube cube))
                {
                    OnCubeClicked(cube);
                }
            }
        }
    }

    private void OnCubeClicked(Cube cube)
    {
        if (Random.Range(0, _settings.StartingCubeChildChance) < cube.ChildMakeChance)
        {
            Vector3 newScale = cube.Scale / 2;
            float newChance = cube.ChildMakeChance / 2;
            int newCubesCount = Random.Range(_settings.MinCubes, _settings.MaxCubes);
            
            Vector3 explosionPosition = cube.transform.position;            
            Cube[] newCubes = new Cube[newCubesCount];

            for (int i = 0; i < newCubesCount; i++)
            {
                newCubes[i] = _cubeFactory.GetFromPool();
                newCubes[i].Initialize(newScale, newChance);
            }
            
            _exploder.Explode(explosionPosition, newCubes);
        }

        _cubeFactory.ReturnToPool(cube);
    }
}