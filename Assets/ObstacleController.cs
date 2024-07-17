using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] string obstacleType = "static"; //destroyable

    public string GetObstacleType()
    {
        return obstacleType;
    }
}
