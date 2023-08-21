using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ResetLevel()
    {
        Time.timeScale = 1f;
        // Obtener el nobmre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Cargar la escena actual
        SceneManager.LoadScene(currentSceneName);
    }
    public void ExitGame()
    {
        // Salir de la aplicación
        Application.Quit();
    }

}
