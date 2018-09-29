using System;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;


namespace Drill
{
    class Rig
    {
        public readonly string Source = "https://oilprice.com";
        public readonly string Uom    = "barrel";
        private DateTime SampleDate;
        private TimeSpan SampleTime;

        public async Task<Decimal> GetPrice()
        {
            SampleDate = DateTime.UtcNow;
            SampleTime = new TimeSpan(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second);
            string price = ParseForPrice(await GetHtmlAsync(Source));
            return System.Convert.ToDecimal(price);
        }

        public DateTime GetSampleDate()
        {
            return SampleDate;
        }

        public TimeSpan GetSampleTime()
        {
            return SampleTime;
        }

        private async Task<HtmlDocument> GetHtmlAsync(string url)
        {
            HttpClient httpClient = new HttpClient();
            HtmlDocument htmlDoc = new HtmlDocument();

            var html = await httpClient.GetStringAsync(url);
            htmlDoc.LoadHtml(html);

            return htmlDoc;
        }


        private string ParseForPrice(HtmlDocument htmlDoc)
        {
            string price = "";

            var htmlBody = htmlDoc.DocumentNode.Descendants("td")
                .Where(p => p.GetAttributeValue("class", "").Contains("blend_name"));

            foreach (var node in htmlBody)
            {
                if (node.InnerText.Contains("WTI"))
                {
                    var priceNodes = node.ParentNode.Descendants().
                        Where(td => td.GetAttributeValue("class", "") == "value");
                    
                    foreach(var p in priceNodes)
                    {
                        price = p.InnerText;
                    }
                }
            }

            return price;
        }
    }
}

