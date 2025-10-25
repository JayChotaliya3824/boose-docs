// ExpressionEvaluator.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BOOSEInterpreter
{
    public class ExpressionEvaluator
    {
        private DataTable dt = new DataTable();

        public object Evaluate(string expression, Dictionary<string, object> variables)
        {
            try
            {
                string populatedExpression = PopulateVariables(expression, variables);

                if (populatedExpression.Contains("<"))
                {
                    return EvaluateComparison(populatedExpression, "<");
                }
                if (populatedExpression.Contains(">"))
                {
                    return EvaluateComparison(populatedExpression, ">");
                }
                if (populatedExpression.Contains("=="))
                {
                    return EvaluateComparison(populatedExpression, "==");
                }
                if (populatedExpression.Contains("!="))
                {
                    return EvaluateComparison(populatedExpression, "!=");
                }

                return dt.Compute(populatedExpression, "");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Expression Error '{expression}': {ex.Message}");
                return 0; // Return 0 on error
            }
        }

        private bool EvaluateComparison(string expression, string op)
        {
            string[] parts = expression.Split(new[] { op }, StringSplitOptions.RemoveEmptyEntries);
            var left = Convert.ToDouble(dt.Compute(parts[0].Trim(), ""));
            var right = Convert.ToDouble(dt.Compute(parts[1].Trim(), ""));

            switch (op)
            {
                case "<": return left < right;
                case ">": return left > right;
                case "==": return left == right;
                case "!=": return left != right;
                default: return false;
            }
        }

        private string PopulateVariables(string expression, Dictionary<string, object> variables)
        {
            string populated = expression;
            foreach (var variable in variables)
            {
                string pattern = $@"\b{variable.Key}\b";
                populated = Regex.Replace(populated, pattern, variable.Value.ToString());
            }
            return populated;
        }
    }
}