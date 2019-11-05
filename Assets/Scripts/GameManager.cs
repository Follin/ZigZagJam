using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool HasStarted;

    public void StartGame() => HasStarted = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartGame();
    }
}
