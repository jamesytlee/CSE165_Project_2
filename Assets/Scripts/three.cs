using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class three : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    private void Start() {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart() {
        while(countdownTime > 0) {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownDisplay.text = "Go!";
        GameController.instance.BeginGame();
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
public class GameController : MonoBehaviour
{
    public static GameController instance;

    private bool canMove;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BeginGame()
    {
        canMove = true;
    }

    public bool CanMove()
    {
        return canMove;
    }

}
