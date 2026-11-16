using System;
using System.Collections.Generic;
using UnityEngine;

public class HouseZone : MonoBehaviour
{
    private List<Thief> _thievesInside = new List<Thief>();

    public event Action<int> ThiefCountChanged;

    public int ThiefCount => _thievesInside.Count;

    private void OnTriggerEnter(Collider other)
    {
        Thief thief = other.GetComponent<Thief>();

        if (thief == null || _thievesInside.Contains(thief))
            return;

        _thievesInside.Add(thief);
        ThiefCountChanged?.Invoke(_thievesInside.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        Thief thief = other.GetComponent<Thief>();

        if (thief == null || _thievesInside.Remove(thief) == false)
            return;

        ThiefCountChanged?.Invoke(_thievesInside.Count);
    }
}