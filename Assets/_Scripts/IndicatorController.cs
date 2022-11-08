using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class IndicatorController : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private GameObject _indicatorObject;
    private bool placementPoseIsValid = false;
    private Pose placementPose;

    public bool PlacementPoseIsValid => placementPoseIsValid;
    public Pose PlacementPose => placementPose;

    private void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        _indicatorObject = GetComponentInChildren<Transform>().gameObject;

        _indicatorObject.SetActive(false);
    }

    private void Update()
    {
        UpdateIndicatorPosition();
        UpdateIndicatorActive();
    }

    private void UpdateIndicatorPosition()
    {
        // Shoot ray cast from the center of screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // If we hit, update position and rotation
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            // Is this optional?
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdateIndicatorActive()
    {
        if (placementPoseIsValid)
        {
            _indicatorObject.SetActive(true);
            _indicatorObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            _indicatorObject.SetActive(false);
        }
    }
}