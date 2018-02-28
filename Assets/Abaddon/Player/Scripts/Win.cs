using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour {

    public GameObject playerState;

    public void OnTriggerEnter(Collider other)
    {
        playerState.GetComponent<PlayerState>().Win();
    }
}
