using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The ExpressionEvaluator class is implemented to process and evaluate mathematical and logical expressions.
    /// A DataTable is utilized for numerical computation, and custom logic is applied for string operations and boolean comparisons.
    /// </summary>
    public class ExpressionEvaluator
    {
        /// <summary>
        /// A DataTable instance is maintained to perform arithmetic calculations using its Compute method.
        /// </summary>
        private readonly DataTable _table = new DataTable();

        /// <summary>
        /// An expression string is evaluated within the context of the provided variables.
        /// The expression is analyzed to determine if it involves string concatenation, logical operations, or numerical computation.
        /// Variable references are resolved before the final evaluation is performed.
        /// </summary>
        /// <param name="expression">The expression string to be evaluated.</param>
        /// <param name="variables">The dictionary of variables is accessed to substitute variable names with their values.</param>
        /// <returns>The result of the evaluation is returned as an object.</returns>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the expression is empty or if the evaluation process fails.
        /// </exception>
        public object Evaluate(string expression, Dictionary<string, object> variables)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new BOOSEException("Empty expression provided.");

            try
            {
                string expr = expression.Trim();

                if (expr.Contains("\""))
                {
                    return EvaluateStringConcatenation(expr, variables);
                }

                if (expr.Contains("&&") || expr.Contains("||") || expr.StartsWith("!"))
                {
                    return EvaluateLogicalExpression(expr, variables);
                }

                if (expr.Contains("<=") || expr.Contains(">="))
                {
                    return EvaluateComparison(expr, variables);
                }

                if (expr.Contains("<") || expr.Contains(">") ||
                    expr.Contains("==") || expr.Contains("!="))
                {
                    return EvaluateComparison(expr, variables);
                }

                string populated = PopulateVariables(expr, variables);
                object result = _table.Compute(populated, "");
                return result;
            }
            catch (Exception ex)
            {
                throw new BOOSEException($"Expression evaluation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Logical expressions involving AND, OR, and NOT operators are evaluated.
        /// The expression is parsed recursively to handle complex conditions.
        /// </summary>
        /// <param name="expression">The logical expression string is processed.</param>
        /// <param name="variables">The variable dictionary is referenced for value substitution.</param>
        /// <returns>The boolean result of the logical operation is returned.</returns>
        private object EvaluateLogicalExpression(string expression, Dictionary<string, object> variables)
        {
            string expr = expression.Replace(" ", "");

            if (expr.StartsWith("!"))
            {
                string inner = expr.Substring(1);
                object value = Evaluate(inner, variables);
                return !Convert.ToBoolean(value);
            }

            int andIndex = expr.IndexOf("&&");
            if (andIndex > 0)
            {
                string left = expr.Substring(0, andIndex);
                string right = expr.Substring(andIndex + 2);
                bool leftVal = Convert.ToBoolean(Evaluate(left, variables));
                bool rightVal = Convert.ToBoolean(Evaluate(right, variables));
                return leftVal && rightVal;
            }

            int orIndex = expr.IndexOf("||");
            if (orIndex > 0)
            {
                string left = expr.Substring(0, orIndex);
                string right = expr.Substring(orIndex + 2);
                bool leftVal = Convert.ToBoolean(Evaluate(left, variables));
                bool rightVal = Convert.ToBoolean(Evaluate(right, variables));
                return leftVal || rightVal;
            }

            return Evaluate(expression, variables);
        }

        /// <summary>
        /// Expressions involving string concatenation are processed.
        /// The input is split into parts, variables are resolved, and the components are joined to form the final string or calculated value.
        /// </summary>
        /// <param name="expression">The concatenation expression is analyzed.</param>
        /// <param name="variables">The variable dictionary is used to retrieve values for non-literal parts.</param>
        /// <returns>The concatenated string or summed numerical value is returned.</returns>
        private object EvaluateStringConcatenation(string expression, Dictionary<string, object> variables)
        {
            var parts = SplitByPlusPreservingQuotes(expression);
            var evaluatedParts = new List<object>();

            foreach (string part in parts)
            {
                string trimmed = part.Trim();
                if (trimmed.Length == 0) continue;

                if (trimmed.StartsWith("\"") && trimmed.EndsWith("\""))
                {
                    evaluatedParts.Add(trimmed.Trim('\"'));
                }
                else
                {
                    try
                    {
                        string populated = PopulateVariables(trimmed, variables);
                        object value = _table.Compute(populated, "");
                        evaluatedParts.Add(value);
                    }
                    catch
                    {
                        evaluatedParts.Add(trimmed);
                    }
                }
            }

            if (evaluatedParts.TrueForAll(p => p is double || p is int || p is decimal))
            {
                double total = 0;
                foreach (object part in evaluatedParts)
                {
                    total += Convert.ToDouble(part);
                }
                return total;
            }

            string result = "";
            foreach (object part in evaluatedParts)
            {
                result += part.ToString();
            }
            return result;
        }

        /// <summary>
        /// Comparison expressions involving relational operators are evaluated.
        /// The operands are isolated, evaluated, and compared to determine the boolean result.
        /// </summary>
        /// <param name="expression">The comparison expression is processed.</param>
        /// <param name="variables">The variable dictionary is accessed for operand evaluation.</param>
        /// <returns>The boolean result of the comparison is returned.</returns>
        private object EvaluateComparison(string expression, Dictionary<string, object> variables)
        {
            string expr = PopulateVariables(expression, variables).Replace(" ", "");

            if (expr.Contains("<="))
            {
                string[] parts = expr.Split(new[] { "<=" }, StringSplitOptions.None);
                double left = Convert.ToDouble(_table.Compute(parts[0], ""));
                double right = Convert.ToDouble(_table.Compute(parts[1], ""));
                return left <= right;
            }
            if (expr.Contains(">="))
            {
                string[] parts = expr.Split(new[] { ">=" }, StringSplitOptions.None);
                double left = Convert.ToDouble(_table.Compute(parts[0], ""));
                double right = Convert.ToDouble(_table.Compute(parts[1], ""));
                return left >= right;
            }
            if (expr.Contains("=="))
            {
                string[] parts = expr.Split(new[] { "==" }, StringSplitOptions.None);
                var left = _table.Compute(parts[0], "");
                var right = _table.Compute(parts[1], "");
                return Equals(left, right);
            }
            if (expr.Contains("!="))
            {
                string[] parts = expr.Split(new[] { "!=" }, StringSplitOptions.None);
                var left = _table.Compute(parts[0], "");
                var right = _table.Compute(parts[1], "");
                return !Equals(left, right);
            }
            if (expr.Contains("<"))
            {
                string[] parts = expr.Split('<');
                double left = Convert.ToDouble(_table.Compute(parts[0], ""));
                double right = Convert.ToDouble(_table.Compute(parts[1], ""));
                return left < right;
            }
            if (expr.Contains(">"))
            {
                string[] parts = expr.Split('>');
                double left = Convert.ToDouble(_table.Compute(parts[0], ""));
                double right = Convert.ToDouble(_table.Compute(parts[1], ""));
                return left > right;
            }

            throw new BOOSEException($"Unsupported comparison operator in: {expression}");
        }

        /// <summary>
        /// The input string is split by the plus operator while respecting quoted string literals.
        /// This method ensures that plus signs inside quotes are treated as characters rather than operators.
        /// </summary>
        /// <param name="input">The input string containing concatenation operations is provided.</param>
        /// <returns>An array of string parts separated by the plus operator is returned.</returns>
        private string[] SplitByPlusPreservingQuotes(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new string[0];

            var list = new List<string>();
            int start = 0;
            bool inQuotes = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (input[i] == '+' && !inQuotes)
                {
                    if (i > start)
                    {
                        list.Add(input.Substring(start, i - start));
                    }
                    start = i + 1;
                }
            }

            if (start < input.Length)
            {
                list.Add(input.Substring(start));
            }

            return list.ToArray();
        }

        /// <summary>
        /// Variable names within an expression are replaced with their current values.
        /// Regular expressions are used to match whole word variable names to prevent partial replacements.
        /// </summary>
        /// <param name="expression">The expression containing variable names is provided.</param>
        /// <param name="variables">The dictionary of variables is accessed for value lookup.</param>
        /// <returns>The expression string with variable values substituted is returned.</returns>
        private string PopulateVariables(string expression, Dictionary<string, object> variables)
        {
            if (variables == null || variables.Count == 0)
                return expression;

            string result = expression;
            foreach (var kvp in variables)
            {
                string escapedName = Regex.Escape(kvp.Key);
                string pattern = $@"\b{escapedName}\b";
                result = Regex.Replace(result, pattern, kvp.Value.ToString(), RegexOptions.IgnoreCase);
            }
            return result;
        }
    }
}