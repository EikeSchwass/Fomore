// Eike Stein: Fomore/Core/User.cs (2018/05/11)

using System;
using System.Xml.Serialization;

namespace Core
{
    /// <summary>
    /// This class represents a user of the application and contains additonal information, such as the location of saved creatures.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// The name of the User
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// The path to the folder, where the created creatures are saved to.
        /// </summary>
        [XmlAttribute]
        public string CreatureFolderPath { get; set; }
    }
}