using System;
using System.IO;
using System.Xml.Serialization;
using System.Web;

namespace RegexWizard.Framework
{
    /// <summary>
    /// Summary description for SelfSerializer.
    /// This class is taken from Matthew Reynolds great article on
    /// CSharpToday: http://www.csharptoday.com/content.asp?id=1763
    /// </summary>
    [Serializable()]
    public class SelfSerializer
    {
        public SelfSerializer()
        {
        }

        // Load - we don't know the class, so we need that...
        public static T Load<T>(string filename)
        {
            // file...
            FileStream stream = null;
            try
            {
                // open the stream...
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

       

        // Save - save the file to disk...
        public virtual void Save<T>(string filename)
        {
            // file...
            FileStream stream = null;
            try
            {
                // open...
                stream = new FileStream(filename, FileMode.Create);
                Save<T>(stream);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public void Save<T>(Stream stream)
        {
            // serialize it...
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
                throw;
            }
        }
    }
}
