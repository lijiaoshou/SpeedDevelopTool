using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommonLib
{
    public class Config
    {
        #region old

        /// <summary>
        ///连接字符串
        /// </summary>
        //public static string ConnectString { get { return GetValue("conStr"); } }

        ///// <summary>
        ///// 用户名
        ///// </summary>

        //public static string UserName { get { return GetValue("userName"); } }

        ///// <summary>
        ///// 栏目keyid
        ///// </summary>
        //public static string LanMuKey { get { return GetValue("lanmuKey"); } }

        ///// <summary>
        ///// 是否开发模式
        ///// </summary>

        //public static string IsDevelopPattern { get { return GetValue("isDevelopPattern"); } }


        ///// <summary>
        ///// 根据key值从配置文件中取出相应的value
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //private static string GetValue(string key)
        //{
        //    string value = ConfigurationManager.AppSettings[key];
        //    return value;
        //}

        #endregion

        #region 完全配置版本

        /// <summary>
        /// 从配置文件中取出先有的演示功能
        /// </summary>
        /// <returns>以英文分号分隔的类别名称</returns>
        public static string GetCategories()
        {
            StringBuilder sb = new StringBuilder();
            XDocument doc = XDocument.Load("SpeedDevelopTool.xml");

            var nodes = doc.Descendants("CategoriesNode");
            XElement node = nodes.First();
            if (node != null)
            {
                var categories = node.Elements();
                foreach (var item in categories)
                {
                    //循环拿到CategoriesNode下的所有节点（现有演示功能）
                    sb.Append(item.Name+";");
                }
            }

            return sb.ToString().TrimEnd(';');
        }

        /// <summary>
        /// 通过传入的类别名和key拿到指定类别的指定属性值
        /// </summary>
        /// <param name="categoryName">类别名</param>
        /// <param name="key">属性名</param>
        /// <returns></returns>
        public static string GetValueByKey(string categoryName,string key)
        {
            StringBuilder sb = new StringBuilder();
            XDocument doc = XDocument.Load("SpeedDevelopTool.xml");
            string result = "";

            //获取所有CatetoriesNode节点
            var nodes = doc.Descendants("CategoriesNode");
            //拿到第一个CategoriedNode节点
            XElement node = nodes.First();
            if (node != null)
            {
                //拿到第一个匹配分类名称的节点
                var category = node.Elements(categoryName).First();
                //拿到指定节点的指定属性值
                result = category.Attribute(key).Value;
            }
            return result;
        }

        #endregion
    }
}
