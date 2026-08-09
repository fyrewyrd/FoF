using UnityEngine;
using System;

[Serializable] public class CreationInfo
{
    [Header("CREATED OBJECT")]
    [SerializeField] public GameObject toCreate;

    [SerializeField] private int _minAmount;
    public int minAmount { get { if (_minAmount < 1) return 1; else return _minAmount; } set { _minAmount = value; } }

    [SerializeField] private int _maxAmount;
    public int maxAmount { get { if (_maxAmount < 1) return 1; else return _maxAmount; } set { _maxAmount = value; } }

    [SerializeField] [Range(0.000f, 1.000f)] public float probability;
}
