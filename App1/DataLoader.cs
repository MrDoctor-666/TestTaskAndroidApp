using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Org.Apache.Http.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace App1
{
    internal class DataLoader
    {
        private List<OfferData> offerDatas;
        private string xpath = "yml_catalog/shop/offers/offer";

        public DataLoader()
        {
            offerDatas = new List<OfferData>();
        }

        public List<string> LoadIDs(string xmlData)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            var nodes = xmlDoc.SelectNodes(xpath);

            foreach (XmlNode childrenNode in nodes)
            {
                string id = childrenNode.Attributes["id"].Value;

                offerDatas.Add(new OfferData(id, childrenNode));
            }

            List<string> ids = new List<string>();
            foreach (OfferData offerData in offerDatas)
                ids.Add(offerData.id);

            return ids;
        }

        public string FindDataByIndex(int index)
        {
            if (offerDatas.Count <= index) return null;

            return offerDatas[index].xmlData.InnerXml;
        }
    }

    internal struct OfferData
    {
        public string id;
        public XmlNode xmlData;

        public OfferData(string id, XmlNode xmlData)
        {
            this.id = id;
            this.xmlData = xmlData;
        }
    }
}