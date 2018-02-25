using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class PauseMenu : MonoBehaviour {

    public static bool paused = false;
    public GameObject Player;
    public GameObject pauseMenuUI;
    public AudioSource gameBackgroundMusic;
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        camera.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        Player.SetActive(true);
        camera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        Cursor.visible = false;
        gameBackgroundMusic.UnPause();
    }

    void Pause()
    {
        Player.SetActive(false);
        camera.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        gameBackgroundMusic.Pause();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
