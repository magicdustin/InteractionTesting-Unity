﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SwitchWires))]
public class SwitchCircuit : Circuit
{
    [Header("Events Every Activation")]
    [Tooltip("These methods are called everytime this circuit is activated")]
    public UnityEvent events;

    [Space]

    [Header("Cycling Events")]
    [SerializeField]
    [Tooltip("Each set of events will trigger in order per activation and then cycle")]
    public List<UnityEvent> eventList;

    private int eventIterator = 0;

    private void Awake()
    {
        if (eventList.Count <= 1)
        {
            Debug.LogWarning("If you are only using one set of events you should use a circuit instead of a switch");
        }
    }

    public override void Activate()
    {
        events.Invoke();
        eventList[eventIterator].Invoke();
        if(eventIterator + 1 == eventList.Count)
        {
            eventIterator = 0;
            return;
        }
        ++eventIterator;
    }
}