using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TCD.Cinematics;
using TCD.Objects;

namespace TCD.Expressions
{
    public class Boolean : ICompareTo
    {
        private const string PATTERN = @"(?<bool>((?<target>[a-zA-Z0-9_]+)\.)?(?<var>[a-zA-Z0-9_]+)\s*(?<operator>==|!=|>|<|>=|<=)\s*((?<type>([a-zA-Z]+)):)?(?<equals>[a-zA-Z0-9_\-{}]+)(\s*(?<bool_comparison>&&|\|\|)\s*)?)";
        private const string INVALID_TARGET = "Boolean could not parse: variable target {0} is invalid!";
        private const string INVALID_TARGET_NOT_PERSISTENT = "Boolean could not parse: variable target {0} is not persistent and " +
            "does not have local variables!";
        private const string INVALID_OPERATOR = "Boolean could not parse: {0} is not a valid operator!";
        private const string INVALID_OPERATOR_FOR_TYPE = "Boolean could not parse: {0} is not a valid operator for variable type {1}!";

        private static Regex regex = new Regex(PATTERN, RegexOptions.IgnoreCase);

        private bool result;
        private Comparison comparison;
        private Match match;
        private string target;
        private string variable;
        private Type type;

        public bool Result => result;

        public Comparison Comparison => comparison;

        public bool CompareNext => Comparison > Comparison.None;

        public Boolean(Capture capture)
        {
            string str = capture.Value;
            match = regex.Match(str);
            ParseBoolean();
        }

        public static implicit operator bool(Boolean boolean) => boolean.Result;

        private void ParseBoolean()
        {
            target = match.Groups["target"].Value;
            variable = match.Groups["var"].Value;
            
            object value = GetVariable();
            Operator op = GetOperator();
            type = ParseType();
            object otherValue = ParseOtherVariable();
            result = CompareValues(value, otherValue, op);

            comparison = ParseComparison();
        }

        private object GetVariable()
        {
            if (!string.IsNullOrEmpty(target))
            {
                if (CinematicTarget.targets.TryGetValue(target, out CinematicTarget cinematicTarget))
                {
                    if (cinematicTarget.TryGetComponent(out BaseObject obj))
                        return obj.localVars.Get(variable);
                    throw new ExpressionParseException(string.Format(INVALID_TARGET_NOT_PERSISTENT, target));
                }
                throw new ExpressionParseException(string.Format(INVALID_TARGET, target));
            }
            return GlobalVars.Get(variable);
        }

        private Operator GetOperator()
        {
            string operatorString = match.Groups["operator"].Value;
            switch (operatorString)
            {
                case "==":
                    return Operator.Equals;
                case "!=":
                    return Operator.NotEquals;
                case ">":
                    return Operator.GreaterThan;
                case "<":
                    return Operator.LessThan;
                case ">=":
                    return Operator.GreaterOrEqual;
                case "<=":
                    return Operator.LessOrEqual;
            }
            throw new ExpressionParseException(string.Format(INVALID_OPERATOR, operatorString));
        }

        private Type ParseType()
        {
            string type = match.Groups["type"].Value;
            switch (type)
            {
                case "bool":
                    return typeof(bool);
                case "float":
                    return typeof(float);
                case "int":
                    return typeof(int);
                default:
                    return typeof(string);
            }
        }

        private object ParseOtherVariable()
        {
            string value = match.Groups["equals"].Value;
            if (type == typeof(bool))
                return bool.Parse(value);
            if (type == typeof(float))
                return float.Parse(value);
            if (type == typeof(int))
                return int.Parse(value);
            return value;
        }

        private bool CompareValues(object a, object b, Operator op)
        {
            if (type == typeof(bool))
                return CompareBool((bool) a,(bool) b, op);
            if (type == typeof(float))
                return CompareFloat((float) a, (float) b, op);
            if (type == typeof(int))
                return CompareFloat((int) a, (int) b, op);
            return CompareString(a.ToString(), b.ToString(), op);
        }

        private bool CompareBool(bool a, bool b, Operator op)
        {
            switch (op)
            {
                case Operator.Equals:
                    return a == b;
                case Operator.NotEquals:
                    return a != b;
                default:
                    throw new ExpressionParseException(string.Format(INVALID_OPERATOR_FOR_TYPE, op, type.Name));
            }
        }

        private bool CompareFloat(float a, float b, Operator op) 
        {
            switch (op)
            {
                case Operator.Equals:
                    return a == b;
                case Operator.NotEquals:
                    return a != b;
                case Operator.GreaterThan:
                    return a > b;
                case Operator.LessThan:
                    return a < b;
                case Operator.GreaterOrEqual:
                    return a >= b;
                default:
                    return a <= b;
            }
        }

        private bool CompareString(string a, string b, Operator op)
        {
            switch (op)
            {
                case Operator.Equals:
                    return a == b;
                case Operator.NotEquals:
                    return a != b;
                default:
                    throw new ExpressionParseException(string.Format(INVALID_OPERATOR_FOR_TYPE, op, type.Name));
            }
        }

        private Comparison ParseComparison()
        {
            string comparisonString = match.Groups["bool_comparison"].Value;
            switch (comparisonString)
            {
                case "&&":
                    return Comparison.And;
                case "||":
                    return Comparison.Or;  
                default:
                    return Comparison.None;
            }
        }
    }
}
