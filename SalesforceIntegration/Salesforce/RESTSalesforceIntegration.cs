using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce
{
    public class RESTSalesforceIntegration
    {
        // https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/quickstart_oauth.htm

        private static string _loginUrl = "https://bofi--devsfr.cs63.my.salesforce.com/services/oauth2/token";
        private static string _accessToken;

        private string _userName = "";
        private string _password = "";
        private string _clientId = "";
        private string _clientSecret = "";
        private string _redirectUri = "";

        public async void Login()
        {
            // https://stackoverflow.com/questions/27376133/c-httpclient-with-post-parameters/37443799
            //Dictionary<string, string> parameters = new Dictionary<string, string> {
            //    { "grant_type", "password" },
            //    { "client_id", _clientId },
            //    { "client_secret", _clientSecret },
            //    { "username", _userName },
            //    { "password", _password },
            //    { "redirect_uri", _redirectUri }
            //};
            //FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(parameters);
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            try
            {
                //HttpClient httpclient = new HttpClient();
                //httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                //HttpResponseMessage response = await httpclient.PostAsync(_loginUrl, encodedContent).ConfigureAwait(false);
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    // Do something with response. Example get content:
                //    // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
                //    Console.WriteLine(response.Content.ToString());
                //}

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_loginUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 15000;
                request.KeepAlive = false;

                string body = string.Format("grant_type: {0}\r\nclient_id: {1}\r\nclient_secret: {2}\r\nusername: {3}\r\npassword: {4}\r\nredirect_uri: {5}\r\n\r\n",
                    "authorization_code", _clientId, _clientSecret, _userName, _password, _redirectUri);

                byte[] bytes = Encoding.UTF8.GetBytes(body); //await encodedContent.ReadAsByteArrayAsync();
                request.ContentLength = bytes.Length;
                System.IO.Stream os = request.GetRequestStream();
                os.Write(bytes, 0, bytes.Length); //Push it out there
                os.Close();

                try
                {
                    var response = (HttpWebResponse)request.GetResponse();
                    string content = string.Empty;
                    using (var stream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            content = sr.ReadToEnd();
                        }
                    }
                    Console.WriteLine(content);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool CallRest()
        {
            return true;
        }
    }
}
