using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace HotelMVVM.Persistency
{
    public class PersistencyFacade
    {
        private const string baseURI = "http://localhost:58571/api/";
        

        

        public async Task<List<T>> GetAll<T>(string URI)
        {
            List<T> objektList = new List<T>();
            using (HttpClient client = new HttpClient())
            {
                string stringAsync = await client.GetStringAsync(baseURI + URI);
                objektList = JsonConvert.DeserializeObject<List<T>>(stringAsync);
            }

            return objektList;
        }


        public async Task<T> GetOne<T>(int id, string URI) where T : new()
        {
            T objekt = new T();
            using (HttpClient client = new HttpClient())
            {
                string stringAsync = await client.GetStringAsync(baseURI + URI + "/" + id);
                objekt = JsonConvert.DeserializeObject<T>(stringAsync);
            }

            return objekt;
        }

        public async Task<bool> DeleteOne(int id, string URI)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage deleteAsync = await client.DeleteAsync(baseURI + URI + "/" + id);
                if (deleteAsync.IsSuccessStatusCode)
                {
                    string jsonString = deleteAsync.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonString);
                    return ok;
                }

            }

            return ok;
        }

       



        public async Task<T> Post<T>(T objekt, string URI)
        {
           
            using (HttpClient client = new HttpClient())
            {
                string jsonstring = JsonConvert.SerializeObject(objekt);
                StringContent content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage postAsync = await client.PostAsync(baseURI + URI, content);
                if (postAsync.IsSuccessStatusCode)
                {
                    string jsonString = postAsync.Content.ReadAsStringAsync().Result;
                    JsonConvert.DeserializeObject(jsonString);
                    return objekt;
                }

                return objekt;
            }
        }





        public async Task Put<T>(int id, T objekt, string URI)
        {
            
            using (HttpClient client = new HttpClient())
            {
                string jsonStr = JsonConvert.SerializeObject(objekt);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage putAsync = await client.PutAsync(baseURI + URI + "/" + id, content);
                HttpResponseMessage resp = putAsync;
                if (resp.IsSuccessStatusCode)
                {
                    string jsonResStr = resp.Content.ReadAsStringAsync().Result;
                    JsonConvert.DeserializeObject(jsonResStr);
                }
            }
            
        }


    }
}
