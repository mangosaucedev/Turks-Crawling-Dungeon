using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TCD.Objects;

namespace TCD.IO
{
    public class PartParser 
    {
        private XmlNode root;
        private PartBlueprint currentPart;

        public PartBlueprint ParseFromNode(XmlNode root)
        { 
            this.root = root;
            currentPart = new PartBlueprint();
            ParsePartNodeAttributes();
            ParsePartVariables();
            return currentPart;
        }

        private void ParsePartNodeAttributes()
        {
            currentPart.name = root.Attributes["Name"]?.InnerText;
        }

        private void ParsePartVariables()
        {
            XmlNodeList variableNodes = root.SelectNodes("Var");
            foreach (XmlNode variableNode in variableNodes)
                ParseVariable(variableNode);
        }
        
        private void ParseVariable(XmlNode variableNode)
        {
            string name = variableNode.Attributes["Name"]?.InnerText;
            object value = variableNode.InnerText;
            currentPart.AddVariable(name, value);
        }
    }
}
