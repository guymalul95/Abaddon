using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class PauseMenu : MonoBehaviour {

    public static bool paused = false;
    public GameObject player;
    public GameObject pauseMenuUI;
    public AudioSource gameBackgroundMusic;
    private Camera pauseCamera;

    private void Start()
    {
        pauseCamera = GetComponent<Camera>();
        pauseCamera.enabled = false;
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
        player.SetActive(true);
        pauseCamera.enabled = false;
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
        player.SetActive(false);
        pauseCamera.enabled = true;
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
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
