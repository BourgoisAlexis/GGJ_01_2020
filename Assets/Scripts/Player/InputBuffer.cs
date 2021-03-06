﻿using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    #region Variables
    private Dictionary<string, int> buffed = new Dictionary<string, int>();
    private int timer = 10;
    #endregion


    private void Awake()
    {
        Setup();
    }

    private void Update()
    {
        Decrease();
    }


    private void Setup()
    {
        buffed.Add("Jump", 0);
        buffed.Add("Shoot", 0);
    }

    public bool CheckButton(string _button)
    {
        if(buffed.ContainsKey(_button))
            if(buffed[_button] > 0)
                return true;

        return false;
    }

    public void Executed(string _button)
    {
        if (buffed.ContainsKey(_button))
            buffed[_button] = 0;
    }

    public void AddInput(string _input)
    {
        if (buffed.ContainsKey(_input))
            buffed[_input] = timer;
    }

    private void Decrease()
    {
        Dictionary<string, int> tempo = new Dictionary<string, int>();

        foreach (KeyValuePair<string, int> n in buffed)
        {
            if(n.Value > 0)
                tempo.Add(n.Key, n.Value - 1);
            else
                tempo.Add(n.Key, n.Value);
        }

        buffed = tempo;
    }
}
