using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private float waypointSize = 1f;
    private LevelGenerator _levelGenerator;

    [Inject]
    private void InjectDependencies(LevelGenerator levelGenerator)
    {
        _levelGenerator = levelGenerator;
    }
    

    

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        
        
            if (currentWaypoint == null)
            {

                return _levelGenerator.PlatformsStack[_levelGenerator.PlatformsStack.Count - 1].transform;
            }

            if (currentWaypoint.GetSiblingIndex() < _levelGenerator.PlatformsStack.Count - 1)
            {

                return _levelGenerator.PlatformsStack[currentWaypoint.GetSiblingIndex() + 1].transform;
            }
            else
            {

                return _levelGenerator.PlatformsStack[_levelGenerator.PlatformsStack.Count - 1].transform;
            }


       


    }
}