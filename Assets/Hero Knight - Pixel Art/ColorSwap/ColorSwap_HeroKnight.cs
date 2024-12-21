using System.Collections.Generic;
using UnityEngine;

namespace Hero_Knight___Pixel_Art.ColorSwap
{
    public class ColorSwapHeroKnight : MonoBehaviour
    {
        // Accessable in Editor
        [SerializeField] private Color[] mSourceColors;
        [SerializeField] private Color[] mNewColors;

        // Private member variables
        private Texture2D mColorSwapTex;
        private Color[] mSpriteColors;
        private SpriteRenderer mSpriteRenderer;
        private bool mInit = false;

        // Initialize values
        private void Awake()
        {
            mSpriteRenderer = GetComponent<SpriteRenderer>();
            InitColorSwapTex();

            SwapDemoColors();
        }

        // OnValidate is called every time m_sourceColors or m_newColors is changed in editor. 
        // Only possible to change colors in real time when in play mode.
        private void OnValidate()
        {
            if (mInit)
            {
                SwapDemoColors();
            }
        }

        // Uses the value from the red channel in the source color (0-255) as an index for where to place the new color into the swap texture (256x1 px)
        public void SwapDemoColors()
        {
            for (int i = 0; i < mSourceColors.Length && i < mNewColors.Length; i++)
            {
                SwapColor((int)(mSourceColors[i].r * 255.0f), mNewColors[i]);
            }

            if (mColorSwapTex)
                mColorSwapTex.Apply();
        }

        public static Color ColorFromInt(int c, float alpha = 1.0f)
        {
            int r = (c >> 16) & 0x000000FF;
            int g = (c >> 8) & 0x000000FF;
            int b = c & 0x000000FF;

            Color ret = ColorFromIntRGB(r, g, b);
            ret.a = alpha;

            return ret;
        }

        public static Color ColorFromIntRGB(int r, int g, int b)
        {
            return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
        }

        public void InitColorSwapTex()
        {
            Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
            colorSwapTex.filterMode = FilterMode.Point;

            for (int i = 0; i < colorSwapTex.width; ++i)
                colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

            colorSwapTex.Apply();

            mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

            mSpriteColors = new Color[colorSwapTex.width];
            mColorSwapTex = colorSwapTex;
            mInit = true;
        }

        public void SwapColor(int index, Color color)
        {
            if (index >= 0 && index < 256)
            {
                mSpriteColors[index] = color;
                mColorSwapTex.SetPixel(index, 0, color);
            }
        }


        public void SwapColors(List<int> indexes, List<Color> colors)
        {
            for (int i = 0; i < indexes.Count; ++i)
            {
                mSpriteColors[indexes[i]] = colors[i];
                mColorSwapTex.SetPixel(indexes[i], 0, colors[i]);
            }

            mColorSwapTex.Apply();
        }

        public void ClearColor(int index)
        {
            Color c = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            mSpriteColors[index] = c;
            mColorSwapTex.SetPixel(index, 0, c);
        }

        public void SwapAllSpritesColorsTemporarily(Color color)
        {
            for (int i = 0; i < mColorSwapTex.width; ++i)
                mColorSwapTex.SetPixel(i, 0, color);
            mColorSwapTex.Apply();
        }

        public void ResetAllSpritesColors()
        {
            for (int i = 0; i < mColorSwapTex.width; ++i)
                mColorSwapTex.SetPixel(i, 0, mSpriteColors[i]);
            mColorSwapTex.Apply();
        }

        public void ClearAllSpritesColors()
        {
            for (int i = 0; i < mColorSwapTex.width; ++i)
            {
                mColorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
                mSpriteColors[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            }

            mColorSwapTex.Apply();
        }
    }
}
