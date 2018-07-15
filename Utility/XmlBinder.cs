using System;
using System.Xml;

namespace EmuController.Utility
{
	public class XmlBinder
	{

    public static string Eval(object o, string xpath)
    {
      return (string) Eval(o, xpath, typeof(string));
    }

    public static object Eval(object o, string xpath, Type type)
    {
      return Eval(o, xpath, type, null);
    }

    public static object Eval(object o, string xpath, Type type, string format)
    {
      XmlNode parent = (XmlNode) o;
      string val = "";

      XmlNodeList nodes = parent.SelectNodes(xpath);

      foreach (XmlNode node in nodes) 
        val += node.InnerText;

      if (type != null && type != typeof(string)) 
      {
        object val2 = Convert.ChangeType(val, type);

        if (format != null)
          return String.Format(format, val2);

        return val2;
      }

      return val;
    }


//    public static string Eval(object o, string xpath)
//    {
//      return Eval((XmlNode) o, xpath);
//    }
//
//    public static string Eval(object o, string xpath, string format)
//    {
//      return Eval((XmlNode) o, xpath, format);
//    }
//
//    public static string Eval(XmlNode parent, string xpath)
//    {
//      return Eval(parent, xpath, null);
//    }
//
//    public static string Eval(XmlNode parent, string xpath, string format)
//    {
//      string val = "";
//
//      XmlNodeList nodes = parent.SelectNodes(xpath);
//
//      foreach (XmlNode node in nodes)
//        val += node.InnerText;
//
//      if (format != null)
//        return String.Format(format, val);
//
//      return val;
//    }
//
//    public static float EvalSingle(object o, string xpath)
//    {
//      return EvalSingle((XmlNode) o, xpath);
//    }
//
//    public static float EvalSingle(XmlNode parent, string xpath)
//    {
//      float val = 0;
//
//      XmlNodeList nodes = parent.SelectNodes(xpath);
//
//      foreach (XmlNode node in nodes)
//        val += Convert.ToSingle(node.InnerText);
//
//      return val;
//    }
  }
}
