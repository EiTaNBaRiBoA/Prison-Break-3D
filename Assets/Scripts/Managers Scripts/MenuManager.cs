using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Canvas uiCanvas;
    public Canvas losingCanvas;
    private bool isMenuActive;
    private bool isLost;
    // Start is called before the first frame update
    void Start()
    {
        isMenuActive = false;
        isLost = false;
        uiCanvas.gameObject.SetActive(false);
        losingCanvas.gameObject.SetActive(false);
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
        if (isLost == false)
        {
            if (!isMenuActive)
            {
                isMenuActive = true;
                uiCanvas.gameObject.SetActive(isMenuActive);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                isMenuActive = false;
                uiCanvas.gameObject.SetActive(isMenuActive);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void LosingCanvas()
    {
        isLost = true;
        losingCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }


    public void Resume()
    {
        MenuUI();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.gameManager.RequestToLoadScene(1);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        GameManager.gameManager.RequestToLoadScene(0);
    }
}
