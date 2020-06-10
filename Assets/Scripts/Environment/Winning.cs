using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Cursor.lockState = CursorLockMode.None;
        GameManager.gameManager.RequestToLoadScene(2);
    }
}
