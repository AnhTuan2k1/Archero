using UnityEngine;

public class Laser : Bullet
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField][Range(0, 1f)] private float width;

    public override ObjectPoolingType BulletType => ObjectPoolingType.Laser;

    private void Awake()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.endWidth = lineRenderer.startWidth = width;
    }

    public void SetPosition(Vector3 start, Vector3 end)
    {
        transform.position = start;
        lineRenderer.SetPosition(1, end);
    }

    public static Laser Instantiate(Vector3 start, Vector3 end)
    {
        Laser laser = ObjectPooling.Instance
            .GetObject(ObjectPoolingType.Laser).GetComponent<Laser>();

        laser.SetPosition(start, end);

        laser.OnInstantiate();
        return laser;
    }

    public override void OnInstantiate()
    {
        base.OnInstantiate();
        Invoke(nameof(Die), 0.5f);
    }

    private void Die()
    {
        ObjectPooling.Instance.ReturnObject(gameObject);
    }
}
