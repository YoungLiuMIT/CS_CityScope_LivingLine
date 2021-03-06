﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;

[TaskCategory("Game")]
public class AIMoveTargetByIDAction : Action
{
    IAstarAI ai;
    public SharedTransform target;
    public PathPoint.PathPointType pointType;
    public float stopDistance = 3;

    public override void OnAwake()
    {
        base.OnAwake();
        ai = this.transform.GetComponent<IAstarAI>();

    }

    public override void OnStart()
    {
        base.OnStart();
        GetTarget();
        ai.isStopped = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (target != null && ai != null) ai.destination = target.Value.position;
        //Debug.Log("Distance..." + ai.remainingDistance);
        if (ai.reachedEndOfPath || ai.remainingDistance < stopDistance)
        {
            ai.isStopped = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    void GetTarget()
    {
        PathPoint point = SingletonTMono<App>.Instance.GetRandomPoint(pointType);
        if (point != null)
        {
            target.Value = point.transform;
        }
    }
}
