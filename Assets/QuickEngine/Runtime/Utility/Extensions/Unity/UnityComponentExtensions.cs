namespace QuickEngine.Extensions
{
    using System.Linq;
    using UnityEngine;

    public static partial class UnityEngineExtensions
    {
        //ColorAlpha

        /// <summary>
        /// 设置组件的层级
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static Component SetLayer(this Component comp, int layer)
        {
            comp.gameObject.layer = layer;
            return comp;
        }

        /// <summary>
        /// 设置组件的层级
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static Component SetLayer(this Component comp, string layerName)
        {
            comp.gameObject.layer = LayerMask.NameToLayer(layerName);
            return comp;
        }

        /// <summary>
        /// 设置组件的标签
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Component SetTag(this Component comp, string tag)
        {
            comp.transform.tag = tag;
            return comp;
        }

        /// <summary>
        /// 设置组件的名字
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Component SetName(this Component comp, string name)
        {
            comp.transform.name = name;
            return comp;
        }

        public static Component SetActiveSafe(this Component comp, bool active)
        {
            if (active != comp.gameObject.activeSelf)
            {
                comp.gameObject.SetActive(active);
            }
            return comp;
        }

        public static void SetEnableSafely(this Behaviour comp, bool enabled)
        {
            if (comp != null && comp.enabled != enabled)
            {
                comp.enabled = enabled;
            }
        }

        public static Component SetVisibleSafe(this Component comp, bool isVisible)
        {
            comp.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
            return comp;
        }

        /// <summary>
        /// 要求拥有某个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="com"></param>
        /// <returns></returns>
        public static T EnsureComponent<T>(this Component com) where T : Component
        {
            T comp = com.GetComponent<T>();
            if (comp == null)
            {
                comp = com.gameObject.AddComponent<T>();
            }
            return comp;
        }

        /// <summary>
        /// 要求拥有某个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="com"></param>
        /// <param name="isAddNewComp">是不是新增的组件</param>
        /// <returns></returns>
        public static T EnsureComponent<T>(this Component com, ref bool isAddNewComp) where T : Component
        {
            T comp = com.GetComponent<T>();
            if (isAddNewComp = (comp == null))
            {
                comp = com.gameObject.AddComponent<T>();
            }
            return comp;
        }

        /// <summary>
        /// 是否拥有某个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comp"></param>
        /// <param name="checkChildren"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this Component comp, bool checkChildren) where T : Component
        {
            if (!checkChildren)
            {
                return comp.GetComponent<T>();
            }
            else
            {
                return comp.GetComponentsInChildren<T>().FirstOrDefault() != null;
            }
        }

        public static bool SetParentSafe(this Component child, Component parent, bool worldPositionStays = true)
        {
            if (parent != null && child != null)
            {
                child.transform.SetParent(parent.transform, worldPositionStays);
                return true;
            }
            return false;
        }

        public static void AddChild(this Component transform, Component childTransform)
        {
            childTransform.SetParentSafe(transform, false);
        }

        public static void AddChild(this Component transform, Component childTransform, bool worldPositionStays)
        {
            childTransform.SetParentSafe(transform, worldPositionStays);
        }

        /// <summary>
        /// Convenience extension that destroys all children of the transform.
        /// </summary>
        public static void DestroyChildren(this Component comp)
        {
            bool isPlaying = Application.isPlaying;

            while (comp.transform.childCount != 0)
            {
                Transform child = comp.transform.GetChild(0);

                if (isPlaying)
                {
                    child.parent = null;
                    UnityEngine.Object.Destroy(child.gameObject);
                }
                else UnityEngine.Object.DestroyImmediate(child.gameObject);
            }
        }

        public static void Destroy(this UnityEngine.Object obj)
        {
            if (obj)
            {
                if (obj is Transform)
                {
                    Transform t = (obj as Transform);
                    GameObject go = t.gameObject;

                    if (Application.isPlaying)
                    {
                        t.parent = null;
                        UnityEngine.Object.Destroy(go);
                    }
                    else UnityEngine.Object.DestroyImmediate(go);
                }
                else if (obj is GameObject)
                {
                    GameObject go = obj as GameObject;
                    Transform t = go.transform;

                    if (Application.isPlaying)
                    {
                        t.parent = null;
                        UnityEngine.Object.Destroy(go);
                    }
                    else UnityEngine.Object.DestroyImmediate(go);
                }
                else if (Application.isPlaying) UnityEngine.Object.Destroy(obj);
                else UnityEngine.Object.DestroyImmediate(obj);
            }
        }

        /// <summary>
        /// Destroy the specified object immediately, unless not in the editor, in which case the regular Destroy is used instead.
        /// </summary>
        public static void DestroyImmediate(this UnityEngine.Object obj)
        {
            if (obj != null)
            {
                if (Application.isEditor) UnityEngine.Object.DestroyImmediate(obj);
                else UnityEngine.Object.Destroy(obj);
            }
        }
    }
}