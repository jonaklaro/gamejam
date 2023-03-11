using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArealAttackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawningPoint;

    private void Start()
    {
        spawningPoint = new GameObject[13];
        for (int i = 0; i < spawningPoint.Length; i++)
        {
            spawningPoint[i].SetActive(false);
        }
    }
}
