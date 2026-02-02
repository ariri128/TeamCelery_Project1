using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    // Horizontal movement
    public float speed = 2f;
    public float edgePadding = 0.4f;

    // Bobbing
    public float bobAmplitude = 0.12f;
    public float bobFrequency = 2f;

    // Sway (optional)
    public float swayRotation = 6f;
    public float swayFrequency = 2f;

    // Direction: 1 = right, -1 = left
    public int direction = 1;

    private Camera cam;
    private SpriteRenderer sr;

    private float baseY;
    private float timeOffset;

    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();

        baseY = transform.position.y;
        timeOffset = Random.Range(0f, 1000f);

        UpdateFlip();
    }

    void Update()
    {
        // Move left/right
        transform.position += Vector3.right * (direction * speed * Time.deltaTime);

        // Bob up/down + tilt
        float t = Time.time + timeOffset;

        float bob = Mathf.Sin(t * bobFrequency) * bobAmplitude;
        Vector3 pos = transform.position;
        pos.y = baseY + bob;
        transform.position = pos;

        float tilt = Mathf.Sin(t * swayFrequency) * swayRotation;
        transform.rotation = Quaternion.Euler(0f, 0f, tilt);

        // Bounce off screen edges
        float leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).x + edgePadding;
        float rightEdge = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f)).x - edgePadding;

        pos = transform.position;

        if (pos.x <= leftEdge && direction < 0)
        {
            pos.x = leftEdge;
            transform.position = pos;

            direction = 1;
            UpdateFlip();
        }
        else if (pos.x >= rightEdge && direction > 0)
        {
            pos.x = rightEdge;
            transform.position = pos;

            direction = -1;
            UpdateFlip();
        }
    }

    void UpdateFlip()
    {
        // If your duck faces the wrong way, flip this logic.
        if (sr != null)
            sr.flipX = (direction < 0);
    }

    // Spawner calls this after spawning so bobbing uses the correct "waterline" Y
    public void SetBaseY(float newBaseY)
    {
        baseY = newBaseY;

        Vector3 pos = transform.position;
        pos.y = baseY;
        transform.position = pos;
    }
}
