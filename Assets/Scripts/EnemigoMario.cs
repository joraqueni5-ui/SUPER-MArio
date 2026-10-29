using UnityEngine;
public class EnemigoMario : MonoBehaviour
{
    public float recorrido = 2.5f;
    public float velocidad = 1f;
    private float origen;
    private void Start() { origen = transform.position.x; }
    private void Update()
    {
        Vector3 p = transform.position;
        p.x = origen + Mathf.Sin(Time.time * velocidad) * recorrido;
        transform.position = p;
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        MarioJugador mario = c.GetComponent<MarioJugador>();
        if (mario == null) return;
        if (mario.transform.position.y > transform.position.y + 0.36f && !mario.nivelAcuatico)
        { mario.Rebotar(); AudioMario.Efecto("moneda"); Destroy(gameObject); }
        else mario.Danar();
    }
}
