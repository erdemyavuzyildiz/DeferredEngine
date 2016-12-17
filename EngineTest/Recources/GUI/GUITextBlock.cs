﻿using System;
using System.Text;
using EngineTest.Main;
using EngineTest.Renderer.RenderModules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTest.Recources.GUI
{
    /// <summary>
    /// Just a colored block with a text inside
    /// </summary>
    public class GUITextBlock : GUIBlock
    {
        public StringBuilder Text;
        public Color TextColor;
        public SpriteFont TextFont;

        public override Vector2 Dimensions
        {
            get { return _dimensions; }
            set
            {
                _dimensions = value;
                ComputeFontPosition();
            }
        }

        private Vector2 _fontPosition;
        private Vector2 _dimensions;

        public GUITextBlock(Vector2 position, Vector2 dimensions, String text, SpriteFont font, Color blockColor, Color textColor, int layer = 0, GUICanvas.GUIAlignment alignment = GUICanvas.GUIAlignment.None, Vector2 parentDimensions = default(Vector2)) : base(position,dimensions, blockColor, layer, alignment, parentDimensions)
        {
            Text = new StringBuilder(text);
            TextColor = textColor;
            TextFont = font;
            ComputeFontPosition();
        }

        protected virtual void ComputeFontPosition()
        {
            if (Text == null) return;
            Vector2 fontDim = TextFont.MeasureString(Text);
            _fontPosition = Dimensions/2 - fontDim/2;
        }

        public override void Draw(GUIRenderer guiRenderer, Vector2 parentPosition)
        {
            guiRenderer.DrawQuad(parentPosition + Position, Dimensions, Color);
            guiRenderer.DrawText(parentPosition + Position + _fontPosition, Text, TextFont, TextColor);
        }
        
    }

    public class GUITextBlockToggle : GUITextBlock
    {
        public bool Toggle;

        private const float ToggleIndicatorSize = 20;
        private const float ToggleIndicatorBorder = 10;

        private Vector2 _fontPosition;

        public GUITextBlockToggle(Vector2 position, Vector2 dimensions, String text, SpriteFont font, Color blockColor, Color textColor, int layer = 0, GUICanvas.GUIAlignment alignment = GUICanvas.GUIAlignment.None, Vector2 parentDimensions = default(Vector2)) : base(position, dimensions, text, font, blockColor, textColor, layer)
        {
            //Text = new StringBuilder(text);
            //TextColor = textColor;
            //TextFont = font;
            //ComputeFontPosition();
        }

        protected override void ComputeFontPosition()
        {
            if (Text == null) return;
            Vector2 fontDim = TextFont.MeasureString(Text);
            _fontPosition = (Dimensions - Vector2.UnitX * (ToggleIndicatorSize + ToggleIndicatorBorder * 2)) / 2 - fontDim / 2;
        }

        public override void Draw(GUIRenderer guiRenderer, Vector2 parentPosition)
        {
            guiRenderer.DrawQuad(parentPosition + Position, Dimensions, Color);
            guiRenderer.DrawQuad(parentPosition + Position + Dimensions*Vector2.UnitX - Vector2.UnitX * ToggleIndicatorSize - Vector2.UnitX*ToggleIndicatorBorder  + Vector2.UnitY*ToggleIndicatorBorder , Vector2.One * ToggleIndicatorSize, Toggle ? Color.Green : Color.Red);
            guiRenderer.DrawText(parentPosition + Position + _fontPosition, Text, TextFont, TextColor);
        }

        public override void Update(GameTime gameTime, Vector2 mousePosition, Vector2 parentPosition)
        {
            if (!Input.WasLMBPressed()) return;

            Vector2 bound1 = Position + parentPosition;
            Vector2 bound2 = bound1 + Dimensions;

            if (mousePosition.X >= bound1.X && mousePosition.Y >= bound1.Y && mousePosition.X < bound2.X &&
                mousePosition.Y < bound2.Y)
            {
                Toggle = !Toggle;
            }
        }

    }
}