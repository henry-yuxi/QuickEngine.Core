namespace QuickEngine.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static partial class UnityEngineExtensions
    {
        #region GameObjectChain

        public static GameObject SetParent(this GameObject go, Transform parent)
        {
            go.transform.parent = parent;
            return go;
        }

        /// <summary>
        /// 隐藏按钮，setActive能不用尽量少用，效率问题。
        /// </summary>
        /// <param name="go"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public static GameObject SetVisible(this GameObject go, bool isVisible)
        {
            go.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
            return go;
        }

        #endregion GameObjectChain

        #region GameObject Extensions

        public static bool IsNullOrInactive(this GameObject go)
        {
            return ((go == null) || (!go.activeSelf));
        }

        public static bool IsActive(this GameObject go)
        {
            return ((go != null) && (go.activeSelf));
        }

        public static void ActivateAndParent(this GameObject go)
        {
            if (go == null)
                return;
            if (go.transform.parent != null)
                go.transform.parent.gameObject.ActivateAndParent();
            go.SetActive(true);
        }

        public static bool HasRigidbody(this GameObject go)
        {
            return (go.GetComponent<Rigidbody>() != null);
        }

        public static bool HasCharacterController(this GameObject go)
        {
            return (go.GetComponent<CharacterController>() != null);
        }

        public static bool HasAnimation(this GameObject go)
        {
            return (go.GetComponent<Animation>() != null);
        }

        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            return (go.GetComponent<T>() != null);
        }

        public static void SetLayerRecursively(this GameObject go, int layer)
        {
            go.layer = layer;
            foreach (Transform t in go.transform)
                t.gameObject.SetLayerRecursively(layer);
        }

        public static void SetCollisionRecursively(this GameObject go, bool enabled)
        {
            Collider GCollide = go.GetComponent<Collider>();
            if (GCollide != null)
                GCollide.enabled = enabled;

            foreach (Transform t in go.transform)
                t.gameObject.SetCollisionRecursively(enabled);
        }

        public static List<T> GetComponentsInChildrenWithTag<T>(this GameObject go, string tag) where T : Component
        {
            List<T> results = new List<T>();

            if (go.CompareTag(tag))
                results.Add(go.GetComponent<T>());

            foreach (Transform t in go.transform)
                results.AddRange(t.gameObject.GetComponentsInChildrenWithTag<T>(tag));

            return results;
        }

        public static T GetInterface<T>(this GameObject go) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + " is not an interface");
                return default(T);
            }
            return go.GetComponents<Component>().OfType<T>().FirstOrDefault();
        }

        public static T GetInterfaceInChildren<T>(this GameObject go) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + " is not an interface");
                return null;
            }

            return go.GetComponentsInChildren<Component>().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetInterfaces<T>(this GameObject go) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + " is not an interface");
                return Enumerable.Empty<T>();
            }

            return go.GetComponents<Component>().OfType<T>();
        }

        public static IEnumerable<T> GetInterfacesInChildren<T>(this GameObject go) where T : class
        {
            if (!typeof(T).IsInterface)
            {
                Debug.LogError(typeof(T).ToString() + " is not an interface");
                return Enumerable.Empty<T>();
            }

            return go.GetComponentsInChildren<Component>(true).OfType<T>();
        }

        public static bool ContainsLayer(this LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }

        /// <summary>
        /// 获取或创建组件
        /// </summary>
        /// <typeparam name="T">要获取或增加的组件</typeparam>
        /// <param name="child">目标对象</param>
        /// <returns>获取或增加的组件</returns>
        public static T GetOrAddComponent<T>(this GameObject child) where T : Component
        {
            T component = child.GetComponent<T>();
            if (component == null)
            {
                component = child.AddComponent<T>();
            }
            return component;
        }

        public static bool TryGetComponent<T>(this GameObject go, out T component)
        {
            component = go.GetComponent<T>();
            return component != null;
        }

        public static T TryGetComponent<T>(this GameObject go, string name = null)
        {
            return go.transform.TryGetComponent<T>(name);
        }

        public static T[] TryGetChindComponents<T>(this GameObject go, string name = null)
        {
            return go.transform.TryGetChindComponents<T>(name);
        }

        #endregion GameObject Extensions
    }
}