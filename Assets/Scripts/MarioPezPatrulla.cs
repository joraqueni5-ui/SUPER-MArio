using UnityEngine;

public class MarioPezPatrulla : MonoBehaviour
{
    public float range = 4f;
    public float speed = 1.5f;
    private Vector3 origin;
    private void Start() { origin = transform.position; }
    private void Update()
    {
        float dx = Mathf.Sin(Time.time * speed) * range;
        transform.position = origin + new Vector3(dx, Mathf.Sin(Time.time * speed * 1.6f) * 0.4f, 0f);
        Vector3 sc = transform.localScale;
        sc.x = Mathf.Abs(sc.x) * (Mathf.Cos(Time.time * speed) >= 0 ? 1f : -1f);
        transform.localScale = sc;
    }
}
