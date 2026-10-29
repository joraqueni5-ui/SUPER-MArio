using UnityEngine;

// Melodia corta ORIGINAL, generada en tiempo real, para pruebas sin archivos de audio.
[RequireComponent(typeof(AudioSource))]
public class MarioMusicaRetro : MonoBehaviour
{
    public float volumen = 0.12f;
    public int variante = 0;
    private void Awake()
    {
        int sampleRate = 22050;
        float secondsPerNote = variante == 1 ? 0.32f : 0.24f;
        float[] melody = variante == 1
            ? new float[] {294, 0, 349, 440, 392, 0, 330, 294, 262, 0, 294, 349, 220, 0, 196, 0}
            : new float[] {330, 392, 494, 0, 440, 392, 330, 294, 392, 440, 523, 0, 440, 349, 392, 0};
        int noteSamples = Mathf.RoundToInt(secondsPerNote * sampleRate);
        int length = noteSamples * melody.Length;
        float[] data = new float[length];
        for (int i = 0; i < data.Length; i++)
        {
            float freq = melody[Mathf.Min(i/noteSamples, melody.Length-1)];
            int at = i % noteSamples;
            float env = Mathf.Min(1f, at/(sampleRate*.015f)) * Mathf.Min(1f, (noteSamples-at)/(sampleRate*.028f));
            float phase = ((i/(float)sampleRate)*freq) % 1f;
            data[i] = freq <= 0 ? 0 : (phase < .5f ? 1f : -.8f) * env * .35f;
        }
        AudioClip music = AudioClip.Create("Melodia retro original", length, 1, sampleRate, false);
        music.SetData(data, 0);
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = music;
        audio.loop = true;
        audio.volume = volumen;
        audio.Play();
    }
}
