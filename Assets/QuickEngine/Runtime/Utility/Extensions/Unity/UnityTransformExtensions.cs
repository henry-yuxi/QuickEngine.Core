namespace QuickEngine.Extensions
{
    using System;
    using UnityEngine;

    public static partial class UnityEngineExtensions
    {
        #region TransformChain

        /// <summary>
        /// 递归设置游戏对象的层次。
        /// </summary>
        /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
        /// <param name="layer">目标层次的编号。</param>
        public static void SetLayerRecursively(this Transform transform, int layer)
        {
            Transform[] transforms = transform.GetComponentsInChildren<Transform>(true);
            if (transforms != null && transforms.Length > 0)
            {
                int length = transforms.Length;
                for (int i = 0; i < length; i++)
                {
                    transforms[i].gameObject.layer = layer;
                }
            }
        }

        public static Transform SetParent(this Transform trans, Transform parent)
        {
            trans.transform.parent = parent;
            return trans;
        }

        /// <summary>
        /// 隐藏按钮，setActive能不用尽量少用，效率问题。
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public static Transform SetVisibleSafe(this Transform trans, bool isVisible)
        {
            trans.localScale = isVisible ? Vector3.one : Vector3.zero;
            return trans;
        }

        public static Transform SetVisibleSafe(this Transform trans, bool isVisible, float scaling = 1f)
        {
            trans.localScale = isVisible ? Vector3.one * scaling : Vector3.zero;
            return trans;
        }

        public static Transform SetPosition(this Transform trans, float? x = null, float? y = null, float? z = null)
        {
            trans.position = trans.position.Set(x, y, z);
            return trans;
        }

        public static Transform SetLocalPosition(this Transform trans, float? x = null, float? y = null, float? z = null)
        {
            trans.localPosition = trans.localPosition.Set(x, y, z);
            return trans;
        }

        public static Transform SetLocalRotation(this Transform trans, float? x = null, float? y = null, float? z = null)
        {
            trans.localRotation = Quaternion.Euler(Vector3.zero.Set(x, y, z));
            return trans;
        }

        public static Transform SetLocalScale(this Transform trans, float? x = null, float? y = null, float? z = null)
        {
            trans.localScale = trans.localScale.Set(x, y, z);
            return trans;
        }

        public static Transform SetLossyScale(this Transform trans, float? x = null, float? y = null, float? z = null)
        {
            var lossyScale = trans.lossyScale.Set(x, y, z);
            trans.localScale = Vector3.one;
            trans.localScale = new Vector3(lossyScale.x / trans.lossyScale.x,
                                               lossyScale.y / trans.lossyScale.y,
                                               lossyScale.z / trans.lossyScale.z);

            return trans;
        }

        public static Transform SetEulerAngles(this Transform trans, float? x = null, float? y = null, float? z = null)
        {
            trans.eulerAngles = trans.eulerAngles.Set(x, y, z);
            return trans;
        }

        public static Transform SetLocalEulerAngles(this Transform trans, float? x = null, float? y = null, float? z = null)
        {
            trans.localEulerAngles = trans.localEulerAngles.Set(x, y, z);
            return trans;
        }

        public static Transform Translate(this Transform comp, float? x = null, float? y = null, float? z = null)
        {
            Vector3 offset = Vector3.zero.Set(x, y, z);
            comp.position += offset;
            return comp;
        }

        public static T ResetRotation<T>(this T comp) where T : Component
        {
            comp.transform.rotation = Quaternion.identity;
            return comp;
        }

        public static T ResetScale<T>(this T comp) where T : Component
        {
            comp.transform.localScale = Vector3.one;
            return comp;
        }

        public static T ResetPosition<T>(this T comp) where T : Component
        {
            comp.transform.position = Vector3.zero;
            return comp;
        }

        public static T ResetLocalPosition<T>(this T comp) where T : Component
        {
            comp.transform.localPosition = Vector3.zero;
            return comp;
        }

        public static T Reset<T>(this T comp) where T : Component
        {
            comp.ResetRotation().ResetPosition().ResetScale();
            return comp;
        }

        public static T RotateAround<T>(this T comp, float? x = null, float? y = null, float? z = null) where T : Component
        {
            Vector3 offset = Vector3.zero.Set(x, y, z);
            comp.transform.Rotate(offset);
            return comp;
        }

        #endregion TransformChain

        #region Transform相关的拓展

        public static Component TryGetComponent(this Transform trans, Type compType, string name = null)
        {
            Transform temp;
            if (name.IsNullOrEmpty()) { temp = trans; }
            else { temp = trans.Find(name); }
            if (temp != null)
            {
                return temp.GetComponent(compType);
            }
            else
            {
                Debug.Log(string.Format("TryGetComponent失败 {0}", name));
                return null;
            }
        }

        public static T TryGetComponent<T>(this Transform trans, string name = null)
        {
            Transform temp;
            if (name.IsNullOrEmpty()) { temp = trans; }
            else { temp = trans.Find(name); }
            if (temp != null)
            {
                return temp.GetComponent<T>();
            }
            else
            {
                Debug.Log(string.Format("TryGetComponent失败 {0}", name));
                return default(T);
            }
        }

        public static T[] TryGetChindComponents<T>(this Transform trans, string name = null)
        {
            Transform temp;
            if (name.IsNullOrEmpty()) { temp = trans; }
            else { temp = trans.Find(name); }
            if (temp != null)
            {
                return temp.GetComponentsInChildren<T>();
            }
            else
            {
                Debug.Log(string.Format("TryGetChindComponents失败 {0}", name));
                return null;
            }
        }

        /// <summary>
        /// 二维空间下使 <see cref="UnityEngine.Transform" /> 指向指向目标点的算法，使用世界坐标。
        /// </summary>
        /// <param name="t"><see cref="UnityEngine.Transform" /> 对象。</param>
        /// <param name="lookAtPoint2D">要朝向的二维坐标点。</param>
        /// <remarks>假定其 forward 向量为 <see cref="UnityEngine.Vector3.up" />。</remarks>
        public static void LookAt2D(this Transform t, Vector2 lookAtPoint2D)
        {
            Vector3 vector = lookAtPoint2D.ToVector3() - t.position;
            vector.y = 0f;

            if (vector.magnitude > 0f)
            {
                t.rotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
            }
        }

        public static bool HasChild(this Transform trans, string name)
        {
            if (trans.IsNull()) { return false; } // TODO: raise exception or log error
            for (int i = 0; i < trans.childCount; i++)
            {
                if (trans.GetChild(i).name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsVisible(this Transform trans)
        {
            if (trans.localScale == Vector3.zero)
                return false;
            else
                return true;
        }

        public static bool IsEnabledAndVisible(this Transform trans)
        {
            return (trans.gameObject.activeSelf && trans.localScale == Vector3.zero);
        }

        #endregion Transform相关的拓展
    }
}