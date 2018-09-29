namespace QuickEngine.Extensions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class UnityComponentExtensions
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
        public static T RequireComponent<T>(this Component com) where T : Component
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
        public static T RequireComponent<T>(this Component com, ref bool isAddNewComp) where T : Component
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
        /// <param name="go"></param>
        /// <param name="checkChildren"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject go, bool checkChildren) where T : Component
        {
            if (!checkChildren)
            {
                return go.GetComponent<T>();
            }
            else
            {
                return go.GetComponentsInChildren<T>().FirstOrDefault() != null;
            }
        }
    }
}