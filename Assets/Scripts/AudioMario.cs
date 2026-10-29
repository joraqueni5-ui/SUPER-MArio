using UnityEngine;
public class AudioMario : MonoBehaviour
{
    public string tema = "subterraneo";
    private AudioSource musica;
    private void Start()
    {
        musica = gameObject.AddComponent<AudioSource>();
        musica.clip = Resources.Load<AudioClip>("Audio/"+tema);
        musica.volume = 0.22f;
        musica.loop = true;
        if (musica.clip != null) musica.Play();
    }
    public static void Efecto(string nombre)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/"+nombre);
        if (clip == null) return;
        Camera cam = Camera.main;
        AudioSource.PlayClipAtPoint(clip,cam != null ? cam.transform.position : Vector3.zero,0.45f);
    }
}
