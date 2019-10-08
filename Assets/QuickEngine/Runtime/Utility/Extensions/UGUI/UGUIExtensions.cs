namespace QuickEngine.Extensions
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public static partial class UGUIExtensions
    {
        public static void SetText(this Text text, object t)
        {
            if (text.IsNull()) { return; } // TODO: raise exception or log error
            if (t.IsNull()) { text.text = string.Empty; }
            text.text = t.ToString();
        }

        public static void SetText(this Text text, string format, params object[] objects)
        {
            if (text.IsNull()) { return; } // TODO: raise exception or log error
            if (format.IsNullOrEmpty()) { text.text = string.Empty; }
            text.text = format.Format(objects);
        }

        public static void SetTexture2D(this Image image, Texture2D texture)
        {
            if (image.IsNull() || texture.IsNull())
            {
                return;
            }
            image.overrideSprite =
                Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5F, 0.5F)
                );
        }

        public static Sprite ToSprite(this byte[] bytes, TextureFormat format, int width = 2, int height = 2, bool mipmap = false)
        {
            if (bytes.IsNull() || bytes.Length == 0 || width < 0 || height < 0)
            {
                return null;
            }
            Texture2D texture = new Texture2D(width, height, format, false);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5F, 0.5F));
        }

        public static byte[] ToBytesPNG(this Sprite sprite)
        {
            if (sprite.IsNull()) { return null; }
            return sprite.texture.EncodeToPNG();
        }

        public static byte[] ToBytesJPG(this Sprite sprite)
        {
            if (sprite.IsNull()) { return null; }
            return sprite.texture.EncodeToJPG();
        }

        public static Sprite ToSprite(this string base64, TextureFormat format, int width = 2, int height = 2, bool mipmap = false)
        {
            if (base64.IsNullOrEmpty()) { return null; }
            return Convert.FromBase64String(base64).ToSprite(format, width, height, mipmap);
        }
    }
}