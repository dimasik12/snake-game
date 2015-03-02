﻿using UnityEngine;
using System.Collections;

public class SelectIntricacy : MonoBehaviour
{
    public static string intricacy;  

    public void ExitIntricacyMenu()
    {
        Application.LoadLevel("SnakeMenu");
    }

    public void Easy()
    {
        intricacy = "easy";
        Application.LoadLevel("Game");
    }

    public void Average()
    {
        intricacy = "average";
        Application.LoadLevel("Game");
    }

    public void Complex()
    {
        intricacy = "complex";
        Application.LoadLevel("Game");
    }

    public void Profi()
    {
        intricacy = "profi";
        Application.LoadLevel("Game");
    }
}
