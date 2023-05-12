using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{
    float currentTime = 0f;

    [SerializeField] TextMeshProUGUI countdown;
    [SerializeField] float startingTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdown.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            Time.timeScale = 0;
        }
    }
}
