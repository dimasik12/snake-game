using UnityEngine;
using System.Collections;
using System;

public sealed class LabGeneration : MonoBehaviour 
{
    private static readonly LabGeneration instanсe = new LabGeneration();

    static LabGeneration()
    {
    }

    private LabGeneration()
    {
    }

    public static LabGeneration Instance
    {
        get { return instanсe; }
    }

    public void createMap(String name)
    {
        GameObject easy = (GameObject)Instantiate(Resources.Load(name, typeof(GameObject)));
        easy.transform.position = new Vector3(0, 0, 0);
    }
	
}

