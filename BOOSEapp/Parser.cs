// Parser.cs
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BOOSEInterpreter
{
    public class Parser
    {
        private DrawingCanvas canvas;
        private CommandFactory factory;
        private ExpressionEvaluator evaluator = new ExpressionEvaluator(); // Fixes CS0103
        public Dictionary<string, MethodDefinition> Methods { get; private set; }

        public Parser(DrawingCanvas canvas)
        {
            this.canvas = canvas;
            this.Methods = new Dictionary<string, MethodDefinition>();
            this.factory = CommandFactory.Instance(this);
        }

        public void ParseProgram(string[] programLines)
        {
            var variables = new Dictionary<string, object>();
            Methods.Clear();
            // Calls the new 4-argument ExecuteBlock
            ExecuteBlock(programLines, 0, programLines.Length - 1, variables);
        }

        // This is the 4-argument ExecuteBlock that fixes CS1501
        private void ExecuteBlock(string[] lines, int start, int end, Dictionary<string, object> variables)
        {
            for (int i = start; i <= end; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = line.Split(' ');
                string command = parts[0].ToLower();

                try
                {
                    if (command == "method")
                    {
                        // --- START OF FIX ---
                        // 'line' is "method draw(p)"
                        // 'parts' is ["method", "draw(p)"]
                        if (parts.Length < 2)
                        {
                            throw new ArgumentException("Missing method signature. Expected 'method name(params)'.");
                        }

                        // parts[1] is "draw(p)"
                        string signature = parts[1];

                        int parenIndex = signature.IndexOf('(');
                        if (parenIndex == -1 || !signature.EndsWith(")"))
                        {
                            throw new ArgumentException($"Invalid method signature: {signature}. Missing '()'.");
                        }

                        // Get "draw" from "draw(p)"
                        string methodName = signature.Substring(0, parenIndex).Trim();

                        // Pass "draw(p)" to the helper, which can parse it
                        string[] paramNames = ParseParameters(signature);
                        // --- END OF FIX ---

                        int endBlock = FindEndBlock(lines, i, "endmethod");

                        MethodDefinition method = new MethodDefinition();
                        method.Parameters.AddRange(paramNames);
                        for (int j = i + 1; j < endBlock; j++)
                        {
                            method.BodyLines.Add(lines[j]);
                        }
                        Methods[methodName] = method; // Now correctly saves "draw"
                        i = endBlock;
                    }
                    else if (command == "while")
                    {
                        string condition = string.Join(" ", parts, 1, parts.Length - 1);
                        int endBlock = FindEndBlock(lines, i, "endwhile");

                        while (EvaluateCondition(condition, variables)) // Uses new helper
                        {
                            ExecuteBlock(lines, i + 1, endBlock - 1, variables);
                        }
                        i = endBlock;
                    }
                    else if (command == "if")
                    {
                        string condition = string.Join(" ", parts, 1, parts.Length - 1);
                        int endBlock = FindEndBlock(lines, i, "endif");
                        int elseBlock = FindElse(lines, i, endBlock);

                        if (EvaluateCondition(condition, variables))
                        {
                            ExecuteBlock(lines, i + 1, elseBlock - 1, variables);
                        }
                        else
                        {
                            ExecuteBlock(lines, elseBlock + 1, endBlock - 1, variables);
                        }
                        i = endBlock;
                    }
                    else if (command == "for")
                    {
                        string varName = parts[1];
                        object startValue = evaluator.Evaluate(parts[3], variables);
                        object endValue = evaluator.Evaluate(parts[5], variables);
                        int endBlock = FindEndBlock(lines, i, "endfor");

                        for (int j = Convert.ToInt32(startValue); j <= Convert.ToInt32(endValue); j++)
                        {
                            variables[varName] = j;
                            ExecuteBlock(lines, i + 1, endBlock - 1, variables);
                        }
                        i = endBlock;
                    }
                    else
                    {
                        ICommand cmd;
                        if (factory.HasCommand(command))
                        {
                            cmd = factory.GetCommand(command);
                        }
                        else if (parts.Length > 1 && parts[1] == "=")
                        {
                            cmd = factory.GetCommand("var");
                        }
                        else
                        {
                            throw new ArgumentException($"Unknown command: {command}");
                        }
                        cmd.Execute(canvas, variables, parts);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error on line {i + 1} ('{line}'): {ex.Message}");
                }
            }
        }

        // --- HELPER METHODS ---

        // New helper that calls the evaluator
        private bool EvaluateCondition(string expression, Dictionary<string, object> variables)
        {
            object result = evaluator.Evaluate(expression, variables);
            return Convert.ToBoolean(result);
        }

        private string[] ParseParameters(string paramString)
        {
            var match = Regex.Match(paramString, @"\((.*?)\)");
            if (match.Success && !string.IsNullOrEmpty(match.Groups[1].Value))
            {
                return match.Groups[1].Value.Split(',');
            }
            return new string[0];
        }

        // New, smarter FindEndBlock
        private int FindEndBlock(string[] lines, int startIndex, string endToken)
        {
            int depth = 0;
            for (int i = startIndex + 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim().ToLower();
                if (line.StartsWith("if") || line.StartsWith("while") || line.StartsWith("for") || line.StartsWith("method"))
                {
                    depth++;
                }
                else if (line == endToken)
                {
                    if (depth == 0)
                        return i;
                    depth--;
                }
            }
            throw new Exception($"Missing '{endToken}' for block at line {startIndex + 1}");
        }

        // New FindElse
        private int FindElse(string[] lines, int startIndex, int endIfIndex)
        {
            int depth = 0;
            for (int i = startIndex + 1; i < endIfIndex; i++)
            {
                string line = lines[i].Trim().ToLower();
                if (line.StartsWith("if") || line.StartsWith("while") || line.StartsWith("for"))
                {
                    depth++;
                }
                else if (line == "else" && depth == 0)
                {
                    return i;
                }
                else if (line.StartsWith("endif") || line.StartsWith("endwhile") || line.StartsWith("endfor"))
                {
                    if (depth > 0)
                        depth--;
                }
            }
            return endIfIndex;
        }
    }
}