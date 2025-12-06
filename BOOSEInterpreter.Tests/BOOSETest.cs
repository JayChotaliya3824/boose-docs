using Microsoft.VisualStudio.TestTools.UnitTesting;
using BOOSEInterpreter;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Reflection; // REQUIRED for the Singleton Reset Fix

namespace BOOSETests
{
    [TestClass]
    public class BOOSETests
    {
        // Fields for testing
        private PictureBox dummyCanvas = new PictureBox();
        private Dictionary<string, object> variables;
        private BOOSEInterpreter.CommandFactory factory;

        [TestInitialize]
        public void Setup()
        {
            // =========================================================
            // THE CRITICAL FIX: RESET THE SINGLETON
            // We force the Factory to forget the old Parser from the previous test.
            // We use Reflection because the _instance field is private.
            // This prevents "Expected: 10, Actual: 0" errors.
            // =========================================================
            FieldInfo field = typeof(BOOSEInterpreter.CommandFactory).GetField("_instance", BindingFlags.Static | BindingFlags.NonPublic);
            if (field != null)
            {
                field.SetValue(null, null);
            }

            // Reset variables dictionary
            variables = new Dictionary<string, object>();
            
            // Note: We do NOT initialize 'factory' here yet. We let the tests do it
            // either manually or via the Parser.
        }

        // Helper method to get the Factory safely for simple command tests
        private BOOSEInterpreter.CommandFactory GetFactory()
        {
            if (factory == null)
            {
                factory = BOOSEInterpreter.CommandFactory.Instance(null);
            }
            return factory;
        }

        // =================================================================
        // PART 1: DRAWING & MOVEMENT TESTS
        // =================================================================

        [TestMethod]
        public void TestMoveToUpdatesPosition()
        {
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            int targetX = 123, targetY = 456;

            canvas.MoveTo(targetX, targetY);
            Point finalPosition = canvas.GetCurrentPosition();

            Assert.AreEqual(targetX, finalPosition.X);
            Assert.AreEqual(targetY, finalPosition.Y);
        }

        [TestMethod]
        public void TestDrawToUpdatesPosition()
        {
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            int targetX = 80, targetY = 90;

            canvas.DrawTo(targetX, targetY);
            Point finalPosition = canvas.GetCurrentPosition();

            Assert.AreEqual(targetX, finalPosition.X);
            Assert.AreEqual(targetY, finalPosition.Y);
        }

        // =================================================================
        // PART 2: INTEGRATION TESTS (Full Program Logic)
        // =================================================================

        [TestMethod]
        public void TestMultiLineProgram()
        {
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            Parser parser = new Parser(canvas);
            string[] program = {
                "moveto 10 20",
                "drawto 50 50"
            };

            parser.ParseProgram(program);

            Point finalPosition = canvas.GetCurrentPosition();
            Assert.AreEqual(50, finalPosition.X);
            Assert.AreEqual(50, finalPosition.Y);
        }

        [TestMethod]
        public void TestVariableAssignment_Logic()
        {
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            Parser parser = new Parser(canvas);
            string[] program = {
                "int x = 50",
                "int y = 100",
                "x = x + 25", 
                "moveto x y"
            };

            parser.ParseProgram(program);

            Point finalPosition = canvas.GetCurrentPosition();
            Assert.AreEqual(75, finalPosition.X);
            Assert.AreEqual(100, finalPosition.Y);
        }

        [TestMethod]
        public void TestWhileLoop_Counter()
        {
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            Parser parser = new Parser(canvas);
            string[] program = {
                "int x = 0",
                "while x < 3",
                "moveto x 0",
                "x = x + 1",
                "endwhile"
            };

            parser.ParseProgram(program);

            Point finalPosition = canvas.GetCurrentPosition();
            Assert.AreEqual(2, finalPosition.X);
        }

        [TestMethod]
        public void TestMethodCall_WithParams()
        {
            // This test verifies that methods work and Parameters are passed correctly.
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

        // =================================================================
        // PART 3: BUG FIX TESTS (Verifying Constraints & Requirements)
        // =================================================================

        [TestMethod]
        public void TestFactory_CreatesCorrectInstance()
        {
            var f = GetFactory(); 
            ICommand cmd = f.MakeCommand("int");
            Assert.IsInstanceOfType(cmd, typeof(IntCommand));
        }

        [TestMethod]
        public void TestIntCommand_HandlesComplexExpressions()
        {
            // Verifies fix for "int x = 10 + 5"
            var cmd = new IntCommand();
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            string[] args = { "int", "score", "=", "10", "+", "20", "*", "2" };

            cmd.Execute(canvas, variables, args);

            Assert.AreEqual(50, variables["score"]);
        }

        [TestMethod]
        public void TestRealCommand_HandlesDecimals()
        {
            // Verifies fix for Real numbers
            var cmd = new RealCommand();
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            string[] args = { "real", "val", "=", "5.5", "+", "0.5" };

            cmd.Execute(canvas, variables, args);

            Assert.AreEqual(6.0, Convert.ToDouble(variables["val"]), 0.001);
        }

        [TestMethod]
        public void TestArray_PeekPreservesDecimals()
        {
            // Verifies fix for Array Data Loss
            DrawingCanvas canvas = new DrawingCanvas(dummyCanvas);
            
            double[] prices = new double[5];
            prices[0] = 10.5;
            variables["prices"] = prices;

            var peekCmd = new PeekCommand();
            string[] args = { "peek", "result", "prices", "0" };

            peekCmd.Execute(canvas, variables, args);

            Assert.IsTrue(variables.ContainsKey("result"));
            Assert.AreEqual(10.5, variables["result"]);
        }
    }
}