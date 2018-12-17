namespace QuickEngine.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;

    public static class UnityEngineExtensions
    {
        #region AudioClip Extensions

        public static byte[] GetData(this AudioClip clip)
        {
            float[] data = new float[clip.samples * clip.channels];

            clip.GetData(data, 0);

            byte[] bytes = new byte[data.Length * 4];
            Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);

            return bytes;
        }

        public static void SetData(this AudioClip clip, byte[] bytes)
        {
            float[] data = new float[bytes.Length / 4];
            Buffer.BlockCopy(bytes, 0, data, 0, bytes.Length);

            clip.SetData(data, 0);
        }

        #endregion AudioClip Extensions

        #region Color Extensions

        public static Color FromRGB(int r, int g, int b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public static Color FromRGBA(int r, int g, int b, int a)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        /// <summary>
        /// 透明
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToTransparent(this Color color)
        {
            return color.SetAlpha(0);
        }

        /// <summary>
        /// 不透明
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToOpaque(this Color color)
        {
            return color.SetAlpha(1);
        }

        public static Color SetColor(this Color color, int? r = null, int? g = null, int? b = null, int? a = null)
        {
            if (r.HasValue) color.r = r.Value / 255f;
            if (g.HasValue) color.g = g.Value / 255f;
            if (b.HasValue) color.b = b.Value / 255f;
            if (a.HasValue) color.a = a.Value / 255f;
            return color;
        }

        public static Color SetColor(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;
            return color;
        }

        public static Color SetAlpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        #endregion Color Extensions

        #region Component Extensions

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

        public static Component SetActiveSafe(this Component comp, bool active)
        {
            if (active != comp.gameObject.activeSelf)
            {
                comp.gameObject.SetActive(active);
            }
            return comp;
        }

        /// <summary>
        /// 隐藏 替代SetActive
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public static Component SetVisible(this Component comp, bool isVisible)
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

        #endregion Component Extensions

        #region LayerMask Extensions

        public static LayerMask Create(params string[] layerNames)
        {
            return NamesToMask(layerNames);
        }

        public static LayerMask Create(params int[] layerNumbers)
        {
            return LayerNumbersToMask(layerNumbers);
        }

        public static LayerMask NamesToMask(params string[] layerNames)
        {
            LayerMask ret = (LayerMask)0;
            foreach (var name in layerNames)
            {
                ret |= (1 << LayerMask.NameToLayer(name));
            }
            return ret;
        }

        public static LayerMask LayerNumbersToMask(params int[] layerNumbers)
        {
            LayerMask ret = (LayerMask)0;
            foreach (var layer in layerNumbers)
            {
                ret |= (1 << layer);
            }
            return ret;
        }

        public static LayerMask Inverse(this LayerMask original)
        {
            return ~original;
        }

        public static LayerMask AddToMask(this LayerMask original, params string[] layerNames)
        {
            return original | NamesToMask(layerNames);
        }

        public static LayerMask RemoveFromMask(this LayerMask original, params string[] layerNames)
        {
            LayerMask invertedOriginal = ~original;
            return ~(invertedOriginal | NamesToMask(layerNames));
        }

        public static string[] MaskToNames(this LayerMask original)
        {
            var output = new List<string>();

            for (int i = 0; i < 32; ++i)
            {
                int shifted = 1 << i;
                if ((original & shifted) == shifted)
                {
                    string layerName = LayerMask.LayerToName(i);
                    if (!string.IsNullOrEmpty(layerName))
                    {
                        output.Add(layerName);
                    }
                }
            }
            return output.ToArray();
        }

        public static string MaskToString(this LayerMask original)
        {
            return MaskToString(original, ", ");
        }

        public static string MaskToString(this LayerMask original, string delimiter)
        {
            return string.Join(delimiter, MaskToNames(original));
        }

        public static bool IsInLayerMask(this GameObject obj, LayerMask mask)
        {
            return ((mask.value & (1 << obj.layer)) > 0);
        }

        #endregion LayerMask Extensions

        #region RectTransform Extensions

        public static void SetDefaultScale(this RectTransform trans)
        {
            trans.localScale = new Vector3(1, 1, 1);
        }

        public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
        {
            trans.pivot = aVec;
            trans.anchorMin = aVec;
            trans.anchorMax = aVec;
        }

        public static Vector2 GetSize(this RectTransform trans)
        {
            return trans.rect.size;
        }

        public static float GetWidth(this RectTransform trans)
        {
            return trans.rect.width;
        }

        public static float GetHeight(this RectTransform trans)
        {
            return trans.rect.height;
        }

        public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
        }

        public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
        }

        public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
        }

        public static void SetSize(this RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
            trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
        }

        public static void SetWidth(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(newSize, trans.rect.size.y));
        }

        public static void SetHeight(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(trans.rect.size.x, newSize));
        }

        #endregion RectTransform Extensions

        #region Vector Extensions

        #region Fields

        public static readonly Vector3 Back = new Vector3(0, 0, -1);

        public static readonly Vector3 Down = new Vector3(0, -1, 0);

        public static readonly Vector3 Forward = new Vector3(0, 0, 1);

        public static readonly Vector3 Left = new Vector3(-1, 0, 0);

        public static readonly Vector3 One = new Vector3(1, 1, 1);

        public static readonly Vector3 Right = new Vector3(1, 0, 0);

        public static readonly Vector3 Up = new Vector3(0, 1, 0);

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);

        #endregion Fields

        public static Vector3 Random(float min, float max)
        {
            return new Vector3()
            {
                x = UnityEngine.Random.Range(min, max),
                y = UnityEngine.Random.Range(min, max),
                z = UnityEngine.Random.Range(min, max),
            };
        }

        public static Vector2 Set(this Vector2 vector, float? x = null, float? y = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            return vector;
        }

        public static Vector3 Set(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            if (z.HasValue) vector.z = z.Value;
            return vector;
        }

        public static Vector4 Set(this Vector4 vector, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            if (z.HasValue) vector.z = z.Value;
            if (w.HasValue) vector.w = w.Value;
            return vector;
        }

        #region Vector相关的拓展

        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0f, vector2.y);
        }

        public static Vector3 ToVector3(this Vector2 vector2, float y)
        {
            return new Vector3(vector2.x, y, vector2.y);
        }

        #endregion Vector相关的拓展

        #endregion Vector Extensions

        #region UnityAction Extensions

        public static bool InvokeGracefully(this UnityAction action)
        {
            if (null != action)
            {
                action();
                return true;
            }
            return false;
        }

        public static bool InvokeGracefully<T1>(this UnityAction<T1> action, T1 arg1)
        {
            if (null != action)
            {
                action(arg1);
                return true;
            }
            return false;
        }

        public static bool InvokeGracefully<T1, T2>(this UnityAction<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (null != action)
            {
                action(arg1, arg2);
                return true;
            }
            return false;
        }

        public static bool InvokeGracefully<T1, T2, T3>(this UnityAction<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (null != action)
            {
                action(arg1, arg2, arg3);
                return true;
            }
            return false;
        }

        public static bool InvokeGracefully<T1, T2, T3, T4>(this UnityAction<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (null != action)
            {
                action(arg1, arg2, arg3, arg4);
                return true;
            }
            return false;
        }

        #endregion UnityAction Extensions
    }
}
