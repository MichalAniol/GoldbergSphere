using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public Slider titlesNumber;
    public Text number;

    void Start ()
    {
        if (PlayerPrefs.GetInt("size") < 3) PlayerPrefs.SetInt("size", (int)titlesNumber.value);
        else
        {
            titlesNumber.value = PlayerPrefs.GetInt("size");
        }
        number.text = titlesNumber.value.ToString();

    }

    void Update ()
    {
        number.text = titlesNumber.value.ToString();
    }

    public void StartButton()
    {
        PlayerPrefs.SetInt("size", (int)titlesNumber.value);
        SceneManager.LoadScene(1);
    }
}
