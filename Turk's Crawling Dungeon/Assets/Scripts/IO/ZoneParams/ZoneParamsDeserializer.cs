using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace TCD.IO
{
    public class ZoneParamsDeserializer : RawDeserializer
    {
        public override string RawPath => "ZoneParams";

        protected override void DeserializeXmlDocument(XmlDocument xml)
        {
            throw new System.NotImplementedException();
        }
    }
}
