using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class three : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    public IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownDisplay.text = "";

        if (countdownDisplay != null)
        {
            countdownDisplay.gameObject.SetActive(false);
        }
    }
}
