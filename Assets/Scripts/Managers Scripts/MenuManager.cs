using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Canvas uiCanvas;
    public Canvas losingCanvas;
    public bool isLost;
    bool isMenuActive;
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
            if (!isMenuActive){OpenMenuUI(uiCanvas.gameObject,true,0,CursorLockMode.None);}
            else {OpenMenuUI(uiCanvas.gameObject,false,1,CursorLockMode.Locked);}
        }
    }

    private void OpenMenuUI(GameObject menuType,bool menuActivation,float timeScale,CursorLockMode typeCursor)
    {
        menuType.SetActive(menuActivation);
        isMenuActive = menuActivation;
        Cursor.visible = menuActivation;
        Time.timeScale =timeScale;
        Cursor.lockState = typeCursor;
    }

    public void LosingCanvas()
    {
        isLost = true;
        OpenMenuUI(losingCanvas.gameObject,true,0,CursorLockMode.None);
    }


    public void Resume()
    {
        MenuUI();
    }

    public void Restart()
    {
        SceneLoader(1);
    }

    public void Menu()
    {
        SceneLoader(0);
    }

    private void SceneLoader(int index)
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        GameManager.gameManager.RequestToLoadScene(index);
    }
}
