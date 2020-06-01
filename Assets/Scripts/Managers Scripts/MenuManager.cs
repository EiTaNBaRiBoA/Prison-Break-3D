using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button resume;
    public Button options;
    public Canvas uiCanvas;
    private bool isMenuActive;
    // Start is called before the first frame update
    void Start()
    {
        isMenuActive = false;
        uiCanvas.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuUI();
        }
    }

    private void MenuUI()
    {
        if (!isMenuActive)
        {
            isMenuActive = true;
            uiCanvas.gameObject.SetActive(isMenuActive);
            Time.timeScale=0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            isMenuActive = false;
            uiCanvas.gameObject.SetActive(isMenuActive);
            Time.timeScale=1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public void Resume()
    {
        MenuUI();
    }

    public void Restart()
    {
        GameManager.gameManager.RequestToLoadScene(1);
        Time.timeScale=1f;
    }

    public void Menu()
    {
        GameManager.gameManager.RequestToLoadScene(0);
        Time.timeScale=1f;
    }
}
