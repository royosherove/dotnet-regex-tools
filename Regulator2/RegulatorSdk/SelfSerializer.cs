using System;
using System.IO;
using System.Xml.Serialization;
using System.Web;

namespace Regulator.SDK.Core
{
	 /// <summary>
    /// Summary description for SelfSerializer.
    /// This class is taken from Matthew Reynolds great article on
    /// CSharpToday: http://www.csharptoday.com/content.asp?id=1763
    /// </summary>
    [Serializable()] public class SelfSerializer
    {
    public SelfSerializer()
    {
    }
    
    // Load - we don't know the class, so we need that...
    public static object Load(Type type, string filename)
    {
        // file...
        FileStream stream = null;
        try
        {
            // open the stream...
            stream = new FileStream(filename, FileMode.Open,FileAccess.Read);
            XmlSerializer serializer = new XmlSerializer(type);
            return serializer.Deserialize(stream);
        }
        finally
        {
            if(stream != null)
                stream.Close();
        }
    }
    
//    // Load - we don't know the class, so we need that...
//    public static object LoadCache(Type type, string filename)
//    {
//        object obj = HttpContext.Current.Cache[filename];
//        if(obj == null)
//        {
//            // file...
//            FileStream stream = null;
//            try
//            {
//                // open the stream...
//                stream = new FileStream(filename, FileMode.Open,FileAccess.Read);
//                XmlSerializer serializer = new XmlSerializer(type);
//                obj =  serializer.Deserialize(stream);
//                if(obj != null)
//                {
//                    CacheDependency cd = new CacheDependency(filename);
//                    HttpContext.Current.Cache.Insert(filename,obj,cd);
//                }
//            }
//            finally
//            {
//                if(stream != null)
//                    stream.Close();
//            }
//        }
//        return obj;
//    }
//    
    
    // Save - save the file to disk...
    public virtual void Save(string filename)
    {
        // file...
        FileStream stream = null;
        try
        {
            // open...
            stream = new FileStream(filename, FileMode.Create);
            Save(stream);
        }
        finally
        {
            if(stream != null)
                stream.Close();
        }
    }
    
    public void Save(Stream stream)
    {
        // serialize it...
        try
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            serializer.Serialize(stream, this);    
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    }
}
