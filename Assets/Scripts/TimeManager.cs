using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static Action OnSecondChanged;
    public static Action OnMinuteChanged;

    private static int Second { get; set; }
    private static int Minute { get; set; }

    private float timer;
    public TextMeshProUGUI timeText; // Referência ao componente de texto na cena

    // Start is called before the first frame update
    void Start()
    {
        Second = 0;
        Minute = 0;
        timer = 0;
        UpdateTimeText(); // Atualiza o texto inicialmente
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            Second++;
            if (Second >= 60)
            {
                Second = 0;
                Minute++;
                OnMinuteChanged?.Invoke();
            }
            OnSecondChanged?.Invoke();
            UpdateTimeText(); // Atualiza o texto quando os segundos mudam
        }
    }

    // Atualiza o componente de texto com o tempo formatado
    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = string.Format("{0:00}:{1:00}", Minute, Second);
        }
    }
}
