// UnitTest1.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BOOSEInterpreter; // Your main project
using System.Drawing;
using System.Windows.Forms;

[TestClass]
public class CommandTests
{
    private PictureBox dummyCanvas = new PictureBox();

    [TestMethod]
    public void TestMoveToUpdatesPosition() // Required by Checkpoint Task 2
    {
        // Arrange
        DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
        int targetX = 123, targetY = 456;

        // Act
        canvas.MoveTo(targetX, targetY);
        Point finalPosition = canvas.GetCurrentPosition();

        // Assert
        Assert.AreEqual(targetX, finalPosition.X);
        Assert.AreEqual(targetY, finalPosition.Y);
    }

    [TestMethod]
    public void TestDrawToUpdatesPosition() // Required by Checkpoint Task 2
    {
        // Arrange
        DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
        int targetX = 80, targetY = 90;

        // Act
        canvas.DrawTo(targetX, targetY);
        Point finalPosition = canvas.GetCurrentPosition();

        // Assert
        Assert.AreEqual(targetX, finalPosition.X);
        Assert.AreEqual(targetY, finalPosition.Y);
    }

    [TestMethod]
    public void TestMultiLineProgram() // Required by Checkpoint Task 2
    {
        // Arrange
        DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
        Parser parser = new Parser(canvas);
        string[] program = { "moveto 10 20", "drawto 50 50" };

        // Act
        foreach (string line in program) { parser.ParseCommand(line); }
        Point finalPosition = canvas.GetCurrentPosition();

        // Assert
        Assert.AreEqual(50, finalPosition.X);
        Assert.AreEqual(50, finalPosition.Y);
    }
}