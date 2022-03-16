using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TCD.Expressions
{
    public class Group : ICompareTo
    {
        public List<Boolean> booleans = new List<Boolean>();

        private bool result;
        private Comparison comparison;
        private Match match;

        public bool Result => result;

        public Comparison Comparison => comparison;

        public bool CompareNext => Comparison > Comparison.None;

        public Group(Match match)
        {
            this.match = match;
            ParseGroup();
        }

        public static implicit operator bool(Group group) => group.Result;

        private void ParseGroup()
        {
            System.Text.RegularExpressions.Group boolGroup = match.Groups["bool"];

            if (boolGroup.Success)
            {
                CaptureCollection captures = boolGroup.Captures;
                for (int i = 0; i < captures.Count; i++)
                {
                    Capture capture = captures[i];
                    Boolean boolean = new Boolean(capture);
                    booleans.Add(boolean);
                }
                result = CompareBooleans();
            }
            else 
                result = true;

            comparison = ParseComparison();
        }

        private Comparison ParseComparison()
        {
            string comparisonString = match.Groups["group_comparison"].Value;
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

        private bool CompareBooleans()
        {
            Boolean previousBoolean = null;
            bool currentValue = true;
            for (int i = 0; i < booleans.Count; i++)
            {
                Boolean boolean = booleans[i];

                if (previousBoolean != null)
                {
                    if (previousBoolean.Comparison == Comparison.And)
                        currentValue = previousBoolean && boolean;
                    else
                        currentValue = previousBoolean || boolean;
                }
                else
                    currentValue = boolean;

                if (!boolean.CompareNext)
                    return currentValue;

                previousBoolean = boolean;
            }
            return currentValue;
        }
    }
}
