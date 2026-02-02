using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duckPrefab;

    public float spawnInterval = 1.5f;
    public int maxDucksAlive = 3;

    // The world Y where the ducks float (top of waves)
    public float waterLineY = -2f;

    // Small random Y variation so it looks natural
    public float waterLineRandomRange = 0.15f;

    public float edgePadding = 0.4f;

    private Camera cam;
    private float timer;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            if (CountAliveDucks() < maxDucksAlive)
                SpawnDuck();
        }
    }

    void SpawnDuck()
    {
        float leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).x + edgePadding;
        float rightEdge = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f)).x - edgePadding;

        float x = Random.Range(leftEdge, rightEdge);
        float y = waterLineY + Random.Range(-waterLineRandomRange, waterLineRandomRange);

        GameObject duck = Instantiate(duckPrefab, new Vector3(x, y, 0f), Quaternion.identity);

        // Tag it so the spawner can count ducks
        duck.tag = "Duck";

        // Set up mover
        DuckMovement mover = duck.GetComponent<DuckMovement>();
        if (mover != null)
        {
            mover.SetBaseY(y);

            // Random start direction
            mover.direction = (Random.value < 0.5f) ? -1 : 1;
        }
    }

    int CountAliveDucks()
    {
        GameObject[] ducks = GameObject.FindGameObjectsWithTag("Duck");
        return ducks.Length;
    }
}
