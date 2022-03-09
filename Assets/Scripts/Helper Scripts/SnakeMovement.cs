using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tags
{
    public static string WALL = "duvar";
    public static string APPLE = "apple";
    public static string Tail = "tail";
    public static string BOMB = "Bomb";
}

public class Metrics
{
    public static float NODE = 0.2f;
}

public enum PlayerDirection
{
    LEFT = 0,
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    COUNT = 4
}
