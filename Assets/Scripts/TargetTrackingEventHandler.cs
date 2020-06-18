using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;

public class TargetTrackingEventHandler : MonoBehaviour, ITrackableEventHandler
{ 
    public Action OnTrackFound;
    public Action OnTrackLost;

    TrackableBehaviour mTrackableBehaviour;
    TrackableBehaviour.Status previousStatus;
    TrackableBehaviour.Status newStatus;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        this.previousStatus = previousStatus;
        this.newStatus  = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackFound?.Invoke();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackLost?.Invoke();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
        }
    }
}
