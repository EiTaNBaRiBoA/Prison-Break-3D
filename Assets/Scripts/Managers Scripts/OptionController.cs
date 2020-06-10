using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    private delegate void MovingCamera();
    private MovingCamera movingCamera;
    public Canvas mainMenu;
    public Canvas optionMenu;
    public Canvas tutorialCanvas;

    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;


    private void Start() {
        musicSlider.value = GameManager.gameManager.ReturnMusicVolume();
        sfxSlider.value = GameManager.gameManager.ReturnSFXVolume();
    }
    private void Update() {
        movingCamera?.Invoke();
    }
    public void MoveToMain()
    {
        movingCamera=MoveToMain;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,mainMenu.transform.position - new Vector3(0,0,10),1f*Time.deltaTime);
    }
        public void MoveToOption()
    {
        movingCamera=MoveToOption;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,optionMenu.transform.position - new Vector3(0,0,10),1f*Time.deltaTime);
    }
    public void MoveToGuide()
    {
        movingCamera=MoveToGuide;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,tutorialCanvas.transform.position - new Vector3(0,0,10),1f*Time.deltaTime);
    }
}
