using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(menuName = "Counter/enemyCounter")]
public class EnemyCounter : ScriptableObject
{
    public int counter;
    public int  enemyCounter
    {
        get { return counter;}
        set { counter = value; }
    }
}
