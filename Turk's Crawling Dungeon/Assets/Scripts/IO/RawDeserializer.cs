using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using UnityEngine;

namespace TCD.IO
{
    public abstract class RawDeserializer : IAssetLoader
    {
        protected string streamingAssetsRaws = Application.streamingAssetsPath + "/Raws/";

        private int filesToDeserialize;
        private int filesDeserialized;

        public float Progress
        {
            get
            {
                if (filesToDeserialize == 0)
                    return 1f;
                return (float) filesDeserialized / filesToDeserialize;
            }
        }

        public abstract string RawPath { get; }

        public IEnumerator LoadAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<XmlDocument> xmlDocuments = GetXmlDocumentsAtPath();
            filesToDeserialize = xmlDocuments.Count;
            yield return DeserializeAllXmlDocumentsInList(xmlDocuments);

            stopwatch.Stop();
            DebugLogger.Log(
                $"\tDeserialized {filesDeserialized}/{filesToDeserialize} raw xml documents in folder " +
                $"/{RawPath}/ in {stopwatch.ElapsedMilliseconds} ms.");
        }

        private List<XmlDocument> GetXmlDocumentsAtPath()
        {
            List<XmlDocument> xmlDocuments = new List<XmlDocument>();
            string path = streamingAssetsRaws + RawPath; 

            if (!Directory.Exists(path))
                return xmlDocuments;

            string[] filePaths = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(filePath);
                if (xml != null)
                    xmlDocuments.Add(xml);
            }

            return xmlDocuments;
        }

        private IEnumerator DeserializeAllXmlDocumentsInList(List<XmlDocument> xmlDocuments)
        {
            foreach (XmlDocument xml in xmlDocuments)
            {
                bool isSuccessful = true;
                try
                {
                    DeserializeXmlDocument(xml);
                }
                catch (Exception e)
                {
                    isSuccessful = false;
                    ExceptionHandler.HandleMessage("Raw \"" + xml.Name + "\" could not be deserialized: " + e.Message);
                }
                if (isSuccessful)
                {
                    filesDeserialized++;
                    yield return null;
                }
            }
        }

        protected abstract void DeserializeXmlDocument(XmlDocument xml);

        protected virtual string EvaluateNode(XmlNode root, string nodeName, bool isRequired = false)
        {
            try
            {
                XmlNode node = root.SelectSingleNode(nodeName);
                string value = node?.InnerText;
                if (value != null || !isRequired)
                    return value;
                else if (value == null && isRequired)
                    ExceptionHandler.Handle(new RawException($"Required node '{nodeName}' not found."));
            }
            catch (RawException e)
            {
                ExceptionHandler.Handle(e);
            }
            catch (Exception e) when (!(e is RawException))
            {
                ExceptionHandler.Handle(new RawException(e.Message));
            }
            return null;
        }

        protected virtual string EvaluateAttribute(XmlNode root, string attributeName, bool isRequired = false)
        {
            try
            {
                XmlAttribute attribute = root.Attributes?[attributeName];
                string value = attribute?.InnerText;
                if (value != null || !isRequired)
                    return value;
                else if (value == null && isRequired)
                    ExceptionHandler.Handle(new RawException($"Required attribute '{attributeName}' not found."));
            }
            catch (RawException e)
            {
                ExceptionHandler.Handle(e);
            }
            catch (Exception e) when (!(e is RawException))
            {
                ExceptionHandler.Handle(new RawException(e.Message));
            }
            return null;
        }
    }
}
