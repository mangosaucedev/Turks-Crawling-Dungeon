using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace TCD.IO
{
    public class ZoneEnvironmentsDeserializer : RawDeserializer
    {
        public override string RawPath => "ZoneEnvironments";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            throw new System.NotImplementedException();
        }
    }
}
