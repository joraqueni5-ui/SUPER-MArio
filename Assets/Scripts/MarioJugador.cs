using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class MarioJugador : MonoBehaviour
{
    public bool nivelAcuatico;
    public Sprite quieto;
    public Sprite corre1;
    public Sprite corre2;
    public Sprite salto;
    public float velocidad = 7f;
    public float impulso = 11f;

    private Rigidbody2D cuerpo;
    private SpriteRenderer dibujo;
    private float horizontal;
    private float ultimoDano = -10f;
    private bool enSuelo;
    private bool saltar;
    private bool muerto;

    private void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        if (GameManager.Instance == null)
            new GameObject("Game Manager").AddComponent<GameManager>();
        cuerpo.gravityScale = nivelAcuatico ? 0.75f : 2.4f;
        cuerpo.freezeRotation = true;
        if (quieto != null) dibujo.sprite = quieto;
    }
    private void Update()
    {
        if (muerto) return;
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) saltar = true;
        if (Mathf.Abs(horizontal) > 0.05f) dibujo.flipX = horizontal < 0;
        if (nivelAcuatico)
        {
            dibujo.sprite = Mathf.Abs(cuerpo.linearVelocity.y) > 0.2f ? (salto != null ? salto : quieto) : quieto;
        }
        else if (!enSuelo) dibujo.sprite = salto != null ? salto : quieto;
        else if (Mathf.Abs(horizontal) > 0.05f)
            dibujo.sprite = Mathf.FloorToInt(Time.time * 10f) % 2 == 0 ? corre1 : corre2;
        else dibujo.sprite = quieto;
    }
    private void FixedUpdate()
    {
        if (muerto) return;
        Vector2 v = cuerpo.linearVelocity;
        v.x = horizontal * velocidad * (nivelAcuatico ? 0.75f : 1f);
        if (saltar && (nivelAcuatico || enSuelo))
        {
            v.y = nivelAcuatico ? 5.9f : impulso;
            enSuelo = false;
            AudioMario.Efecto("salto");
        }
        saltar = false;
        if (nivelAcuatico) v.y = Mathf.Clamp(v.y, -2.8f, 5.9f);
        cuerpo.linearVelocity = v;
    }
    private void OnCollisionStay2D(Collision2D c)
    {
        foreach (ContactPoint2D p in c.contacts)
            if (p.normal.y > 0.45f) { enSuelo = true; break; }
    }
    private void OnCollisionExit2D(Collision2D c) { enSuelo = false; }
    public void Rebotar()
    {
        Vector2 v = cuerpo.linearVelocity;
        v.y = nivelAcuatico ? 5f : 8f;
        cuerpo.linearVelocity = v;
    }
    public void Danar()
    {
        if (muerto || Time.time - ultimoDano < 1.2f) return;
        ultimoDano = Time.time;
        muerto = true;
        AudioMario.Efecto("dano");
        if (GameManager.Instance != null) GameManager.Instance.ResetLevel(0.65f);
    }
    private void OnGUI()
    {
        GUIStyle s = new GUIStyle(GUI.skin.label);
        s.fontSize = Mathf.Clamp(Screen.height / 28, 18, 29);
        s.fontStyle = FontStyle.Bold;
        s.normal.textColor = Color.white;
        s.normal.background = Texture2D.blackTexture;
        int vidas = GameManager.Instance != null ? GameManager.Instance.lives : 3;
        int monedas = GameManager.Instance != null ? GameManager.Instance.coins : 0;
        GUI.Label(new Rect(14, 8, 400, 40), "MARIO  x" + vidas + "    MONEDAS " + monedas, s);
    }
}
