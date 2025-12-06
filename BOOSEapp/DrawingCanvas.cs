using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using BOOSE;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The DrawingCanvas class is used to perform drawing operations on a bitmap surface.
    /// It extends the functionality of the BOOSE.Canvas class to support specific shapes and text rendering using GDI+.
    /// </summary>
    public class DrawingCanvas : BOOSE.Canvas
    {
        /// <summary>
        /// The Graphics object used for drawing on the bitmap.
        /// </summary>
        private Graphics g;

        /// <summary>
        /// The Pen object used for drawing lines and outlines.
        /// </summary>
        private Pen currentPen;

        /// <summary>
        /// The PictureBox control that displays the drawing.
        /// </summary>
        private PictureBox canvas;

        /// <summary>
        /// A flag indicating whether shapes should be filled when drawn.
        /// </summary>
        private bool isFillOn = false;

        /// <summary>
        /// A new instance of the DrawingCanvas class is initialized.
        /// The graphics context is created from the provided PictureBox, and the default pen colour is set to Red.
        /// </summary>
        /// <param name="outputCanvas">The PictureBox control where the output is displayed.</param>
        public DrawingCanvas(PictureBox outputCanvas) : base()
        {
            canvas = outputCanvas;
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            g = Graphics.FromImage(canvas.Image);
            // Change default to Red
            currentPen = new Pen(Color.Red, 1);
            base.Set(canvas.Width, canvas.Height);
            ClearCanvas();
        }

        /// <summary>
        /// The current colour of the pen is retrieved or set.
        /// </summary>
        public override object PenColour
        {
            get => currentPen.Color;
            set
            {
                if (value is Color color)
                {
                    currentPen.Color = color;
                }
            }
        }

        /// <summary>
        /// The pen colour is set using specific RGB values.
        /// </summary>
        /// <param name="red">The red component of the colour (0-255).</param>
        /// <param name="green">The green component of the colour (0-255).</param>
        /// <param name="blue">The blue component of the colour (0-255).</param>
        public override void SetColour(int red, int green, int blue)
        {
            Color color = Color.FromArgb(red, green, blue);
            this.PenColour = color;
        }

        /// <summary>
        /// A line is drawn from the current position to the specified coordinates.
        /// The canvas is refreshed to display the change.
        /// </summary>
        /// <param name="x">The x-coordinate of the destination.</param>
        /// <param name="y">The y-coordinate of the destination.</param>
        public override void DrawTo(int x, int y)
        {
            Point start = new Point(this.Xpos, this.Ypos);
            base.DrawTo(x, y);
            g.DrawLine(currentPen, start, new Point(this.Xpos, this.Ypos));
            Refresh();
        }

        /// <summary>
        /// The pen is moved to the specified coordinates without drawing a line.
        /// </summary>
        /// <param name="x">The target x-coordinate.</param>
        /// <param name="y">The target y-coordinate.</param>
        public override void MoveTo(int x, int y)
        {
            base.MoveTo(x, y);
        }

        /// <summary>
        /// The canvas is cleared and reset to a white background.
        /// The pen colour is reset to the default Red.
        /// </summary>
        public override void Clear()
        {
            base.Reset();
            g.Clear(Color.White);
            // IMPORTANT: Reset pen to default Red when clearing the canvas
            currentPen.Color = Color.Red;
            Refresh();
        }

        /// <summary>
        /// A circle is drawn centered at the current pen position.
        /// If filling is enabled, the circle is filled with the current pen colour.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="filled">A boolean indicating if the circle is filled.</param>
        public override void Circle(int radius, bool filled)
        {
            int diameter = radius * 2;
            int topLeftX = this.Xpos - radius;
            int topLeftY = this.Ypos - radius;
            Rectangle rect = new Rectangle(topLeftX, topLeftY, diameter, diameter);

            if (filled)
            {
                using (SolidBrush brush = new SolidBrush(currentPen.Color))
                {
                    g.FillEllipse(brush, rect);
                }
            }
            else
            {
                g.DrawEllipse(currentPen, rect);
            }
            Refresh();
        }

        /// <summary>
        /// A rectangle is drawn at the current pen position.
        /// If filling is enabled, the rectangle is filled with the current pen colour.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="filled">A boolean indicating if the rectangle is filled.</param>
        public override void Rectangle(int width, int height, bool filled)
        {
            Rectangle rect = new Rectangle(this.Xpos, this.Ypos, width, height);

            if (filled)
            {
                using (SolidBrush brush = new SolidBrush(currentPen.Color))
                {
                    g.FillRectangle(brush, rect);
                }
            }
            else
            {
                g.DrawRectangle(currentPen, rect);
            }
            Refresh();
        }

        /// <summary>
        /// A rectangle is drawn by calling the Rectangle method.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="filled">A boolean indicating if the rectangle is filled.</param>
        public override void Rect(int width, int height, bool filled)
        {
            Rectangle(width, height, filled);
        }

        /// <summary>
        /// A triangle is drawn based on the specified width and height.
        /// The triangle is defined by three points calculated from the current position.
        /// </summary>
        /// <param name="width">The width of the triangle base.</param>
        /// <param name="height">The height of the triangle.</param>
        public override void Tri(int width, int height)
        {
            Point[] points = new Point[]
            {
                new Point(this.Xpos + width / 2, this.Ypos),
                new Point(this.Xpos, this.Ypos + height),
                new Point(this.Xpos + width, this.Ypos + height)
            };

            if (isFillOn)
            {
                using (SolidBrush brush = new SolidBrush(currentPen.Color))
                {
                    g.FillPolygon(brush, points);
                }
            }
            else
            {
                g.DrawPolygon(currentPen, points);
            }
            Refresh();
        }

        /// <summary>
        /// Text is written at the current pen position.
        /// The text is rendered using a standard Sans Serif font.
        /// </summary>
        /// <param name="text">The string of text to be written.</param>
        public override void WriteText(string text)
        {
            using (Font font = new Font(FontFamily.GenericSansSerif, 12))
            using (SolidBrush brush = new SolidBrush(currentPen.Color))
            {
                g.DrawString(text, font, brush, this.Xpos, this.Ypos);
            }
            Refresh();
        }

        /// <summary>
        /// The underlying bitmap image is retrieved from the canvas.
        /// </summary>
        /// <returns>The Bitmap object representing the canvas content.</returns>
        public override object getBitmap()
        {
            return canvas.Image;
        }

        /// <summary>
        /// The Clear method is called to clear the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            Clear();
        }

        /// <summary>
        /// The PictureBox is invalidated to force a repaint of the canvas.
        /// </summary>
        public void Refresh()
        {
            canvas.Invalidate();
        }

        /// <summary>
        /// The current position of the pen is retrieved as a Point object.
        /// </summary>
        /// <returns>A Point representing the current X and Y coordinates.</returns>
        public Point GetCurrentPosition()
        {
            return new Point(this.Xpos, this.Ypos);
        }

        /// <summary>
        /// The fill state is set to determine if shapes are filled or outlined.
        /// </summary>
        /// <param name="state">The boolean state to set (true for filled, false for outline).</param>
        public void SetFill(bool state)
        {
            this.isFillOn = state;
        }

        /// <summary>
        /// The pen colour is set to the specified Color object.
        /// </summary>
        /// <param name="color">The colour to be applied to the pen.</param>
        public void SetPenColour(Color color)
        {
            this.PenColour = color;
        }

        /// <summary>
        /// A rectangle is drawn using the current fill state settings.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public void DrawRectangle(int width, int height)
        {
            Rectangle(width, height, isFillOn);
        }

        /// <summary>
        /// A circle is drawn using the current fill state settings.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        public void DrawCircle(int radius)
        {
            Circle(radius, isFillOn);
        }
    }
}