using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    [TextArea(3, 10)]
    public string[] common;

    [TextArea(3, 10)]
    public string[] assassin;

    [TextArea(3, 10)]
    public string[] weapon;

    [TextArea(3, 10)]
    public string[] local;

}
