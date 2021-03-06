﻿using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    #region Variables
    public RectTransform Up;
    public RectTransform Down;
    public DialogueBox DialogueBox;
    public Image BlackScreen;

    private int speed;
    #endregion


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    private void Start()
    {
        BlackScreen.color = Color.black;
        BlackFade(false);
        DialogueBox.gameObject.SetActive(false);
        speed = CameraManager.Instance.PanSpeed;
    }

    public void Cinemode(bool _cine)
    {
        StartCoroutine(CineModing(_cine));
    }

    public void BlackFade(bool _b)
    {
        StartCoroutine(BlackFading(_b));
    }



    //Coroutines
    private IEnumerator CineModing(bool _cine)
    {
        int n = 0;

        if(_cine)
        {
            while (n < speed)
            {
                yield return new WaitForFixedUpdate();
                Up.localPosition -= Vector3.up * 500 / speed;
                Down.localPosition += Vector3.up * 500 / speed;
                n++;
            }
        }
        else
        {
            while (n < speed / 2)
            {
                yield return new WaitForFixedUpdate();
                Up.localPosition += Vector3.up * 1000 / speed;
                Down.localPosition -= Vector3.up * 1000 / speed;
                n++;
            }
        }
    }

    private IEnumerator BlackFading(bool _b)
    {
        float wait = 0.3f;

        if(_b)
        {
            Color color = new Color(0, 0, 0, 0);
            float alpha = 0;
            BlackScreen.color = color;

            while (BlackScreen.color.a < 1)
            {
                yield return new WaitForFixedUpdate();
                alpha += 0.05f;
                color = new Color(0, 0, 0, alpha);
                BlackScreen.color = color;
            }

            yield return new WaitForSeconds(wait);

            SceneManager.LoadScene(0);
        }
        else if(!_b)
        {
            Color color = new Color(0, 0, 0, 1);
            float alpha = 1;
            BlackScreen.color = color;

            yield return new WaitForSeconds(wait);

            while (BlackScreen.color.a > 0)
            {
                yield return new WaitForFixedUpdate();
                alpha -= 0.05f;
                color = new Color(0, 0, 0, alpha);
                BlackScreen.color = color;
            }
        }
    }
}
