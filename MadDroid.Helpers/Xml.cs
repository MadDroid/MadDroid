using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace MadDroid.Helpers
{
    /// <summary>
    /// Xml helper class 
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// Get all <see cref="XElement"/> with a specific tag name
        /// </summary>
        /// <param name="stream">The stream of the xml</param>
        /// <param name="tagName">The tag name to get the <see cref="XElement"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="XmlException"/>
        /// <exception cref="InvalidOperationException"/>
        public static IEnumerable<XElement> GetElements(Stream stream, string tagName)
        {
            using (var reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();

                while (reader.Read())
                {
                    while (reader.NodeType == XmlNodeType.Element && reader.Name == tagName)
                    {
                        if (XNode.ReadFrom(reader) is XElement element)
                        {
                            yield return element;
                        }
                    }
                }
            }
        }
    }
}
