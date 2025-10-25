// DrawingCanvas.cs
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace BOOSEInterpreter
{
    /// <summary>
    /// This class manages the drawing state and operations on a PictureBox.
    /// </summary>
    public class DrawingCanvas
    {
        private Graphics g;
        private Pen currentPen;
        private Point currentPosition; 
        private PictureBox canvas;
        private bool isFillOn = false; 
        /// <summary>
        /// It initializes a new DrawingCanvas linked to a PictureBox.
        /// </summary>
        /// <param name="outputCanvas">The PictureBox to draw on.</param>
        public DrawingCanvas(PictureBox outputCanvas)
        {
            canvas = outputCanvas;
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            g = Graphics.FromImage(canvas.Image);

            currentPen = new Pen(Color.Black, 1);
            currentPosition = new Point(0, 0);
            ClearCanvas();
        }

        
        public void ClearCanvas()
        {
            // Check if the bitmap is null, or if the PictureBox size is different.
            if (canvas.Image == null || canvas.Image.Width != canvas.Width || canvas.Image.Height != canvas.Height)
            {
                // If so, create a new, correctly-sized Bitmap
                // and get a new Graphics object for it.
                canvas.Image = new Bitmap(canvas.Width, canvas.Height);
                g = Graphics.FromImage(canvas.Image);

                Debug.WriteLine($"Created new Bitmap with size {canvas.Width}x{canvas.Height}");
            }

            // Now, clear the (correctly sized) bitmap
            g.Clear(Color.White);
            Refresh();
        }
        /// <summary>
        ///This function forces the PictureBox to repaint itself.
        /// </summary>
        public void Refresh()
        {
            canvas.Invalidate();
        }

        /// <summary>
        /// Gets the current pen position. For testing purposes.
        /// </summary>
        public Point GetCurrentPosition()
        {
            return currentPosition;
        }

        public void SetFill(bool state) 
        {
            this.isFillOn = state;
            Debug.WriteLine($"Fill set to: {state}");
        }
        /// <summary>
        /// Replaces the 'moveto' command.
        /// </summary>
        public void MoveTo(int x, int y)
        {
            currentPosition = new Point(x, y);
            Debug.WriteLine($"Moved to: {currentPosition}");
        }

        /// <summary>
        /// Replaces the 'drawto' command.
        /// </summary>
        public void DrawTo(int x, int y)
        {
            Point newPosition = new Point(x, y);
            g.DrawLine(currentPen, currentPosition, newPosition);
            currentPosition = newPosition; // Update position
            Refresh();
        }

        /// <summary>
        /// Replaces the 'pencolour' command.
        /// </summary>
        public void SetPenColour(Color color)
        {
            currentPen.Color = color;
            Debug.WriteLine($"Pen colour set to: {color}");
        }

    
        public void DrawRectangle(int width, int height)
        {
            if (isFillOn)
            {
                g.FillRectangle(new SolidBrush(currentPen.Color), currentPosition.X, currentPosition.Y, width, height);
            }
            else
            {
                g.DrawRectangle(currentPen, currentPosition.X, currentPosition.Y, width, height);
            }
            Refresh();
        }


        public void DrawCircle(int radius)
        {
            int topLeftX = currentPosition.X - radius;
            int topLeftY = currentPosition.Y - radius;

            if (isFillOn)
            {
                g.FillEllipse(new SolidBrush(currentPen.Color), topLeftX, topLeftY, radius * 2, radius * 2);
            }
            else
            {
                g.DrawEllipse(currentPen, topLeftX, topLeftY, radius * 2, radius * 2);
            }
            Refresh();
        }

    }
}