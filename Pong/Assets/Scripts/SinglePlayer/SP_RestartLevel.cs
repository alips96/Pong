using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SP_RestartLevel : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private Text scoreText;
    [SerializeField] private float ballInitialSpeed = 5f;

    private void Start()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public void ResumeGame() //Called by restart level button.
    {
        playerMovement.enabled = true;
        scoreText.text = "0";
        GetComponent<Rigidbody2D>().velocity = new Vector2(ballInitialSpeed, Random.Range(-2f, 2f));
    }

    public void ToMainMenu() //Called by main menu button.
    {
        string name = PlayerPrefs.GetString("DISPLAYNAME");
        PlayerPrefs.SetString("LOGGEDIN", name);
        SceneManager.LoadScene(0);
    }
}
