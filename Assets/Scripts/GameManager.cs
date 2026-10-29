using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; } = 1;
    public int stage { get; private set; } = 1;
    public int lives { get; private set; } = 3;
    public int coins { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        // Permite probar directamente cualquier escena desde Unity.
        // No se fuerza volver a 1-1 al iniciar una prueba.
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadLevel(int newWorld, int newStage)
    {
        world = newWorld;
        stage = newStage;
        if (newWorld == 1 && newStage == 2)
            SceneManager.LoadScene("1-2subterraneo");
        else if (newWorld == 2 && newStage == 2)
            SceneManager.LoadScene("2-2Acuatico");
        else if (newWorld == 3 && newStage == 1)
            SceneManager.LoadScene("3-1CastilloBowser");
        else
            SceneManager.LoadScene($"{newWorld}-{newStage}");
    }

    public void NextLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1-1": LoadLevel(1, 2); break;
            case "1-2subterraneo": LoadLevel(2, 2); break;
            case "2-2Acuatico": LoadLevel(3, 1); break;
            case "3-1CastilloBowser": SceneManager.LoadScene("Victoria"); break;
            default: NewGame(); break;
        }
    }

    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;
        if (lives > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            GameOver();
    }

    public void AddCoin()
    {
        coins++;
        if (coins >= 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife() { lives++; }
}
