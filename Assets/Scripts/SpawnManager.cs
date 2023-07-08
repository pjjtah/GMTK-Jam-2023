using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SpawnManager", order = 1)]
public class SpawnManager : ScriptableObject
{
    public int level;
    public GameObject[] fruitList;
    public GameObject[] powerUps;

    public int[] fruitAmounts;
    public int[] powerUpAmounts;

    public int slices;

    public int slicesAtSameTime;
    public float sliceInterval;
    public float sliceTime;
    public int maxSlices;

    public bool Reversed;
}
