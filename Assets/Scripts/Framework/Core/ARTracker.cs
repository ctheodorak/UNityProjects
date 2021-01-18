using UnityEngine;
using GoogleARCore;

namespace HolographicKiller.Framework.Core
{
    public class ARTracker : MonoBehaviour
    {
        private bool _isQuitting = false;

        private void UpdateApplicationLifecycle()
        {
            if (Session.Status != SessionStatus.Tracking)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (_isQuitting)
            {
                return;
            }

            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _isQuitting = true;
                Invoke("DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _isQuitting = true;
                Invoke("DoQuit", 0.5f);
            }
        }

        private void DoQuit()
        {
            Application.Quit();
        }

        void Awake()
        {
            Application.targetFrameRate = 60;
        }

        void Update()
        {
            UpdateApplicationLifecycle();
        }
    }
}