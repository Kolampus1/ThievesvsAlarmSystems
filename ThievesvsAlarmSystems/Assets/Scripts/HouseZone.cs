using System;
using System.Collections.Generic;
using UnityEngine;

public class HouseZone : MonoBehaviour
{
    private List<Thief> _thievesInside = new List<Thief>();

    public event Action<bool> AlarmStateChanged;

    public bool IsAlarmActive => _thievesInside.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        Thief thief = other.GetComponent<Thief>();

        if(thief == null || _thievesInside.Contains(thief))
            return;
        
        bool wasAlarmActive = IsAlarmActive;

        _thievesInside.Add(thief);

        if(wasAlarmActive == false)
            AlarmStateChanged?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Thief thief = other.GetComponent<Thief>();

        if(thief == null || _thievesInside.Remove(thief) == false)
            return;

        if(_thievesInside.Count == 0)
            AlarmStateChanged?.Invoke(false);
    }
}