using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dialog.Scripts;
using UnityEngine.SceneManagement;

public class AlertBehaviour : MonoBehaviour
{
    public bool foundPlayer { get; private set; }
    public float alertValidTime = 3f;
    private float _alertValidTimestamp;
    public bool hasFeedback;
    public Behaviour[] alertDisables;
    public AudioSource shout;

    private void Awake()
    {
        foundPlayer = false;
    }

    public void FoundPlayer(Transform target)
    {
        if (foundPlayer)
            return;
        if (GetComponent<Enemy>().dead)
            return;
        if (EnemySystem.instance.gameOver)
            return;

        Debug.Log("发现玩家");
        foundPlayer = true;
        _alertValidTimestamp = Time.time + alertValidTime;
        if (hasFeedback)
            PlayFeedback(target);
    }

    void PlayFeedback(Transform target)
    {
        GetComponent<EnemyBehaviour>().Stand();
        foreach (var d in alertDisables)
            d.enabled = false;

        transform.LookAt(target, Vector3.up);
        shout.Play();
        GetComponentInChildren<FieldOfView>().SetToRed();
        //敌人ai 停下移动，面向玩家
        //喊话
        //绿色变成红色
        //一段时间后失败
    }

    private void Update()
    {
        if (EnemySystem.instance.gameOver)
            return;

        if (foundPlayer && Time.time > _alertValidTimestamp)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        EnemySystem.instance.GameOver();
        List<ButtonData> buttonList = new List<ButtonData>();
        buttonList.Add(new ButtonData("Retry", () =>
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }));
        MenuBehaviour menu = DialogService.GetInstance().CreateMenu("You have been found. \nMission failed:(", buttonList);
        menu.Show();
    }
}
