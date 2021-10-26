using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorExample
{
    public readonly int date = 100;
    public readonly string weekday;
    public ConstructorExample(int date, string weekday)
    {
        this.date = date;
        this.weekday = weekday;
    }
}
