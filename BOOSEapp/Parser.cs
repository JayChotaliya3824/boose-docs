using System;
using System.Collections.Generic;
using System.Linq;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The Parser class is implemented to process and execute BOOSE scripts.
    /// Script commands are analyzed line by line, flow control statements are managed, and methods are defined and invoked.
    /// The parser interacts with the CommandFactory to instantiate commands and the DrawingCanvas to reflect visual outputs.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// A reference to the DrawingCanvas is maintained for command execution.
        /// </summary>
        private DrawingCanvas canvas;

        /// <summary>
        /// A dictionary of global variables is stored to maintain state throughout program execution.
        /// </summary>
        private Dictionary<string, object> globalVariables;

        /// <summary>
        /// The CommandFactory instance is used to create specific command objects.
        /// </summary>
        private CommandFactory factory;

        /// <summary>
        /// A dictionary of method definitions is exposed to store and retrieve user-defined methods.
        /// </summary>
        public Dictionary<string, MethodDefinition> Methods { get; private set; }

        /// <summary>
        /// A flag is set to indicate whether the parser is currently recording a method definition.
        /// </summary>
        private bool isDefiningMethod = false;

        /// <summary>
        /// The name of the method currently being defined is stored.
        /// </summary>
        private string currentMethodName = "";

        /// <summary>
        /// The definition object for the method currently being parsed is held temporarily.
        /// </summary>
        private MethodDefinition currentMethod = null;

        /// <summary>
        /// A stack of ControlFlow objects is used to manage nested loops and conditional statements.
        /// </summary>
        private Stack<ControlFlow> controlStack = new Stack<ControlFlow>();

        /// <summary>
        /// The complete list of program lines is stored for indexed access during execution.
        /// </summary>
        private List<string> programLines;

        /// <summary>
        /// The index of the line currently being executed is tracked.
        /// </summary>
        private int currentLineIndex;

        /// <summary>
        /// A new instance of the Parser class is initialized.
        /// The canvas, variable storage, method storage, and command factory are set up for operation.
        /// </summary>
        /// <param name="canvas">The DrawingCanvas instance used for graphical output.</param>
        public Parser(DrawingCanvas canvas)
        {
            this.canvas = canvas;
            this.globalVariables = new Dictionary<string, object>();
            this.Methods = new Dictionary<string, MethodDefinition>();
            this.factory = CommandFactory.Instance(this);
        }

        /// <summary>
        /// A complete program provided as an array of strings is parsed and executed.
        /// The lines are iterated through, and control flow logic is applied to handle loops, conditionals, and method definitions.
        /// </summary>
        /// <param name="lines">The array of script lines to be executed.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if a method definition is not properly closed with an 'end method' statement.
        /// </exception>
        public void ParseProgram(string[] lines)
        {
            programLines = lines.ToList();
            currentLineIndex = 0;

            while (currentLineIndex < programLines.Count)
            {
                string line = programLines[currentLineIndex].Trim();

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//") || line.StartsWith("*"))
                {
                    currentLineIndex++;
                    continue;
                }

                if (isDefiningMethod)
                {
                    if (line.ToLower().StartsWith("end method"))
                    {
                        Methods[currentMethodName.ToLower()] = currentMethod;
                        isDefiningMethod = false;
                        currentMethodName = "";
                        currentMethod = null;
                    }
                    else
                    {
                        currentMethod.BodyLines.Add(line);
                    }
                    currentLineIndex++;
                }
                else if (line.ToLower().StartsWith("method"))
                {
                    ParseMethodDefinition(line);
                    currentLineIndex++;
                }
                else if (line.ToLower().StartsWith("for"))
                {
                    HandleFor(line);
                }
                else if (line.ToLower().StartsWith("while"))
                {
                    HandleWhile(line);
                }
                else if (line.ToLower().StartsWith("if"))
                {
                    HandleIf(line);
                }
                else if (line.ToLower().StartsWith("else"))
                {
                    HandleElse();
                }
                else if (line.ToLower().StartsWith("end for"))
                {
                    HandleEndFor();
                }
                else if (line.ToLower().StartsWith("end while"))
                {
                    HandleEndWhile();
                }
                else if (line.ToLower().StartsWith("end if"))
                {
                    HandleEndIf();
                }
                else
                {
                    ExecuteLine(line, globalVariables);
                    currentLineIndex++;
                }
            }

            if (isDefiningMethod)
            {
                throw new BOOSEException($"Method '{currentMethodName}' is missing 'end method'");
            }
        }

        /// <summary>
        /// A 'for' loop initialization line is processed.
        /// The loop variable is initialized, and a ForControl object is pushed onto the control stack to track loop state.
        /// </summary>
        /// <param name="line">The line containing the 'for' statement.</param>
        /// <exception cref="BOOSEException">Thrown if the syntax of the 'for' loop is invalid.</exception>
        private void HandleFor(string line)
        {
            // Parse "for i = 1 to 10 step 1"
            string[] parts = line.Split(new[] { '=' }, 2);
            if (parts.Length < 2)
                throw new BOOSEException("Invalid for syntax");

            string varPart = parts[0].Trim();
            string[] varParts = varPart.Split(' ');
            string varName = varParts[varParts.Length - 1];

            string rangePart = parts[1].Trim();
            // Split by " to " and " step " to get start, end, step
            string[] rangeTokens = rangePart.Split(new[] { " to ", " step " }, StringSplitOptions.None);

            int start = CommandHelper.EvaluateInt(rangeTokens[0], globalVariables);
            int end = CommandHelper.EvaluateInt(rangeTokens[1], globalVariables);
            int step = 1;

            if (rangeTokens.Length > 2)
            {
                step = CommandHelper.EvaluateInt(rangeTokens[2], globalVariables);
            }

            // Initialize the loop variable
            globalVariables[varName] = start;

            var forControl = new ForControl
            {
                Variable = varName,
                Start = start,
                End = end,
                Step = step,
                StartLine = currentLineIndex
            };

            controlStack.Push(forControl);
            currentLineIndex++;
        }

        /// <summary>
        /// The end of a 'for' loop is processed.
        /// The loop variable is updated, and the condition is checked to determine whether to repeat the loop or exit.
        /// </summary>
        /// <exception cref="BOOSEException">Thrown if an 'end for' is encountered without a matching 'for'.</exception>
        private void HandleEndFor()
        {
            if (controlStack.Count == 0 || !(controlStack.Peek() is ForControl))
                throw new BOOSEException("End for without matching for");

            var forControl = (ForControl)controlStack.Peek();

            // Get current value from global variables to ensure we are using the updated value
            int currentValue = Convert.ToInt32(globalVariables[forControl.Variable]);

            // Increment/Decrement
            currentValue += forControl.Step;

            // Check loop condition
            if ((forControl.Step > 0 && currentValue <= forControl.End) ||
                (forControl.Step < 0 && currentValue >= forControl.End))
            {
                // Update variable for next iteration
                globalVariables[forControl.Variable] = currentValue;

                // IMPORTANT: Jump back to the line AFTER the 'for' statement
                currentLineIndex = forControl.StartLine + 1;
            }
            else
            {
                // Loop finished
                controlStack.Pop();
                currentLineIndex++;
            }
        }

        /// <summary>
        /// A 'while' loop start statement is processed.
        /// The condition is evaluated; if true, execution proceeds, otherwise the loop body is skipped.
        /// </summary>
        /// <param name="line">The line containing the 'while' statement.</param>
        private void HandleWhile(string line)
        {
            string condition = line.Substring(5).Trim();
            var evaluator = new ExpressionEvaluator();
            bool result = Convert.ToBoolean(evaluator.Evaluate(condition, globalVariables));

            if (result)
            {
                var whileControl = new WhileControl
                {
                    Condition = condition,
                    StartLine = currentLineIndex
                };
                controlStack.Push(whileControl);
                currentLineIndex++;
            }
            else
            {
                int depth = 1;
                currentLineIndex++;
                while (currentLineIndex < programLines.Count && depth > 0)
                {
                    string nextLine = programLines[currentLineIndex].Trim().ToLower();
                    if (nextLine.StartsWith("while"))
                        depth++;
                    else if (nextLine.StartsWith("end while"))
                        depth--;
                    currentLineIndex++;
                }
            }
        }

        /// <summary>
        /// The end of a 'while' loop is processed.
        /// The loop condition is re-evaluated to determine whether to jump back to the start or continue execution.
        /// </summary>
        /// <exception cref="BOOSEException">Thrown if an 'end while' is encountered without a matching 'while'.</exception>
        private void HandleEndWhile()
        {
            if (controlStack.Count == 0 || !(controlStack.Peek() is WhileControl))
                throw new BOOSEException("End while without matching while");

            var whileControl = (WhileControl)controlStack.Peek();
            var evaluator = new ExpressionEvaluator();
            bool result = Convert.ToBoolean(evaluator.Evaluate(whileControl.Condition, globalVariables));

            if (result)
            {
                currentLineIndex = whileControl.StartLine;
            }
            else
            {
                controlStack.Pop();
                currentLineIndex++;
            }
        }

        /// <summary>
        /// An 'if' statement is processed.
        /// The condition is evaluated, and the execution path is determined based on the boolean result.
        /// Nested 'if' blocks are accounted for when skipping code segments.
        /// </summary>
        /// <param name="line">The line containing the 'if' statement.</param>
        private void HandleIf(string line)
        {
            string condition = line.Substring(2).Trim();
            var evaluator = new ExpressionEvaluator();
            bool result = Convert.ToBoolean(evaluator.Evaluate(condition, globalVariables));

            var ifControl = new IfControl
            {
                Condition = condition,
                StartLine = currentLineIndex,
                ExecutedBranch = result
            };
            controlStack.Push(ifControl);

            if (!result)
            {
                int depth = 1;
                currentLineIndex++;
                while (currentLineIndex < programLines.Count && depth > 0)
                {
                    string nextLine = programLines[currentLineIndex].Trim().ToLower();
                    if (nextLine.StartsWith("if") && !nextLine.StartsWith("if "))
                    {
                        // Basic check for nested if, ideally requires stricter parsing
                        if (nextLine.Length > 2 && nextLine[2] == ' ') depth++;
                    }
                    else if (nextLine == "if") // exact match
                    {
                        depth++;
                    }
                    else if (nextLine == "else" && depth == 1)
                    {
                        break;
                    }
                    else if (nextLine.StartsWith("end if"))
                    {
                        depth--;
                        if (depth == 0)
                            break;
                    }
                    currentLineIndex++;
                }
            }
            else
            {
                currentLineIndex++;
            }
        }

        /// <summary>
        /// An 'else' statement is processed.
        /// If the preceding 'if' block was executed, the 'else' block is skipped; otherwise, it is entered.
        /// </summary>
        /// <exception cref="BOOSEException">Thrown if an 'else' is encountered without a matching 'if'.</exception>
        private void HandleElse()
        {
            if (controlStack.Count == 0 || !(controlStack.Peek() is IfControl))
                throw new BOOSEException("Else without matching if");

            var ifControl = (IfControl)controlStack.Peek();

            if (ifControl.ExecutedBranch)
            {
                int depth = 1;
                currentLineIndex++;
                while (currentLineIndex < programLines.Count && depth > 0)
                {
                    string nextLine = programLines[currentLineIndex].Trim().ToLower();
                    if (nextLine.StartsWith("if") && !nextLine.StartsWith("if "))
                    {
                        if (nextLine.Length > 2 && nextLine[2] == ' ') depth++;
                    }
                    else if (nextLine == "if")
                    {
                        depth++;
                    }
                    else if (nextLine.StartsWith("end if"))
                    {
                        depth--;
                        if (depth == 0)
                        {
                            // Do not decrement here, point to End If line
                            break;
                        }
                    }
                    currentLineIndex++;
                }
            }
            else
            {
                currentLineIndex++;
            }
        }

        /// <summary>
        /// The end of an 'if' block is processed.
        /// The control flow stack is popped to return to the previous execution context.
        /// </summary>
        /// <exception cref="BOOSEException">Thrown if an 'end if' is encountered without a matching 'if'.</exception>
        private void HandleEndIf()
        {
            if (controlStack.Count == 0 || !(controlStack.Peek() is IfControl))
                throw new BOOSEException("End if without matching if");

            controlStack.Pop();
            currentLineIndex++;
        }

        /// <summary>
        /// A method definition line is parsed.
        /// The method name and parameters are extracted, and the parser enters method definition mode.
        /// </summary>
        /// <param name="line">The line containing the method signature.</param>
        private void ParseMethodDefinition(string line)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string methodName = parts[2];

            currentMethod = new MethodDefinition();
            currentMethodName = methodName;
            isDefiningMethod = true;

            if (parts.Length > 3)
            {
                string paramString = string.Join(" ", parts.Skip(3));
                ParseMethodParameters(paramString, currentMethod);
            }
        }

        /// <summary>
        /// The parameters of a method definition are parsed and stored.
        /// </summary>
        /// <param name="paramString">The string containing parameter definitions.</param>
        /// <param name="method">The MethodDefinition object to be populated.</param>
        private void ParseMethodParameters(string paramString, MethodDefinition method)
        {
            paramString = paramString.Trim();
            if (string.IsNullOrEmpty(paramString)) return;

            string[] paramParts;
            if (paramString.Contains(","))
            {
                paramParts = paramString.Split(',');
            }
            else
            {
                // Handle parameters without commas (e.g. "int a int b")
                var words = paramString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var tempParams = new List<string>();
                for (int i = 0; i < words.Length; i += 2)
                {
                    if (i + 1 < words.Length)
                        tempParams.Add(words[i] + " " + words[i + 1]);
                }
                paramParts = tempParams.ToArray();
            }

            foreach (string paramPart in paramParts)
            {
                string trimmed = paramPart.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    string[] typeAndName = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (typeAndName.Length >= 2)
                        method.Parameters.Add(typeAndName[typeAndName.Length - 1]);
                }
            }
        }

        /// <summary>
        /// A single line of code is executed.
        /// The command type is identified, arguments are parsed, and the appropriate command object is invoked.
        /// </summary>
        /// <param name="line">The line of code to be executed.</param>
        /// <param name="variables">The dictionary of variables available for the execution context.</param>
        /// <exception cref="BOOSEException">Thrown if an unknown command is encountered.</exception>
        public void ExecuteLine(string line, Dictionary<string, object> variables)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            string[] args = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (args.Length == 0) return;

            string commandName = args[0].ToLower();

            if (args.Length >= 3 && args[1] == "=")
            {
                new VarCommand().Execute(canvas, variables, args);
            }
            else if (factory.HasCommand(commandName))
            {
                factory.GetCommand(commandName).Execute(canvas, variables, args);
            }
            else
            {
                throw new BOOSEException($"Unknown command: '{commandName}'");
            }
        }

        /// <summary>
        /// A block of code representing a method body is executed.
        /// Each line in the block is processed sequentially within the provided local variable scope.
        /// </summary>
        /// <param name="bodyLines">The array of strings representing the method body.</param>
        /// <param name="localVariables">The dictionary of local variables for the method execution.</param>
        public void ExecuteMethodBodyBlock(string[] bodyLines, Dictionary<string, object> localVariables)
        {
            foreach (string line in bodyLines)
            {
                string trimmedLine = line.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedLine))
                    ExecuteLine(trimmedLine, localVariables);
            }
        }
    }

    /// <summary>
    /// The ControlFlow abstract class is defined to serve as a base for all flow control structures.
    /// The starting line index of the control block is stored.
    /// </summary>
    public abstract class ControlFlow { public int StartLine { get; set; } }

    /// <summary>
    /// The ForControl class is implemented to manage the state of 'for' loops.
    /// The loop variable, start, end, and step values are maintained.
    /// </summary>
    public class ForControl : ControlFlow { public string Variable { get; set; } public int Start { get; set; } public int End { get; set; } public int Step { get; set; } }

    /// <summary>
    /// The WhileControl class is implemented to manage the state of 'while' loops.
    /// The loop condition string is stored for re-evaluation.
    /// </summary>
    public class WhileControl : ControlFlow { public string Condition { get; set; } }

    /// <summary>
    /// The IfControl class is implemented to manage the state of 'if-else' blocks.
    /// The condition and the execution status of the branch are tracked.
    /// </summary>
    public class IfControl : ControlFlow { public string Condition { get; set; } public bool ExecutedBranch { get; set; } }
}