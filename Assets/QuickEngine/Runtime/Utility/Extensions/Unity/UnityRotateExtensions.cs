namespace QuickEngine.Extensions
{
    using UnityEngine;

    public static partial class UnityEngineExtensions
    {
        #region Rotate_DegreesPerSecond

        public static void Rotate_DegreesPerSecond(this GameObject go, Vector3 direction, float timeInSeconds)
        {
            Rotate_DegreesPerSecond(go.transform, direction, timeInSeconds);
        }

        public static void Rotate_DegreesPerSecond(this Transform goTrans, Vector3 direction, float timeInSeconds)
        {
            goTrans.Rotate(direction * timeInSeconds * Time.deltaTime);
        }

        #endregion Rotate_DegreesPerSecond

        #region RotateAroundAxis

        #region RotateAroundAxis_X

        public static void RotateAroundAxis_X(this GameObject go, float degrees, float timeInSeconds)
        {
            RotateAroundAxis_X(go.transform, degrees, timeInSeconds);
        }

        public static void RotateAroundAxis_X(this Transform goTrans, float degrees, float timeInSeconds)
        {
            Rotate_DegreesPerSecond(goTrans, new Vector3(degrees, 0, 0), timeInSeconds);
        }

        #endregion RotateAroundAxis_X

        #region RotateAroundAxis_Y

        public static void RotateAroundAxis_Y(this GameObject go, float degrees, float timeInSeconds)
        {
            RotateAroundAxis_Y(go.transform, degrees, timeInSeconds);
        }

        public static void RotateAroundAxis_Y(this Transform goTrans, float degrees, float timeInSeconds)
        {
            Rotate_DegreesPerSecond(goTrans, new Vector3(0, degrees, 0), timeInSeconds);
        }

        #endregion RotateAroundAxis_Y

        #region RotateAroundAxis_Z

        public static void RotateAroundAxis_Z(this GameObject go, float degrees, float timeInSeconds)
        {
            RotateAroundAxis_Z(go.transform, degrees, timeInSeconds);
        }

        public static void RotateAroundAxis_Z(this Transform goTrans, float degrees, float timeInSeconds)
        {
            Rotate_DegreesPerSecond(goTrans, new Vector3(0, 0, degrees), timeInSeconds);
        }

        #endregion RotateAroundAxis_Z

        #endregion RotateAroundAxis

        #region LookAt

        public static void LookAt(this GameObject go, GameObject targetGo)
        {
            LookAt(go, targetGo.transform);
        }

        public static void LookAt(this GameObject go, Transform targetTrans)
        {
            go.transform.LookAt(targetTrans);
        }

        public static void LookAt(this GameObject go, Vector3 targetVector)
        {
            go.transform.LookAt(targetVector);
        }

        #endregion LookAt
    }
}