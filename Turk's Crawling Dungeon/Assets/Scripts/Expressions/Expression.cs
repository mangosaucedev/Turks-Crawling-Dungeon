using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TCD.Expressions
{
    public class Expression : IReturnsBool
    {
        private const string PATTERN = @"(?<group>\(\s*((?<bool>((?<target>[a-zA-Z0-9_]+)\.)?(?<var>[a-zA-Z0-9_]+)\s*(?<operator>==|!=|>|<|>=|<=)\s*((?<type>([a-zA-Z]+)):)?(?<equals>[a-zA-Z0-9_\-{}]+)(\s*(?<bool_comparison>&&|\|\|)\s*)?)+\s*\)(\s*(?<group_comparison>&&|\|\|)\s*)?))";

        private static Regex regex = new Regex(PATTERN, RegexOptions.IgnoreCase);

        public bool isValid;
        public List<Group> groups = new List<Group>();

        private bool result;
        private string str;

        public bool Result => result;

        public Expression(string str)
        {
            this.str = str;
            ParseExpression();
        }

        public static implicit operator bool(Expression expression) => expression.isValid && expression.Result;

        private void ParseExpression()
        {
            if (!regex.IsMatch(str))
                return;

            MatchCollection matchCollection = regex.Matches(str);

            for (int i = 0; i < matchCollection.Count; i++)
            {
                Match match = matchCollection[i];
                Group group = new Group(match);
                groups.Add(group);
            }
            result = CompareGroups();
            isValid = true; 
        }

        private bool CompareGroups()
        {
            Group previousGroup = null;
            bool currentValue = true;
            for (int i = 0; i < groups.Count; i++)
            {
                Group group = groups[i];

                if (previousGroup != null)
                {
                    if (previousGroup.Comparison == Comparison.And)
                        currentValue = previousGroup && group;
                    else
                        currentValue = previousGroup || group;
                }
                else
                    currentValue = group;

                if (!group.CompareNext)
                    return currentValue;

                previousGroup = group;
            }
            return currentValue;
        }
    }
}
