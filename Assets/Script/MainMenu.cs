using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panelCreditos;

    public void PlayGame()
    {
        SceneManager.LoadScene("EscenaPrincipal");
    }

    public void ShowCredits()
    {
        panelCreditos.SetActive(true);
    }

    public void CloseCredits()
    {
        panelCreditos.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
}
