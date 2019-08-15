using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Xml;

namespace XC.Home.Common
{
    public class XMLHelpercs
    {

        public string CreateFolder()
        {
            string fileName = "myXml";
            string folderPath = "C:\\Configurations";
            string filePath = @"C:\\Configurations\" + fileName + ".xml";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                //给文件夹Everyone赋完全控制权限
                DirectorySecurity folderSec = new DirectorySecurity();
                folderSec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                System.IO.Directory.SetAccessControl(folderPath, folderSec);
                CreateFile(filePath);

            }
            else
            {
                CreateFile(filePath);
            }
            return filePath;
        }

        public void CreateFile(string filePath)
        {
            List<Person> list = new List<Person>();
            list.Add(new Person() { Name = "张三", Age = 19, Email = "hl@yahoo.com" });
            list.Add(new Person() { Name = "李四", Age = 29, Email = "xzl@yahoo.com" });
            list.Add(new Person() { Name = "王五", Age = 39, Email = "hhw@yahoo.com" });
            list.Add(new Person() { Name = "赵六", Age = 9, Email = "ys@yahoo.com" });


            //1.创建一个Dom对象
            XmlDocument xDoc = new XmlDocument();
            //2.编写文档定义
            XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xmlDec);

            //3.编写一个根节点
            XmlElement xmlRoot = xDoc.CreateElement("List");
            xDoc.AppendChild(xmlRoot);


            //4.循环创建Person节点
            for (int i = 0; i < list.Count; i++)
            {
                //4.1创建一个Person元素
                XmlElement xmlPerson = xDoc.CreateElement("Person");
                XmlAttribute xmlAttrId = xDoc.CreateAttribute("id");
                xmlAttrId.Value = (i + 1).ToString();
                //将属性增加到Person节点中
                xmlPerson.Attributes.Append(xmlAttrId);

                //4.2在这里向Person节点下增加子节点
                //创建Name
                XmlElement xmlName = xDoc.CreateElement("Name");
                xmlName.InnerText = list[i].Name;
                xmlPerson.AppendChild(xmlName);

                //创建Age
                XmlElement xmlAge = xDoc.CreateElement("Age");
                xmlAge.InnerText = list[i].Age.ToString();
                xmlPerson.AppendChild(xmlAge);

                //创建一个Email节点

                XmlElement xmlEmail = xDoc.CreateElement("Email");
                xmlEmail.InnerText = list[i].Email;
                xmlPerson.AppendChild(xmlEmail);

                //最后把Person加到根节点下
                xmlRoot.AppendChild(xmlPerson);

            }



            //5.将xmlDocument对象写入到文件中
            xDoc.Save(@"C:\Configurations\myXml.xml");
            {
                if (!File.Exists(filePath))
                {
                    using (FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        //给Xml文件EveryOne赋完全控制权限
                        DirectorySecurity fSec = new DirectorySecurity();
                        fSec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                        System.IO.Directory.SetAccessControl(filePath, fSec);
                    }


                }
            }
        }

        public class Person {
            public string Name { get; set; }

            public int Age { get; set; }

            public string Email { get; set; }
        }
    }
}
