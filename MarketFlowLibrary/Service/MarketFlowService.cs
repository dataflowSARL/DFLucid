using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft;
using System.Threading;
using MKFLibrary;
using MKFLibrary.API;

namespace MarketFlowLibrary
{

	public class MarketFlowService
	{

		//10.10.0.150:38201 //internal service
        //moph.dataflow.com.lb //public service
        //private const string serviceBaseURI = "http://10.10.5.244/MKFAPI/API/Values/"; //"http://staging2.folens.ie/api/";//gloria machine

        private const string serviceBaseURI = "http://moph.dataflow.com.lb/MKFAPI/API/Values/"; //"http://staging2.folens.ie/api/";

        private const string loginAction = "login";
		private const string getPosition = "GetPosition";
        private const string logoutAction = "LogOut";
        private const string updatePasswordAction = "UpdatePassword";
        private const string getAccountSummaryAction = "GetAccountSummary";
        private const string getStatementAction = "GetStatement";
        private const string getOperationsAction = "GetOperations";
        private const string getRealisedProfitAction = "GetRealisedProfit";
        private const string getRiskSummaryAction = "GetRiskSummary";
        private const string getPortfolioSummary = "GetPortfolioSummary";


        private static bool TestMode { get; set; }

		private static string ServiceURL { get; set;}

		public static void SetupTestMode(bool testMode=false, string testURL="")
		{
			TestMode = testMode;
			if (TestMode) {
				ServiceURL = testURL;
			} else {
				ServiceURL = serviceBaseURI;
			}

			if (ServiceURL.Last () != '/') {

				ServiceURL += "/";
			}
		}

        public async static Task<LoginResult> Login(MKFUser user)
        {
            LoginResult result = new LoginResult();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, loginAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
            }

            return result;
        }

        public async static Task<API_Response<Position>> GetPosition(MKFUser user)
        {
            API_Response<Position> result = new API_Response<Position>();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, getPosition);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<API_Response<Position>>(response.Content.ReadAsStringAsync().Result);
            }

            return result;

                           
        }

        public async static Task<LoginResult> LogOut(MKFUser user)
        {
            LoginResult result = new LoginResult();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, logoutAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        public async static Task<LoginResult> UpdatePassword(MKFUser user)
        {
            LoginResult result = new LoginResult();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, updatePasswordAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        public async static Task<API_Response<AccountSummary>> GetAccountSummary(ParamDate paramDate)
        {
            API_Response<AccountSummary> result = new API_Response<AccountSummary>();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, getAccountSummaryAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(paramDate);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<API_Response<AccountSummary>>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        public async static Task<API_Response<TRNS>> GetStatement(ParamDate paramDate)
        {
            API_Response<TRNS> result = new API_Response<TRNS>();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, getStatementAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(paramDate);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<API_Response<TRNS>>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        public async static Task<API_Response<Operations>> GetOperations(ParamDate paramDate)
        {
            API_Response<Operations> result = new API_Response<Operations>();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, getOperationsAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(paramDate);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<API_Response<Operations>>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        public async static Task<API_Response<ClosedOperations>> GetRealisedProfit(ParamDate paramDate)
        {
            API_Response<ClosedOperations> result = new API_Response<ClosedOperations>();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, getRealisedProfitAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(paramDate);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<API_Response<ClosedOperations>>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        public async static Task<API_Response<RiskSummary>> GetRiskSummary(MKFUser user)
        {
            API_Response<RiskSummary> result = new API_Response<RiskSummary>();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, getRiskSummaryAction);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<API_Response<RiskSummary>>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        public async static Task<API_Response<PortfolioSummary>> GetPortfolioSummary(MKFUser user)
        {
            API_Response<PortfolioSummary> result = new API_Response<PortfolioSummary>();
            HttpClient client = new HttpClient();
            string serviceURL = string.Format("{0}{1}", serviceBaseURI, getPortfolioSummary);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(serviceURL, stringContent);

            if (response.IsSuccessStatusCode)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<API_Response<PortfolioSummary>>(response.Content.ReadAsStringAsync().Result);
            }

            return result;


        }

        /*	public async static Task<ServiceResponse<User>> Login(string email, string password, string deviceId)
            {
                User result = new User();
                string message = string.Empty;
                try
                {
                    HttpClient client = new HttpClient();
                    string serviceURL = string.Format("{0}{1}", serviceBaseURI, loginAction);
                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                        new KeyValuePair<string, string>("email", email),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("device_id", deviceId),
                    };
                    var content = new FormUrlEncodedContent(parameters);
                    var response = await client.PostAsync(serviceURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        //result = await response.Content.ReadAsStringAsync();
                        XDocument doc = XDocument.Load(response.Content.ReadAsStreamAsync().Result);

                        if (doc.Root.Element(XName.Get("status")).Value == "200")
                        {
                            XElement userElement = doc.Root.Element(XName.Get("user"));
                            result.FirstName = userElement.Element(XName.Get("firstName")).Value;
                            result.LastName = userElement.Element(XName.Get("lastName")).Value;
                            result.Email = userElement.Element(XName.Get("email")).Value;
                            result.SecurityToken = userElement.Element(XName.Get("security_token")).Value;
                            result.UserRoles = userElement.Descendants(XName.Get("role")).Select(role => role.Value).ToList();
                            message = CRMSService.SUCCESS;
                        }
                        else
                        {
                            result = null;
                            message = doc.Root.Element(XName.Get("message")).Value;
                            //throw new Exception(string.Format("ERROR:{0}", message));
                        }
                    }
                }
                catch (WebException ex)
                {
                    result = null;
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (HttpRequestException ex)
                {
                    result = null;
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (Exception ex)
                {
                    result = null;
                    message = ex.Message;
                }
                return new ServiceResponse<User>(result, message);
            }

            public async static Task<string> Register(string email = "arte@artea.info", string password = "pass123", string firstname = "NameF1", string lastname = "NameS2")
            {
                string result = string.Empty;
                HttpClient client = new HttpClient();
                string serviceURL = string.Format("{0}{1}", serviceBaseURI, userRegister);
                var parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("firstName", firstname),
                    new KeyValuePair<string, string>("lastName", lastname)
                };
                var content = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync(serviceURL, content);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }

                return result;
            }


            public async static Task<ServiceResponse<BookData>> GetUserBookLastUpdateTime(BlockItem blockItem, string securityToken, string deviceId, CancellationToken cancellationToken)
            {
                BookData result = null;
                string message = string.Empty;

                try
                {
                    HttpClient client = new HttpClient();

                    string serviceURL = string.Format("{0}{1}", serviceBaseURI, getUserBookLastUpdateTime);

                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                        new KeyValuePair<string, string>("security_token", securityToken),
                        new KeyValuePair<string, string>("device_id", deviceId),
                        new KeyValuePair<string, string>("book_sku", blockItem.BookRef),
                        new KeyValuePair<string, string>("note_type", blockItem.ItemType.GetEnumDescription())
                    };

                    var content = new FormUrlEncodedContent(parameters);
                    var response = await client.PostAsync(serviceURL, content, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        XDocument doc = XDocument.Load(response.Content.ReadAsStreamAsync().Result);

                        if (doc.Root.Element(XName.Get("status")).Value == "200")
                        {
                            result = doc.Root.Descendants(XName.Get("note")).Select(x => new BookData()
                                {
                                    ServerID = Convert.ToInt32(x.Element(XName.Get("note_id")).Value),
                                    BookSKU = x.Element(XName.Get("book_sku")).Value,
                                    Type = x.Element(XName.Get("note_type")).Value,
                                    TimeStamp = Utilities.GetCRMSDATE((x.Element(XName.Get("last_updated"))).Value, Utilities.DF_FOLENS_TIME).Value
                                }).FirstOrDefault();
                        }
                        else
                        {
                            result = null;
                            message = doc.Root.Element(XName.Get("message")).Value;
                        }
                    }
                }
                catch (WebException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (HttpRequestException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                return new ServiceResponse<BookData>(result, message);
            }

            public async static Task<ServiceResponse<List<Book>>> GetUserBooks(string securityToken, string deviceId)//string owner = "pat@ican.ie", 
            {
                List<Book> result = new List<Book>();
                string message = string.Empty;
                try
                {
                    HttpClient client = new HttpClient();
                    client.Timeout = new TimeSpan(0, 0, 60);

                    string serviceURL = string.Format("{0}{1}", ServiceURL, getUserBooks);
                    //string serviceURL = string.Format("{0}{1}", serviceBaseURI, getUserBooks);

                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                        new KeyValuePair<string, string>("security_token", securityToken),
                        new KeyValuePair<string, string>("device_id", deviceId),
                        //new KeyValuePair<string, string>("book_owner", owner)
                    };
                    var content = new FormUrlEncodedContent(parameters);
                    var response = await client.PostAsync(serviceURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // = await response.Content.ReadAsStringAsync();
                        XDocument doc = XDocument.Load(response.Content.ReadAsStreamAsync().Result);

                        if (doc.Root.Element(XName.Get("status")).Value == "200")
                        {
                            result = BookManager.ParseBuyBooksList(doc);
                        }
                        message = CRMSService.SUCCESS;
                    }
                }
                catch (WebException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (HttpRequestException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                return new ServiceResponse<List<Book>>(result, message);
            }

            public async static Task<ServiceResponse<List<BookData>>> GetUserNotes(Book book, BlockItem blockItem, string securityToken, string deviceId, CancellationToken cancellationToken)
            {
                List<BookData> result = new List<BookData>();
                string message = string.Empty;

                try
                {
                    HttpClient client = new HttpClient();
                    string serviceURL = string.Format("{0}{1}", serviceBaseURI, getUserNotes);
                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                        new KeyValuePair<string, string>("security_token", securityToken),
                        new KeyValuePair<string, string>("device_id", deviceId),
                        new KeyValuePair<string, string>("book_sku", book.BookRef),
                        new KeyValuePair<string, string>("note_type", blockItem.ItemType.GetEnumDescription())
                    };

                    if (blockItem.TimeStamp != DateTime.MinValue)
                    {
                        new KeyValuePair<string, string>("last_updated", blockItem.TimeStamp.ToCRMSDateString(Utilities.DF_FOLENS_TIME));
                    }


                    var content = new FormUrlEncodedContent(parameters);
                    var response = await client.PostAsync(serviceURL, content, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        XDocument doc = XDocument.Load(response.Content.ReadAsStreamAsync().Result);

                        if (doc.Root.Element(XName.Get("status")).Value == "200")
                        {
                            result = doc.Root.Descendants(XName.Get("note")).Select(x => new BookData()
                                {
                                    AppItemID = x.Element(XName.Get("app_item_id")).Value,
                                    //AppItemID = "12",
                                    ServerID = Convert.ToInt32(x.Element(XName.Get("note_id")).Value),
                                    BookSKU = x.Element(XName.Get("book_sku")).Value,
                                    Type = x.Element(XName.Get("note_type")).Value,
                                    Text = x.Element(XName.Get("note_text")).Value,
                                    AdditionnalInfo = DF.Common.Utilities.DeserializeString<SerializableDictionary>(x.Element(XName.Get("additionalinfo")).ToString().Replace("additionalinfo", "map")),
                                    Files = x.Descendants(XName.Get("file")).Select(fileElement => fileElement.Value).ToList()
                                }).ToList();
                        }
                        else
                        {
                            result = null;
                            message = doc.Root.Element(XName.Get("message")).Value;
                        }
                    }
                }
                catch (WebException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (HttpRequestException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                return new ServiceResponse<List<BookData>>(result, message);
            }

            public async static Task<ServiceResponse<int>> DeleteUserNote(BlockItem pageBlock, string securityToken, string deviceId, CancellationToken cancellationToken)
            {
                int result = 0;
                string message = string.Empty;

                try
                {
                    HttpClient client = new HttpClient();
                    string serviceURL = string.Format("{0}{1}", serviceBaseURI, deleteUserNote);
                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                        new KeyValuePair<string, string>("note_id", pageBlock.ServerID.ToString()),
                        new KeyValuePair<string, string>("security_token", securityToken),
                        new KeyValuePair<string, string>("device_id", deviceId),
                    };
                    var content = new FormUrlEncodedContent(parameters);
                    var response = await client.PostAsync(serviceURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        XDocument doc = XDocument.Load(response.Content.ReadAsStreamAsync().Result);

                        if (doc.Root.Element(XName.Get("status")).Value == "200")
                        {
                            result = Convert.ToInt32(doc.Root.Element(XName.Get("note_id")).Value);
                        }
                        else
                        {
                            message = doc.Root.Element(XName.Get("message")).Value;
                        }
                    }
                }
                catch (WebException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (HttpRequestException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                return new ServiceResponse<int>(result, message);
            }

            public async static Task<ServiceResponse<int>> DeleteUserNotes(int[] noteIds, string securityToken, string deviceId)
            {
                int result = 0;
                string message = string.Empty;

                try
                {
                    HttpClient client = new HttpClient();
                    string serviceURL = string.Format("{0}{1}", serviceBaseURI, deleteUserNotes);
                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                        new KeyValuePair<string, string>("security_token", securityToken),
                        new KeyValuePair<string, string>("device_id", deviceId),
                    };

                    foreach (var noteId in noteIds)
                    {
                        parameters.Add(new KeyValuePair<string, string>("note_id[]", noteId.ToString()));
                    }

                    var content = new FormUrlEncodedContent(parameters);

                    var response = await client.PostAsync(serviceURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        XDocument doc = XDocument.Load(response.Content.ReadAsStreamAsync().Result);

                        if (doc.Root.Element(XName.Get("status")).Value == "200")
                        {
                            //result = Convert.ToInt32(doc.Root.Element(XName.Get("note_id")).Value);
                        }
                        else
                        {
                            message = doc.Root.Element(XName.Get("message")).Value;
                        }
                    }
                }
                catch (WebException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (HttpRequestException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                return new ServiceResponse<int>(result, message);
            }

            public async static Task<ServiceResponse<int>> AddUpdateUserNote(BookData data, string securityToken, string deviceId, CancellationToken cancellationToken)
            {
                int result = 0;
                string message = string.Empty;

                try
                {
                    HttpClient client = new HttpClient();
                    string action = data.ServerID > 0 ? updateUserNote : putUserNote;

                    string serviceURL = string.Format("{0}{1}", serviceBaseURI, action);

                    var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("secretKey", SECRET_KEY),
                        new KeyValuePair<string, string>("note_type", data.Type),
                        new KeyValuePair<string, string>("book_sku", data.BookSKU),
                        new KeyValuePair<string, string>("app_item_id", data.AppItemID),
                        new KeyValuePair<string, string>("item_timestamp", data.TimeStamp.ToCRMSDateString(Utilities.DF_UNIVERSAL_TIME)),//"O"
                        new KeyValuePair<string, string>("note_text", data.Text),
                        new KeyValuePair<string, string>("security_token", securityToken),
                        new KeyValuePair<string, string>("device_id", deviceId),
                    };

                    if (data.ServerID > 0)
                    {
                        parameters.Add(new KeyValuePair<string, string>("note_id", data.ServerID.ToString()));
                    }

                    string mapInfo = DF.Common.Utilities.XMLSerializeOneLine(data.AdditionnalInfo);
                    string additionalInfo = DF.Common.Utilities.ChangeXMLRootName(mapInfo, "additionalinfo");
                    parameters.Add(new KeyValuePair<string, string>("additionalinfo", additionalInfo));


                    var content = new MultipartFormDataContent();
                    foreach (var keyValuePair in parameters)
                    {
                        content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                    }

                    #region Physical File
                    //if (data.PhysicalFiles != null)
                    //{
                    //    foreach (var file in data.PhysicalFiles)
                    //    {
                    //        byte[] fileArray = await ReadFile(file);
                    //        var fileContent = new ByteArrayContent(fileArray);
                    //        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    //        {
                    //            Name = "attached_file[]",
                    //            FileName = file.Name
                    //        };
                    //        content.Add(fileContent);
                    //    }
                    //}
                    #endregion

                    if (data.Base64Files != null)
                    {
                        int counter = 0;
                        foreach (var file in data.Base64Files)
                        {
                            string fileName = string.Format("{0}-{1}.png", data.LocalID, counter);
                            byte[] fileArray = Functions.ConvertStringImageToBytes(file);
                            var fileContent = new ByteArrayContent(fileArray);
                            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                Name = "attached_file[]",
                                FileName = fileName
                            };

                            counter++;
                            content.Add(fileContent);
                        }
                    }
                    //string s = "<additionalinfo><item key=\"data\">[{\"type\":\"Note\",\"text\":\"les bâtiments, les grandes rues et les \\n                \\n                \\n                    événements sportifs en France sont très célèbres.\\n                \\n                Some of the buildings, monuments, avenues\",\"page\":1,\"id\":\"[\\\"6\\\",\\\"7\\\",\\\"8\\\",\\\"9\\\",\\\"10\\\",\\\"11\\\"]\",\"bounds\":\"[\\\"1319,1358\\\",\\\"1365,1382\\\",\\\"1424,1445\\\",\\\"1469,1518\\\",\\\"1529,1546\\\",\\\"1595,1636\\\"]\",\"pName\":\"iframe_0002\",\"group\":\"5\",\"jump\":\"[\\\"1\\\",\\\"1\\\",\\\"1\\\",\\\"1\\\",\\\"1\\\",\\\"1\\\"]\",\"desc\":\"{\\\\rtf1\\\\fbidis\\\\ansi\\\\ansicpg1252\\\\deff0\\\\nouicompat\\\\deflang1033{\\\\fonttbl{\\\\f0\\\\fnil\\\\fcharset0 Segoe UI;}{\\\\f1\\\\fnil Segoe UI;}}\\r\\n{\\\\colortbl ;\\\\red255\\\\green0\\\\blue0;\\\\red0\\\\green0\\\\blue0;}\\r\\n{\\\\*\\\\generator Riched20 6.3.9600}\\\\viewkind4\\\\uc1 \\r\\n\\\\pard\\\\ltrpar\\\\tx720\\\\cf1\\\\b\\\\f0\\\\fs24 d\\\\cf2 s\\\\b0 dsd\\\\cf1\\\\b  hello world\\\\cf2\\\\b0\\\\f1\\\\par\\r\\n\\r\\n\\\\pard\\\\ltrpar\\\\tx720\\\\par\\r\\n}\\r\\n\"}]</item></additionalinfo>";

                    var response = await client.PostAsync(serviceURL, content, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        XDocument doc = XDocument.Load(response.Content.ReadAsStreamAsync().Result);

                        if (doc.Root.Element(XName.Get("status")).Value == "200")
                        {
                            result = Convert.ToInt32(doc.Root.Element(XName.Get("note_id")).Value);
                        }

                        if (doc.Root.Element(XName.Get("message")) != null)
                        {
                            message = doc.Root.Element(XName.Get("message")).Value;
                        }
                    }
                }
                catch (WebException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (HttpRequestException ex)
                {
                    message = CRMSService.ERROR_INTERNET_CONNECTION;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                return new ServiceResponse<int>(result, message);
            } */

    }
}


