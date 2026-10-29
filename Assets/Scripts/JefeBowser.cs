using UnityEngine;
using UnityEngine.SceneManagement;
public class JefeBowser : MonoBehaviour
{
    public int golpesNecesarios = 3;
    public float velocidad = 1.1f;
    public float recorrido = 2f;
    private float origen;
    private float ultimoGolpe = -5f;
    private SpriteRenderer sr;
    private void Start() { origen = transform.position.x; sr = GetComponent<SpriteRenderer>(); }
    private void Update()
    {
        Vector3 p = transform.position;
        p.x = origen + Mathf.Sin(Time.time * velocidad) * recorrido;
        transform.position = p;
        if (sr != null) sr.color = Time.time - ultimoGolpe < 0.25f ? Color.white : new Color(1f,0.82f,0.74f);
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        MarioJugador mario = c.GetComponent<MarioJugador>();
        if (mario == null || Time.time - ultimoGolpe < 0.8f) return;
        if (mario.transform.position.y > transform.position.y + 1.0f)
        {
            ultimoGolpe = Time.time;
            mario.Rebotar(); golpesNecesarios--;
            AudioMario.Efecto("moneda");
            if (golpesNecesarios <= 0) SceneManager.LoadScene("Victoria");
        }
        else mario.Danar();
    }
}
