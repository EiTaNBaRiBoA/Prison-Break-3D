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

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    const string MUSIC_VOLUME_KEY = "MusicVolume";
    const string SFX_VOLUME_KEY = "SFXVolume";

    private void Start() {
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
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
