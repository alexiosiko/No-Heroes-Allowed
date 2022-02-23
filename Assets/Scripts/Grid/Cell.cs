using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    public enum Type
    {
        level1,
        level2,
        level3,
        level4,
        empty,
    }
    public Type type;
    public Vector3Int position;
}
