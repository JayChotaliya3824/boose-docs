// CommandHelper.cs
using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public static class CommandHelper
    {
        private static ExpressionEvaluator evaluator = new ExpressionEvaluator();

        public static int EvaluateInt(string arg, Dictionary<string, object> variables)
        {
            object result = evaluator.Evaluate(arg, variables);
            return Convert.ToInt32(result);
        }
    }
}