using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI
{
    public static class ButtonInputKeys 
    {
        public static ButtonInputKey[] inputKeys;

        static ButtonInputKeys()
        {
            inputKeys = new ButtonInputKey[]
            {
                new ButtonInputKey("a", KeyCode.A),
                new ButtonInputKey("b", KeyCode.B),
                new ButtonInputKey("c", KeyCode.C),
                new ButtonInputKey("d", KeyCode.D),
                new ButtonInputKey("e", KeyCode.E),
                new ButtonInputKey("f", KeyCode.F),
                new ButtonInputKey("g", KeyCode.G),
                new ButtonInputKey("h", KeyCode.H),
                new ButtonInputKey("i", KeyCode.I),
                new ButtonInputKey("j", KeyCode.J),
                new ButtonInputKey("k", KeyCode.K),
                new ButtonInputKey("l", KeyCode.L),
                new ButtonInputKey("m", KeyCode.M),
                new ButtonInputKey("n", KeyCode.N),
                new ButtonInputKey("o", KeyCode.O),
                new ButtonInputKey("p", KeyCode.P),
                new ButtonInputKey("q", KeyCode.Q),
                new ButtonInputKey("r", KeyCode.R),
                new ButtonInputKey("s", KeyCode.S),
                new ButtonInputKey("t", KeyCode.T),
                new ButtonInputKey("u", KeyCode.U),
                new ButtonInputKey("v", KeyCode.V),
                new ButtonInputKey("w", KeyCode.W),
                new ButtonInputKey("x", KeyCode.X),
                new ButtonInputKey("y", KeyCode.Y),
                new ButtonInputKey("z", KeyCode.Z),
                new ButtonInputKey("A", KeyCode.A, KeyCode.LeftShift),
                new ButtonInputKey("B", KeyCode.B, KeyCode.LeftShift),
                new ButtonInputKey("C", KeyCode.C, KeyCode.LeftShift),
                new ButtonInputKey("D", KeyCode.D, KeyCode.LeftShift),
                new ButtonInputKey("E", KeyCode.E, KeyCode.LeftShift),
                new ButtonInputKey("F", KeyCode.F, KeyCode.LeftShift),
                new ButtonInputKey("G", KeyCode.G, KeyCode.LeftShift),
                new ButtonInputKey("H", KeyCode.H, KeyCode.LeftShift),
                new ButtonInputKey("I", KeyCode.I, KeyCode.LeftShift),
                new ButtonInputKey("J", KeyCode.J, KeyCode.LeftShift),
                new ButtonInputKey("K", KeyCode.K, KeyCode.LeftShift),
                new ButtonInputKey("L", KeyCode.L, KeyCode.LeftShift),
                new ButtonInputKey("M", KeyCode.M, KeyCode.LeftShift),
                new ButtonInputKey("N", KeyCode.N, KeyCode.LeftShift),
                new ButtonInputKey("O", KeyCode.O, KeyCode.LeftShift),
                new ButtonInputKey("P", KeyCode.P, KeyCode.LeftShift),
                new ButtonInputKey("Q", KeyCode.Q, KeyCode.LeftShift),
                new ButtonInputKey("R", KeyCode.R, KeyCode.LeftShift),
                new ButtonInputKey("S", KeyCode.S, KeyCode.LeftShift),
                new ButtonInputKey("T", KeyCode.T, KeyCode.LeftShift),
                new ButtonInputKey("U", KeyCode.U, KeyCode.LeftShift),
                new ButtonInputKey("V", KeyCode.V, KeyCode.LeftShift),
                new ButtonInputKey("W", KeyCode.W, KeyCode.LeftShift),
                new ButtonInputKey("X", KeyCode.X, KeyCode.LeftShift),
                new ButtonInputKey("Y", KeyCode.Y, KeyCode.LeftShift),
                new ButtonInputKey("Z", KeyCode.Z, KeyCode.LeftShift),
            };
        }

        public static bool IsKeyDown(string key)
        {
            ButtonInputKey inputKey = GetInputKey(key);
            if (inputKey != null)
                return IsInputKeyDown(inputKey);
            return false;
        }

        public static ButtonInputKey GetInputKey(int index)
        {
            if (inputKeys.Length > index)
                return inputKeys[index];
            return null;
        }

        public static ButtonInputKey GetInputKey(string key)
        {
            foreach (ButtonInputKey inputKey in inputKeys)
            {
                string currentKey = inputKey.str;
                if (key == currentKey)
                    return inputKey;
            }
            return null;
        }

        private static bool IsInputKeyDown(ButtonInputKey inputKey)
        {
            KeyCode key = inputKey.key;
            KeyCode primaryModifier = inputKey.primaryModifier;
            KeyCode secondaryModifier = inputKey.secondaryModifier;
            bool primaryModifierPressed = primaryModifier == KeyCode.None || Input.GetKey(primaryModifier);
            bool secondaryModifierPressed = secondaryModifier == KeyCode.None || Input.GetKey(secondaryModifier);
            bool modifiersPressed = primaryModifierPressed && secondaryModifierPressed;
            return (Input.GetKeyDown(key) && modifiersPressed);
        }
    }
}
