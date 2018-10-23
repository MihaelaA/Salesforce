using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Salesforce
{
    public class SalesforceIntegration
    {
        public static SalesforceProxy.SoapClient Client;

        private static SalesforceProxy.SoapClient _loginClient;
        private static SalesforceProxy.SessionHeader _sessionHeader;
        private static EndpointAddress _endpoint;

        // For REST calls
        private static string _sessionId;

        private string _userName;
        private string _password;

        public SalesforceIntegration(string user, string password, string token)
        {
            _userName = user;
            _password = password + token;
        }

        public void Login()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _loginClient = new SalesforceProxy.SoapClient();
            SalesforceProxy.LoginResult lr = _loginClient.login(null, _userName, _password);
            _endpoint = new EndpointAddress(lr.serverUrl);
            _sessionId = lr.sessionId;
            _sessionHeader = new SalesforceProxy.SessionHeader { sessionId = _sessionId };

            var myBinding = new BasicHttpsBinding();
            myBinding.MaxReceivedMessageSize = 500 * 1024 * 1024;
            myBinding.MaxBufferPoolSize = 500 * 1024 * 1024;
            myBinding.MaxBufferSize = 500 * 1024 * 1024;
            myBinding.Name = "Soap";

            Client = new SalesforceProxy.SoapClient(myBinding,//"Soap", 
                _endpoint);
        }

        public void Logout()
        {
            Client.logout(_sessionHeader);
        }

        #region SOAP
        public SalesforceProxy.sObject[] Query(string soqlQuery)
        {
            List<SalesforceProxy.sObject> result = new List<SalesforceProxy.sObject>();

            SalesforceProxy.QueryResult qr;
            Client.query(_sessionHeader, null, null, null, soqlQuery, out qr);
            if (qr.size > 0)
                result.AddRange(qr.records);

            while (!qr.done)
            {
                Client.queryMore(_sessionHeader, null, qr.queryLocator, out qr);
                if (qr.size > 0)
                    result.AddRange(qr.records);
            }

            return result.ToArray();
        }

        public SalesforceProxy.sObject[] Retrieve(string fields, string objectType, string[] ids)
        {
            SalesforceProxy.sObject[] result;
            Client.retrieve(_sessionHeader, null, null, null, fields, objectType, ids, out result);
            return result;
        }

        public SalesforceProxy.DescribeSObjectResult DescribeSObject(string objectType)
        {
            SalesforceProxy.DescribeSObjectResult result;
            Client.describeSObject(_sessionHeader, null, null, objectType, out result);
            result.fields = result.fields.OrderBy(p => p.name).ToArray();
            return result;
        }

        public SalesforceProxy.DescribeGlobalResult DescribeGlobal()
        {
            SalesforceProxy.DescribeGlobalResult result;
            Client.describeGlobal(_sessionHeader, null, out result);
            result.sobjects = result.sobjects.OrderBy(p => p.name).ToArray();
            return result;
        }

        public SalesforceProxy.SaveResult Create(SalesforceProxy.sObject item)
        {
            SalesforceProxy.sObject[] sObjects = { item };
            SalesforceProxy.LimitInfo[] limitInfoHeader;
            SalesforceProxy.SaveResult[] result;
            Client.create(_sessionHeader, null, null, null, null, null, null, null, null, null, null, null, 
                sObjects, out limitInfoHeader, out result);
            return result[0];
        }

        public SalesforceProxy.SaveResult Update(SalesforceProxy.sObject item)
        {
            //TODO
            //SalesforceProxy.sObject[] sObjects = { item };
            //SalesforceProxy.LimitInfo[] limitInfoHeader;
            //SalesforceProxy.SaveResult[] result;
            //Client.update(_sessionHeader, null, null, null, null, null, null, null, null, null, null, null,
            //    sObjects, out limitInfoHeader, out result);
            //return result[0];
            return null;
        }
        #endregion

        #region REST
        // https://dzone.com/articles/a-few-great-ways-to-consume-restful-apis-in-c
        // https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/quickstart_oauth.htm
        public bool CallRest()
        {
            bool result = false;
            string url = _endpoint.Uri.AbsoluteUri.Substring(0, _endpoint.Uri.AbsoluteUri.Length - _endpoint.Uri.AbsolutePath.Length);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/services/data/v37.0/sobjects/");
            request.Method = "GET";
            request.Headers["Authorization"] = "Bearer " + _sessionId;

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                string content = string.Empty;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                        result = true;
                    }
                }
                Console.WriteLine(content);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public bool GetREST(string urlPath)
        {
            bool result = false;
            string url = _endpoint.Uri.AbsoluteUri.Substring(0, _endpoint.Uri.AbsoluteUri.Length - _endpoint.Uri.AbsolutePath.Length);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + urlPath);
            request.Method = "GET";
            request.Headers["Authorization"] = "Bearer " + _sessionId;

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                string content = string.Empty;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                        result = true;
                    }
                }
                Console.WriteLine(content);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        // https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/dome_update_fields.htm
        public bool UpdateREST(string urlPath, string jsonString)
        {
            bool result = false;
            string url = _endpoint.Uri.AbsoluteUri.Substring(0, _endpoint.Uri.AbsoluteUri.Length - _endpoint.Uri.AbsolutePath.Length);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + urlPath);
            request.Method = "PATCH";
            request.Headers["Authorization"] = "Bearer " + _sessionId;
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                }

                var response = (HttpWebResponse)request.GetResponse();
                string content = string.Empty;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                        result = true;
                    }
                }
                Console.WriteLine(content);
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion
    }
}
