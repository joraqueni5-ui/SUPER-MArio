using UnityEngine;
using UnityEngine.SceneManagement;

// Administra vidas, monedas y el recorrido entre los cinco niveles.
// Reemplaza solo GameManager.cs: conserva el archivo .meta original de Unity.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;
    public int coins { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }
    private void OnDestroy() { if (Instance == this) Instance = null; }
    private void Start() { /* Se puede ejecutar cualquier escena desde el editor. */ }

    public void NewGame() { lives = 3; coins = 0; LoadLevel(1, 1); }
    public void GameOver() { SceneManager.LoadScene("GameOver"); }

    public void LoadLevel(int newWorld, int newStage)
    {
        world = newWorld; stage = newStage;
        string name = newWorld == 1 && newStage == 2 ? "1-2subterraneo" :
                      newWorld == 2 && newStage == 2 ? "2-2Acuatico" :
                      newWorld == 3 && newStage == 1 ? "CastilloBowser" :
                      $"{newWorld}-{newStage}";
        SceneManager.LoadScene(name);
    }
    public void NextLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1-1": LoadLevel(1, 2); break;
            case "1-2subterraneo": LoadLevel(2, 2); break;
            case "2-2Acuatico": LoadLevel(3, 1); break;
            case "CastilloBowser": SceneManager.LoadScene("Victoria"); break;
            default: NewGame(); break;
        }
    }
    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), Mathf.Max(0.05f, delay));
    }
    public void ResetLevel()
    {
        lives--;
        if (lives > 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else GameOver();
    }
    public void AddCoin() { coins++; if (coins >= 100) { coins = 0; AddLife(); } }
    public void AddLife() { lives++; }
}
