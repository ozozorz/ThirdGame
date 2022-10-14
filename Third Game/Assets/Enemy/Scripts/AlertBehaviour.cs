using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dialog.Scripts;
using UnityEngine.SceneManagement;

public class AlertBehaviour : MonoBehaviour
{
    public bool foundPlayer { get; private set; }

    private int count = 1;

    private void Awake()
    {
        foundPlayer = false;
    }

    public void FoundPlayer() {
        foundPlayer = true;
        if (count <= 0) {
            return;
        }
        Invoke("ShowMenu", 1f);
        count--;
    }

    void ShowMenu() {
        
        List<ButtonData> buttonList = new List<ButtonData>();
        buttonList.Add(new ButtonData("Restar", () => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }));
        MenuBehaviour menu = DialogService.GetInstance().CreateMenu("You have been found. \nMission failed:(", buttonList);
        menu.Show();
    }
}
