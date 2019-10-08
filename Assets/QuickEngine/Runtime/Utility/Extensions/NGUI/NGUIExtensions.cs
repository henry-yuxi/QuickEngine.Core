#if UNITY_NGUI
namespace QuickEngine.Extensions
{
    using UnityEngine;
    using System.Collections.Generic;

    public static partial class NGUIExtensions
    {
#region UIEventListener Extensions

        public static UIEventListener TryGetUIEventListener<T>(this Transform trans, string name) where T : Component
        {
            return UIEventListener.Get(trans.TryGetComponent<T>(name).gameObject);
        }

#region TryRegistOnClick

        public static void TryRegistOnClick(this Component component, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            TryRegistOnClick(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnClick(this Component component, string name, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            TryRegistOnClick(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnClick<T>(this Component component, string name, UIEventListener.VoidDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnClick(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnClick(this GameObject go, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onClick += action;
            }
            else
            {
                UIEventListener.Get(go).onClick = action;
            }
        }

#endregion TryRegistOnClick

#region TryRegistOnPress

        public static void TryRegistOnPress(this Component component, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            TryRegistOnPress(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnPress(this Component component, string name, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            TryRegistOnPress(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnPress<T>(this Component component, string name, UIEventListener.BoolDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnPress(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnPress(this GameObject go, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onPress += action;
            }
            else
            {
                UIEventListener.Get(go).onPress = action;
            }
        }

#endregion TryRegistOnPress

#region TryRegistOnDrag

        public static void TryRegistOnDrag(this Component component, UIEventListener.VectorDelegate action, bool isAddUp = false)
        {
            TryRegistOnDrag(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnDrag(this Component component, string name, UIEventListener.VectorDelegate action, bool isAddUp = false)
        {
            TryRegistOnDrag(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDrag<T>(this Component component, string name, UIEventListener.VectorDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnDrag(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDrag(this GameObject go, UIEventListener.VectorDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onDrag += action;
            }
            else
            {
                UIEventListener.Get(go).onDrag = action;
            }
        }

#endregion TryRegistOnDrag

#region TryRegistOnDragOver

        public static void TryRegistOnDragOver(this Component component, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            TryRegistOnDragOver(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnDragOver(this Component component, string name, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            TryRegistOnDragOver(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDragOver<T>(this Component component, string name, UIEventListener.VoidDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnDragOver(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDragOver(this GameObject go, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onDragOver += action;
            }
            else
            {
                UIEventListener.Get(go).onDragOver = action;
            }
        }

#endregion TryRegistOnDragOver

#region TryRegistOnDragOut

        public static void TryRegistOnDragOut(this Component component, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            TryRegistOnDragOut(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnDragOut(this Component component, string name, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            TryRegistOnDragOut(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDragOut<T>(this Component component, string name, UIEventListener.VoidDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnDragOut(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDragOut(this GameObject go, UIEventListener.VoidDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onDragOut += action;
            }
            else
            {
                UIEventListener.Get(go).onDragOut = action;
            }
        }

#endregion TryRegistOnDragOut

#region TryRegistOnDrop

        public static void TryRegistOnDrop(this Component component, UIEventListener.ObjectDelegate action, bool isAddUp = false)
        {
            TryRegistOnDrop(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnDrop(this Component component, string name, UIEventListener.ObjectDelegate action, bool isAddUp = false)
        {
            TryRegistOnDrop(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDrop<T>(this Component component, string name, UIEventListener.ObjectDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnDrop(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnDrop(this GameObject go, UIEventListener.ObjectDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onDrop += action;
            }
            else
            {
                UIEventListener.Get(go).onDrop = action;
            }
        }

#endregion TryRegistOnDrop

#region TryRegistOnSelect

        public static void TryRegistOnSelect(this Component component, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            TryRegistOnSelect(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnSelect(this Component component, string name, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            TryRegistOnSelect(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnSelect<T>(this Component component, string name, UIEventListener.BoolDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnSelect(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnSelect(this GameObject go, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onSelect += action;
            }
            else
            {
                UIEventListener.Get(go).onSelect = action;
            }
        }

#endregion TryRegistOnSelect

#region TryRegistOnHover

        public static void TryRegistOnHover(this Component component, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            TryRegistOnHover(component.gameObject, action, isAddUp);
        }

        public static void TryRegistOnHover(this Component component, string name, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            TryRegistOnHover(component.transform.TryGetComponent<Transform>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnHover<T>(this Component component, string name, UIEventListener.BoolDelegate action, bool isAddUp = false) where T : Component
        {
            TryRegistOnHover(component.transform.TryGetComponent<T>(name).gameObject, action, isAddUp);
        }

        public static void TryRegistOnHover(this GameObject go, UIEventListener.BoolDelegate action, bool isAddUp = false)
        {
            if (go == null) { return; }
            if (isAddUp)
            {
                UIEventListener.Get(go).onHover += action;
            }
            else
            {
                UIEventListener.Get(go).onHover = action;
            }
        }

#endregion TryRegistOnHover

#region TryUnRegistUIButton

        public static void TryUnRegistUIButton(this Component com)
        {
            TryUnRegistUIButton(com.gameObject);
        }

        public static void TryUnRegistUIButton(this Component component, string name)
        {
            TryUnRegistUIButton(component.transform.TryGetComponent<Transform>(name).gameObject);
        }

        public static void TryUnRegistUIButton(this GameObject go)
        {
            if (go == null) { return; }
            UIEventListener listener = UIEventListener.Get(go);
            listener.onClick = null;
            listener.onPress = null;
            listener.onDragOver = null;
            listener.onDrop = null;
            listener.onSelect = null;
            listener.onHover = null;
        }

#endregion TryUnRegistUIButton

#endregion UIEventListener Extensions

#region UIRoot Extensions

        public static UIRoot SetScalingStyle(this UIRoot mUIRoot, UIRoot.Scaling scalingStyle)
        {
            if (mUIRoot == null)
            {
                return null;
            }
            else
            {
                mUIRoot.scalingStyle = scalingStyle;
                return mUIRoot;
            }
        }

        public static UIRoot SetMinMaximumHeight(this UIRoot mUIRoot, int minimumHeight, int maximumHeight)
        {
            if (mUIRoot == null)
            {
                return null;
            }
            else
            {
                mUIRoot.minimumHeight = minimumHeight;
                mUIRoot.maximumHeight = maximumHeight;
                return mUIRoot;
            }
        }

        public static UIRoot SetManualWidthHeight(this UIRoot mUIRoot, int manualWidth, int manualHeight, bool fitWidth = false, bool fitHeight = false)

        {
            if (mUIRoot == null)
            {
                return null;
            }
            else
            {
                mUIRoot.manualWidth = manualWidth;
                mUIRoot.manualHeight = manualHeight;
                mUIRoot.fitWidth = fitWidth;
                mUIRoot.fitHeight = fitHeight;
                return mUIRoot;
            }
        }

#endregion UIRoot Extensions

#region UIWidget Extensions

        public static void SetDepth(this UIWidget mUIWidget, int depth)
        {
            if (mUIWidget == null) { return; }
            mUIWidget.depth = depth;
        }

        public static void SetMainTexture(this UIWidget mUIWidget, Texture texture)
        {
            if (mUIWidget == null) { return; }
            mUIWidget.mainTexture = texture;
        }

        public static void SetMaterial(this UIWidget mUIWidget, Material material)
        {
            if (mUIWidget == null) { return; }
            mUIWidget.material = material;
        }

        public static void SetShader(this UIWidget mUIWidget, Shader shader)
        {
            if (mUIWidget == null) { return; }
            mUIWidget.shader = shader;
        }

        public static void SetColor(this UIWidget mUIWidget, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            if (mUIWidget.IsNull()) { return; }
            mUIWidget.color = mUIWidget.color.SetColor(r, g, b, a);
        }

        public static void SetColor(this UIWidget mUIWidget, int? r = null, int? g = null, int? b = null, int? a = null)
        {
            if (mUIWidget.IsNull()) { return; }
            mUIWidget.color = mUIWidget.color.SetColor(r, g, b, a);
        }

#endregion UIWidget Extensions

#region UI2DSprite Extensions

        public static void SetSprite(this UI2DSprite mUI2DSprite, Sprite sprite2D)
        {
            if (mUI2DSprite == null) { return; }
            mUI2DSprite.sprite2D = sprite2D;
        }

#endregion UI2DSprite Extensions

#region UI2DSpriteAnimation Extensions

        public static void PlayForward(this UI2DSpriteAnimation mUI2DSpriteAnimation, int framesPerSecond, int frameIndex = 0, bool ignoreTimeScale = true, bool loop = false)
        {
            mUI2DSpriteAnimation.framesPerSecond = framesPerSecond;
            mUI2DSpriteAnimation.frameIndex = frameIndex;
            mUI2DSpriteAnimation.loop = loop;
            mUI2DSpriteAnimation.ignoreTimeScale = ignoreTimeScale;
            mUI2DSpriteAnimation.enabled = true;
            mUI2DSpriteAnimation.ResetToBeginning();
        }

#endregion UI2DSpriteAnimation Extensions

#region UIAtlas Extensions

        public static void SetMaterial(this UIAtlas mUIAtlas, Material material)
        {
            if (mUIAtlas == null) { return; }
            mUIAtlas.spriteMaterial = material;
        }

#endregion UIAtlas Extensions

#region UIInput Extensions

        public static void SetText(this UIInput mUIInput, object t)
        {
            if (mUIInput.IsNull()) { return; } // TODO: raise exception or log error
            if (t.IsNull()) { mUIInput.value = string.Empty; }
            mUIInput.value = t.ToString();
        }

        public static void SetText(this UIInput mUIInput, string format, params object[] objects)
        {
            if (mUIInput.IsNull()) { return; } // TODO: raise exception or log error
            if (format.IsNullOrEmpty()) { mUIInput.value = string.Empty; }
            mUIInput.value = format.Format(objects);
        }

        public static void TryRegistOnChange(this Component component, string name, EventDelegate.Callback callback)
        {
            TryRegistOnChange(component.transform.TryGetComponent<UIInput>(name), callback);
        }

        public static void TryRegistOnChange(this UIInput mUIInput, EventDelegate.Callback callback)
        {
            if (mUIInput == null) { return; }
            EventDelegate.Add(mUIInput.onChange, callback);
        }

        public static void TryUnRegistOnChange(this Component component, string name, EventDelegate.Callback callback)
        {
            TryUnRegistOnChange(component.transform.TryGetComponent<UIInput>(name), callback);
        }

        public static void TryUnRegistOnChange(this UIInput mUIInput, EventDelegate.Callback callback)
        {
            if (mUIInput == null) { return; }
            EventDelegate.Remove(mUIInput.onChange, callback);
        }

        public static void TryRegistOnSubmit(this Component component, string name, EventDelegate.Callback callback)
        {
            TryRegistOnSubmit(component.transform.TryGetComponent<UIInput>(name), callback);
        }

        public static void TryRegistOnSubmit(this UIInput mUIInput, EventDelegate.Callback callback)
        {
            if (mUIInput == null) { return; }
            EventDelegate.Add(mUIInput.onSubmit, callback);
        }

        public static void TryUnRegistOnSubmit(this Component component, string name, EventDelegate.Callback callback)
        {
            TryUnRegistOnSubmit(component.transform.TryGetComponent<UIInput>(name), callback);
        }

        public static void TryUnRegistOnSubmit(this UIInput mUIInput, EventDelegate.Callback callback)
        {
            if (mUIInput == null) { return; }
            EventDelegate.Remove(mUIInput.onSubmit, callback);
        }

        public static void TryUnRegistUIInput(this Component component, string name, EventDelegate.Callback callback)
        {
            TryUnRegistUIInput(component.transform.TryGetComponent<UIInput>(name), callback);
        }

        public static void TryUnRegistUIInput(this UIInput mUIInput, EventDelegate.Callback callback)
        {
            if (mUIInput == null) { return; }
            mUIInput.onSubmit = null;
            mUIInput.onChange = null;
        }

#endregion UIInput Extensions

#region UILabel Extensions

        public static void SetText(this UILabel mUILabel, object t)
        {
            if (mUILabel.IsNull()) { return; } // TODO: raise exception or log error
            if (t.IsNull()) { mUILabel.text = string.Empty; }
            mUILabel.text = t.ToString();
        }

        public static void SetText(this UILabel mUILabel, string format, params object[] objects)
        {
            if (mUILabel.IsNull()) { return; }
            if (format.IsNullOrEmpty()) { mUILabel.text = string.Empty; }
            mUILabel.text = format.Format(objects);
        }

        public static void SetWrapText(this UILabel mUILabel, object t)
        {
            if (mUILabel.IsNull()) { return; } // TODO: raise exception or log error
            if (t.IsNull()) { mUILabel.text = string.Empty; }
            string content = t.ToString();
            bool bWarp = mUILabel.Wrap(content, out content, mUILabel.height);
            if (!bWarp && !content.IsNullOrEmpty())
            {
                content = content.Substring(0, content.Length - 1);
                content = content.Insert(content.Length, "...");
            }
            else
            {
                content = t.ToString();
            }
            mUILabel.text = content;
        }

        public static void SetWrapText(this UILabel mUILabel, string format, params object[] objects)
        {
            if (mUILabel.IsNull()) { return; }
            if (format.IsNullOrEmpty()) { mUILabel.text = string.Empty; }
            string content = format.Format(objects);
            bool bWarp = mUILabel.Wrap(content, out content, mUILabel.height);
            if (!bWarp && !content.IsNullOrEmpty())
            {
                content = content.Substring(0, content.Length - 1);
                content = content.Insert(content.Length, "...");
            }
            else
            {
                content = format.Format(objects);
            }
            mUILabel.text = content;
        }

#endregion UILabel Extensions

#region UISprite Extensions

        public static void SetSprite(this UISprite sprite, string name)
        {
            if (sprite.IsNull() || name.IsNull()) { return; } // TODO: raise exception or log error
            sprite.spriteName = name;
        }

#endregion UISprite Extensions

#region UISpriteAnimation Extensions

        public static void PlayForward(this UISpriteAnimation mUISpriteAnimation, string namePrefix, int framesPerSecond, int frameIndex = 0, bool loop = false)
        {
            mUISpriteAnimation.namePrefix = namePrefix;
            mUISpriteAnimation.framesPerSecond = framesPerSecond;
            mUISpriteAnimation.frameIndex = frameIndex;
            mUISpriteAnimation.loop = loop;
            mUISpriteAnimation.enabled = true;
            mUISpriteAnimation.ResetToBeginning();
        }

#endregion UISpriteAnimation Extensions

#region UIButton Extensions

        public static void SetSprite(this UIButton mUIButton, string name)
        {
            if (mUIButton.IsNull() || name.IsNullOrEmpty()) { return; } // TODO: raise exception or log error
            mUIButton.disabledSprite = name;
            mUIButton.hoverSprite = name;
            mUIButton.pressedSprite = name;
            mUIButton.normalSprite = name;
        }

        private static Dictionary<int, BoxCollider> mBoxColliderDict = new Dictionary<int, BoxCollider>();

        public static void SetBoxCollider(this UIButton mUIButton, bool isVisible)
        {
            if (mUIButton.IsNull()) { return; }
            if (isVisible)
            {
                mUIButton.SetState(UIButtonColor.State.Normal, true);
            }
            else
            {
                mUIButton.SetState(UIButtonColor.State.Disabled, true);
            }
            int code = mUIButton.GetHashCode();
            BoxCollider mCacheBoxCollider;
            if (!mBoxColliderDict.TryGetValue(code, out mCacheBoxCollider))
            {
                mCacheBoxCollider = mUIButton.GetComponent<BoxCollider>();
                mBoxColliderDict.TryAdd(code, mCacheBoxCollider);
            }
            mCacheBoxCollider.enabled = isVisible;
        }

#endregion UIButton Extensions

#region UITexture Extensions

        public static void SetMainTexture(this UITexture mUITexture, Texture texture)
        {
            if (mUITexture == null) { return; }
            mUITexture.mainTexture = texture;
        }

#endregion UITexture Extensions

//        /// <summary>
//        /// 世界坐标转屏幕坐标
//        /// </summary>
//        /// <param name="point"></param>
//        /// <returns></returns>
//        public static Vector3 WorldToScreen(this Vector3 pos)
//        {
//            return Camera.main.WorldToScreenPoint(pos);
//        }

//        /// <summary>
//        /// 屏幕坐标转世界坐标
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static Vector3 ScreenToWorld(this Vector3 pos)
//        {
//            return Camera.main.ScreenToWorldPoint(pos);
//        }

//        /// <summary>
//        /// 屏幕坐标转NGUI坐标  是position而不是localPosition
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static Vector3 ScreenToNGUI(this Vector3 pos)
//        {
//            return UICamera.currentCamera.ScreenToWorldPoint(pos);
//        }

//        /// <summary>
//        /// NGUI坐标转屏幕坐标
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static Vector3 NGUIToScreen(this Vector3 pos)
//        {
//            return UICamera.currentCamera.WorldToScreenPoint(pos);
//        }

//        /// <summary>
//        /// 世界坐标转NGUI坐标
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static Vector3 WorldToNGUI(this Vector3 pos)
//        {
//            Vector3 mPos = Camera.main.WorldToScreenPoint(pos);
//            mPos = UICamera.currentCamera.ScreenToWorldPoint(mPos);
//            mPos.z = 0;
//            return mPos;
//        }

//        public static Vector3 NGUIToWorld(this Vector3 pos)
//        {
//            Vector3 mPos = UICamera.currentCamera.WorldToScreenPoint(pos);
//            mPos = Camera.main.ScreenToWorldPoint(mPos);
//            mPos.y = 0;
//            return mPos;
//        }
//    }
}
}
#endif