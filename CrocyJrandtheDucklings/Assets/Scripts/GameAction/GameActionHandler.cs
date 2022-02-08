using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    public GameAction action;
    public UnityEvent startEvent, respondEvent, respondLateEvent;
    public float holdTime = 0.1f;
    private WaitForSeconds waitObj;

    private void Start()
    {
        startEvent.Invoke();
    }

    private void OnEnable()
    {
        waitObj = new WaitForSeconds(holdTime);
        action.raiseNoArgs += Respond;
    }

    private void Respond()
    {
        respondEvent.Invoke();
        StartCoroutine(RespondLate());
    }

    private IEnumerator RespondLate()
    {
        yield return waitObj;
        respondLateEvent.Invoke();
    }

    private void OnDestroy()
    {
        action.raiseNoArgs = null;
    }
}