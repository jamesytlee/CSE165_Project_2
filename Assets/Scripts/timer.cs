using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{
    float currentTime = 0f;
    float maxTime = 600f;
    float delay = 3f;
    bool startCounting = false;

    [SerializeField] TextMeshProUGUI countup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSeconds(delay));
    }

    // Update is called once per frame
    void Update()
    {
        if (startCounting && currentTime < maxTime)
        {
            currentTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            countup.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        startCounting = true;
    }
}
