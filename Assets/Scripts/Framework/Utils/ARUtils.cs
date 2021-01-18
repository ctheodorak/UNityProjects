using GoogleARCore;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HolographicKiller.Framework.Utils
{
    public class ARUtils : MonoBehaviour
    {
        public static bool TryFindDetectedPlane(Touch touch, Vector2 touchPosition, out Pose? hitPose)
        {
            if (IsUItouched(touch))
            {
                hitPose = null;
                return false;
            }

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touchPosition.x, touchPosition.y, raycastFilter, out hit))
            {
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(Camera.main.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    if (hit.Trackable is DetectedPlane)
                    {
                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                        if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                        {
                            hitPose = null;
                            return true;
                        }
                        else
                        {
                            hitPose = new Pose(hit.Pose.position, hit.Pose.rotation);
                            return true;
                        }
                    }
                    else
                    {
                        hitPose = null;
                        return false;
                    }
                }
            }
            hitPose = null;
            return false;
        }

        private static bool IsUItouched(Touch touch)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true;
            }

            return false;
        }
    }
}