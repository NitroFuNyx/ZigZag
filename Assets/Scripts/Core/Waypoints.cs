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

                return _levelGenerator.PlatformsList[_levelGenerator.PlatformsList.Count - 1].transform;
            }

            if (currentWaypoint.GetSiblingIndex() < _levelGenerator.PlatformsList.Count - 1)
            {

                return _levelGenerator.PlatformsList[currentWaypoint.GetSiblingIndex() + 1].transform;
            }
            else
            {

                return _levelGenerator.PlatformsList[_levelGenerator.PlatformsList.Count - 1].transform;
            }


       


    }
}