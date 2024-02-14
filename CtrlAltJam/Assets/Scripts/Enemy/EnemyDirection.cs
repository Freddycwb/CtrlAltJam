using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    public Action startMoveToDestination;
    public Action reachDestination;

    protected bool _checkingPlayerLastPos;

    public bool GetCheckingPlayerLastPos()
    {
        return _checkingPlayerLastPos;
    }

    public void SetCheckingPlayerLastPos(bool value)
    {
        _checkingPlayerLastPos = value;
    }

    public virtual Vector2 Direction()
    {
        return Vector2.zero;
    }
}
