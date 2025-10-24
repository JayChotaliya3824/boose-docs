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
        private Point currentPosition; // Our UNRESTRICTED pen position
        private PictureBox canvas;

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

        /// <summary>
        /// This function clears the canvas to white and refreshes.
        /// </summary>
        public void ClearCanvas()
        {
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

        /// <summary>
        /// Replaces the 'rect' command.
        /// </summary>
        public void DrawRectangle(int width, int height)
        {
            g.DrawRectangle(currentPen, currentPosition.X, currentPosition.Y, width, height);
            Refresh();
        }

        
        public void DrawCircle(int radius)
        {
            g.DrawEllipse(currentPen, currentPosition.X - radius, currentPosition.Y - radius, radius * 2, radius * 2);
            Refresh();
        }
    }
}