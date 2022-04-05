using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml;
using UnityEngine;
using TCD.Zones;

namespace TCD.IO
{
    [AssetLoader]
    public class ZoneParamsDeserializer : RawDeserializer
    {
        private PropertyInfo[] properties;

        public override string RawPath => "ZoneParams";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            properties = typeof(ZoneParams).GetProperties();
            XmlNode root = xml.SelectSingleNode("ZoneParams");
            XmlNodeList zoneParamsNodes = root.SelectNodes("ZoneParam");
            foreach (XmlNode zoneParamsNode in zoneParamsNodes)
                DeserializeZoneParams(zoneParamsNode);
        }

        private void DeserializeZoneParams(XmlNode node)
        {
            ZoneParams zoneParams = new ZoneParams();
            zoneParams.name = EvaluateAttribute(node, "Name", true);
            string typeString = EvaluateAttribute(node, "Type") ?? "Generic";
            zoneParams.Type = (ZoneGeneratorType) Enum.Parse(typeof(ZoneGeneratorType), typeString);
            Type type = zoneParams.GetType();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                string stringValue = EvaluateNode(node, propertyName) ?? default;
                if (string.IsNullOrEmpty(stringValue))
                    continue;
                object value = default;
                if (property.PropertyType == typeof(bool))
                    value = bool.Parse(stringValue);
                if (property.PropertyType == typeof(int))
                    value = int.Parse(stringValue);
                if (property.PropertyType == typeof(float))
                    value = float.Parse(stringValue, CultureInfo.InvariantCulture);
                if (property.PropertyType == typeof(string))
                    value = stringValue;
                PropertyInfo instanceProperty = type.GetProperty(propertyName);
                instanceProperty.SetValue(zoneParams, value);
            }
            Assets.Add(zoneParams.name, (IZoneParams) zoneParams);
        }
    }
}
