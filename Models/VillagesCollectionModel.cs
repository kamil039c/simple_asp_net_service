using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace service_kamil.Models
{
    public enum SortType { Asc, Desc };
    public enum RequestType { Xml, Json };

    [Serializable()]
    [System.Xml.Serialization.XmlRoot("root")]
    public class VillagesCollectionModel
    {
        [XmlArray("villages")]
        [XmlArrayItem("village", typeof(VillageModel))]
        public VillageModel[] Villages { get; set; }

        public static VillagesCollectionModel GetVillages(
            RequestType requestType = RequestType.Xml, 
            string sort = "id", 
            SortType sortType = SortType.Asc, 
            int from = 0, 
            int count = 0 
            )
        {

            if (requestType == RequestType.Json)
            {
                throw new Exception("Json request isn't supported jet");
            }

            string requestString = "xml";
            if (requestType == RequestType.Json)
            {
                requestString = "json";
            }

            string sortString = "asc";
            if (sortType == SortType.Desc)
            {
                sortString = "desc";
            }

            string uri = "xml.php?request=" + requestString + "&sort=" + sort + "&order=" + sortString + "&from=" + from + "&count=" + count;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/" + requestString)
            );

            Console.Out.WriteLine(uri);

            VillagesCollectionModel vcm = null;
            XmlSerializer serializer = new XmlSerializer(typeof(VillagesCollectionModel));

            try
            {
                Task<HttpResponseMessage> responseTask;
                HttpResponseMessage response;

                responseTask = client.GetAsync(uri);
                responseTask.Wait();
                response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<Stream> streamTask;
                    Stream stream;

                    streamTask = response.Content.ReadAsStreamAsync();
                    streamTask.Wait();
                    stream = streamTask.Result;

                    vcm = (VillagesCollectionModel)serializer.Deserialize(stream);
                    stream.Close();
                }
            } catch (Exception e) {
                throw new Exception("Wystąpił błąd podczas wywoływania api: " + e.Message);
            }

            return vcm;
        }
    }
}
