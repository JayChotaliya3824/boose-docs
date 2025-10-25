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
    public void TestMoveToUpdatesPosition() 
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
    public void TestDrawToUpdatesPosition() 
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

    public void TestMultiLineProgram() 
    {
        // Arrange
        DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
        Parser parser = new Parser(canvas);
        string[] program = {
        "moveto 10 20",
        "drawto 50 50"
    };

       
        parser.ParseProgram(program);

        // Assert
        Point finalPosition = canvas.GetCurrentPosition();
        Assert.AreEqual(50, finalPosition.X);
        Assert.AreEqual(50, finalPosition.Y);
    }

    [TestMethod]
    public void TestVariableAssignment()
    {
        
        DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
        Parser parser = new Parser(canvas);
        string[] program = {
        "int x = 50",
        "int y = 100",
        "x = x + 25", // Tests VarCommand and ExpressionEvaluator
        "moveto x y"  // x should be 75
    };

        parser.ParseProgram(program);

        Point finalPosition = canvas.GetCurrentPosition();
        Assert.AreEqual(75, finalPosition.X);
        Assert.AreEqual(100, finalPosition.Y);
    }

    [TestMethod]
    public void TestWhileLoop()
    {
        // Tests Task 6 (While)
        DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
        Parser parser = new Parser(canvas);
        string[] program = {
        "int x = 0",
        "while x < 3",
        "moveto x 0", // Will move to 0, 1, 2
        "x = x + 1",
        "endwhile" 
        // After loop, x is 3, last moveto was x=2
    };

        parser.ParseProgram(program);

        Point finalPosition = canvas.GetCurrentPosition();
        Assert.AreEqual(2, finalPosition.X); 
    }

    [TestMethod]
    public void TestMethodCall()
    {
        DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
        Parser parser = new Parser(canvas);
        string[] program = {
        "int x = 10",
        "method draw(p)",
        "moveto p 100",
        "circle p",
        "endmethod",
        "call draw(x)"
    };

        parser.ParseProgram(program);

        Point finalPosition = canvas.GetCurrentPosition();
        Assert.AreEqual(10, finalPosition.X);
    }

}