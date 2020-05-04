using System;
using System.IO;
using System.Net;

namespace RESTAPI
{
    public class REST
    {
        public enum QueryTypeEnum
        {
            GET,
            POST,
            PUT,
            PATCH,
            DELETE
        };

        private string endPoint { get; set; }
        private QueryTypeEnum queryMethod;
        public REST(string endPoint, QueryTypeEnum queryMethod)
        {
            this.endPoint = endPoint;
            this.queryMethod = queryMethod;
        }
        public string WriteRequest(string data, string contentType)
        {
            WebRequest webRequest = (WebRequest)WebRequest.Create(endPoint);
            WebResponse webResponse = null;

            webRequest.Method = queryMethod.ToString();
            webRequest.ContentType = contentType;

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            webRequest.ContentLength = byteArray.Length;

            string responseFromServer = string.Empty;

            try
            {
                Stream dataStream = webRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                webResponse = webRequest.GetResponse();

                using (dataStream = webResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                responseFromServer = $"/error/{ex.Message}/";
            }
            finally
            {
                if (webResponse != null)
                {
                    ((IDisposable)webResponse).Dispose();
                }
            }

            webResponse.Close();
            return responseFromServer;
        }
        public string ReadResponse()
        {
            string responseValue = string.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(endPoint);
            httpWebRequest.Method = queryMethod.ToString();
            HttpWebResponse httpWebResponse = null;

            try
            {
                using (httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream responseStreamReader = httpWebResponse.GetResponseStream())
                    {
                        if (responseStreamReader != null)
                        {
                            using (StreamReader streamReader = new StreamReader(responseStreamReader))
                            {
                                responseValue = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseValue = $"/error/{ex.Message}/";
            }
            finally
            {
                if (httpWebResponse != null)
                {
                    ((IDisposable)httpWebResponse).Dispose();
                }
            }
            return responseValue;
        }
    }
}