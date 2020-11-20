using System.Runtime.InteropServices;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARCore
{
    internal static class Api
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        [DllImport("UnityARCore", EntryPoint="UnityARCore_session_setFeatureRequested")]
        public static extern void SetFeatureRequested(Feature feature, bool requested);

        [DllImport("UnityARCore", EntryPoint="UnityARCore_session_getRequestedFeatures")]
        public static extern Feature GetRequestedFeatures();

        public static bool Android => true;
#else
        public static void SetFeatureRequested(Feature feature, bool requested) {}
        public static Feature GetRequestedFeatures() => Feature.None;
        public static bool Android => false;
#endif
    }
}
