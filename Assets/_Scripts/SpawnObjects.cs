using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    private IndicatorController _indicatorController;

    private void Start()
    {
        _indicatorController = FindObjectOfType<IndicatorController>();
    }
    private void Update()
    {
        if (_indicatorController.PlacementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(_spawnObject, _indicatorController.PlacementPose.position, _indicatorController.PlacementPose.rotation);
    }
}
