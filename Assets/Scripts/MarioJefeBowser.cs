using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class MarioJefeBowser : MonoBehaviour
{
    public int resistencia = 3;
    public float velocidad = 1f;
    public float recorrido = 3f;
    public AudioClip sonidoGolpe;
    private Vector3 origen;
    private float ultimoGolpe = -10f;
    private AudioSource audioSource;
    private SpriteRenderer sprite;

    private void Start()
    {
        origen = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        transform.position = origen + Vector3.left * Mathf.Sin(Time.time * velocidad) * recorrido;
        if (sprite != null) sprite.color = Time.time - ultimoGolpe < 0.25f ? Color.white : new Color(1f, 0.88f, 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null || player.dead || Time.time - ultimoGolpe < 1f) return;
        // Mario debe caer desde arriba sobre el jefe para hacer dano.
        bool golpeDesdeArriba = player.transform.position.y > transform.position.y + 0.35f;
        if (golpeDesdeArriba)
        {
            ultimoGolpe = Time.time;
            resistencia--;
            if (sonidoGolpe != null) audioSource.PlayOneShot(sonidoGolpe);
            if (resistencia <= 0) SceneManager.LoadScene("Victoria");
        }
        else
        {
            player.Hit();
        }
    }
}
