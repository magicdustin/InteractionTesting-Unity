﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[ExecuteInEditMode]
[RequireComponent(typeof(SwitchCircuit))]
public class SwitchWires : MonoBehaviour
{
    public Color mainWireColor = Color.red, switchWireColor = Color.green;

    private SwitchCircuit _switch;
    private int mainEventCount;

    private void Awake()
    {
        _switch = GetComponent<SwitchCircuit>();
    }

    private void OnDrawGizmos()
    {
        if (!_switch) return;

        Handles.color = mainWireColor;
        mainEventCount = _switch.GetMainEvents().GetPersistentEventCount();
        for (int i = 0; i < mainEventCount; i++)
        {
            Component component = _switch.GetMainEvents().GetPersistentTarget(i) as Component;
            if (!component) continue;
            Transform tr = component.transform;
            Handles.DrawLine(tr.position, transform.position);
            Handles.Label(tr.position, "Main");
        }

        Handles.color = switchWireColor;
        for (int i = 0; i < _switch.GetEventList().Count; i++)
        {
            UnityEvent curEvent = _switch.GetEventList()[i];
            int curEventCount = curEvent.GetPersistentEventCount();
            for (int j = 0; j < curEventCount; j++)
            {
                Component component = curEvent.GetPersistentTarget(j) as Component;
                if (!component) continue;
                Transform tr = component.transform;
                Handles.DrawLine(tr.position, transform.position);
                Handles.Label(tr.position, "#" + (i+1));
            }

        }
    }
}
