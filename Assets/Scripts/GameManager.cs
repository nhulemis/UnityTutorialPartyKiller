using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int currentLevel;

    public bool IsEndGame { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsEndGame = false;
        Instance = this;
        currentLevel = 1;
        StartCoroutine(LoadFirstLevel());

    }

    private IEnumerator LoadFirstLevel()
    {
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);

        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEndGame)
        {
            IsEndGame = false;
            StartCoroutine(EndGame());
        }
    }

    int GetNextLevel()
    {
        //return Random.Range(1, 11);
        //return ++currentLevel;
        return 2;
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(.75f);

        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }

        foreach (var fx in GameObject.FindGameObjectsWithTag("Fx"))
        {
            Destroy(fx);
        }

        foreach (var player in GameObject.FindGameObjectsWithTag("PlayerA"))
        {
            Destroy(player);
        }

        yield return new WaitForSeconds(.75f);

        AsyncOperation gameUnload = SceneManager.UnloadSceneAsync(currentLevel);

        while (!gameUnload.isDone)
        {
            yield return new WaitForSeconds(.01f);
        }

        currentLevel = GetNextLevel();

        SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);

        yield return new WaitForSeconds(1);

    }
}
