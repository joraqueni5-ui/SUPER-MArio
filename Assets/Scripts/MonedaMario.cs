using UnityEngine;
public class MonedaMario : MonoBehaviour
{
    private float baseY;
    private void Start() { baseY = transform.position.y; }
    private void Update()
    {
        Vector3 p = transform.position;
        p.y = baseY + Mathf.Sin(Time.time * 4f) * 0.07f;
        transform.position = p;
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<MarioJugador>() == null) return;
        if (GameManager.Instance != null) GameManager.Instance.AddCoin();
        AudioMario.Efecto("moneda"); Destroy(gameObject);
    }
}
