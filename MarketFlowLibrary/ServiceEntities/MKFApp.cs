﻿using System;
using System.Globalization;
//using Foundation;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Threading;
using System.Linq;
//using UIKit;
using System.Timers;
using System.Net.Http;
using System.Diagnostics;
using MarketFlowLibrary;
using System.Security.Policy;
using Android.App.Usage;
//using MarketFlowLibrary.API;

namespace MarketFlowLibrary
{
    public sealed class MKFApp
    {
        public const string AppIdentifier = "com.reader.folens";
        public const string InsightsKey = "f69ec18029ae848320f9c74e1d478f286202dbfe";
        const string InsightsLoggedInUser = "loggedInUser";
        const string InsightsBookInfo = "book";
        public static Url OpenedFile = null;
        public static string DATE_FORMAT = "MM/dd/yyyy";

        //public const float APPVERSION = 0.02f;
        private static readonly MKFApp instance = new MKFApp();

        //Debug settings
        string testKey = string.Empty;

        private static TimeSpan appTimer;
        bool isiOS11 = false;

        object synch = new object ();

        //private int checkLicenseInfoOnline = 1;
        private System.Timers.Timer licenseTimer;

        //public NSUrlSession downloadSession;
        //public DeviceType DeviceType { get { return Entities.DeviceType.iOS;  } }


        static MKFApp()
        {
        }

        private MKFApp()
        {

//            versionSetting = SettingsManager.GetSettingsByKey("version", new User() { ID = 0 });
//            this.version = versionSetting != null && !string.IsNullOrEmpty(versionSetting.Data) ? float.Parse(versionSetting.Data, CultureInfo.InvariantCulture.NumberFormat) : 0;



            //appTimer = new TimeSpan(24, 0, 0);
            //licenseTimer = new System.Timers.Timer();
            //licenseTimer.Interval = appTimer.TotalMilliseconds; 
            //licenseTimer.Elapsed += LicenseRefreshEventHandler;
            //licenseTimer.Start();
           
            //Version version = new Version(UIDevice.CurrentDevice.SystemVersion);
            //isiOS11 = (version >= new Version (11, 0));


        }


        //void HandleReachabilityChanged(object sender, EventArgs e)
        //{
        //    NetworkStats status = Reachability.RemoteHostStatus();
        //    isConnected = ((status == NetworkStatus.NotReachable) ? false : true);
        //}

        //void LicenseRefreshEventHandler(object sender, ElapsedEventArgs e)
        //{
        //    licenseTimer.Stop();

        //    try
        //    {
        //        if (this.IsConnected)
        //        {
        //            //this.GetServerTime();
        //            //this.GetConfig();

        //            //this.GetUserActivities();
        //            //this.GetApplicationSettings();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ;
        //    }

        //    licenseTimer.Start();
        //}

        public static MKFApp Current
        {

            get
            {
                return instance;
            }
        }


        public List<Position> UserPositions { get; set; }

        private MKFUser user;

        public MKFUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                if (value != null)
                {
                    //user.ID = UserManager.Save(user);
                    //GetUserLicenseSettings();
                    //GetApplicationLockedSetting();
                    //downloadFixModeSetting = null;

                    //GetReaderSettings();
                    //GetApplicationSettings();
                    //GetServerTime();
                    //GetConfig(); 




                    //versionSetting = SettingsManager.GetSettingsByKey("version", new User() { ID = 0 });
                    //this.version = versionSetting != null && !string.IsNullOrEmpty(versionSetting.Data) ? float.Parse(versionSetting.Data, CultureInfo.InvariantCulture.NumberFormat) : 0;  
                }
            }
        }

        public string GetUrl(){
            return MarketFlowLibrary.MarketFlowService.GetUrl();
        }

        public async Task<LoginResult> Login(string username, string password)
        {
            MKFUser input = new MKFUser();
            input.Username = username;
            input.Password = password;
            //input.IPAddr = BundleVersion.ToString(); //app version 
            //

            LoginResult loginResult = await MarketFlowLibrary.MarketFlowService.Login(input);

            if (loginResult.Success == true)
            { 
                MKFUser loggedUser = new MKFUser();
                loggedUser.Username = loginResult.CliID;
                loggedUser.WebCliCode = loginResult.WebCliCode;
                loggedUser.CliCode = loginResult.CliCode;

                this.User = loggedUser;
            }

            return loginResult;
        }

        public async Task<List<Position>> GetPositions(bool forceRefresh = false, bool saveResult = true)
        {
            List<Position> result = UserPositions;
            API_Response<Position> api_Response = new API_Response<Position>();
            if (UserPositions == null || forceRefresh == true)
            {
                api_Response = await MarketFlowLibrary.MarketFlowService.GetPosition(this.User);
                result = api_Response.Content;
                if (api_Response.Success == true)
                {
                    if (saveResult == true)
                    {
                        UserPositions = result;
                    }
                }
            }

            return result;
        }

        public async Task<LoginResult> Logout()
        {
            LoginResult loginResult = await MarketFlowLibrary.MarketFlowService.LogOut(this.User);
            if(loginResult.Success == true) {
                this.User = null;
                this.UserPositions = null;
            }
            return loginResult;
        }

        public async Task<LoginResult> UpdatePassword(string newPassword, string oldPassword, string webclicode, string clicode, string username){
            MKFUser input = new MKFUser();
            this.User.NewPassword = newPassword;
            this.User.Password = oldPassword;
            LoginResult loginResult = await MarketFlowLibrary.MarketFlowService.UpdatePassword(this.User);
            return loginResult;
        }

        public async Task<API_Response<AccountSummary>> GetAccountSummary(){
            ParamDate paramDate = new ParamDate();
            paramDate.userMKF = this.User;
            paramDate.DateTo = DateTime.Now;
            API_Response<AccountSummary> response = await MarketFlowLibrary.MarketFlowService.GetAccountSummary(paramDate);
            return response;
        }

        public async Task<List<TRNS>> GetStatement(ParamDate paramDate) {
            List<TRNS> response = new List<TRNS>();
            API_Response<TRNS> api_response = new API_Response<TRNS>();
            api_response = await MarketFlowLibrary.MarketFlowService.GetStatement(paramDate);
            if(api_response.Success == true) {
                response = api_response.Content;
            }
            return response;
        }

        public async Task<List<Operations>> GetOperations(ParamDate paramDate) {
            List<Operations> response = new List<Operations>();
            API_Response<Operations> api_response = new API_Response<Operations>();
            api_response = await MarketFlowLibrary.MarketFlowService.GetOperations(paramDate);
            if(api_response.Success == true) {
                response = api_response.Content;
            }
            return response;
        }

        public async Task<List<ClosedOperations>> GetRealisedProfits(ParamDate paramDate) {
            List<ClosedOperations> response = new List<ClosedOperations>();
            API_Response<ClosedOperations> api_response = new API_Response<ClosedOperations>();
            api_response = await MarketFlowLibrary.MarketFlowService.GetRealisedProfit(paramDate);
            if(api_response.Success == true) {
                response = api_response.Content;
            }
            return response;
        }

        public async Task<List<RiskSummary>> GetRiskSummary() {
            List<RiskSummary> response = new List<RiskSummary>();
            API_Response<RiskSummary> api_response = new API_Response<RiskSummary>();
            api_response = await MarketFlowLibrary.MarketFlowService.GetRiskSummary(this.User);
            if(api_response.Success == true) {
                response = api_response.Content;
            }
            return response;
        }

        public async Task<API_Response<PortfolioSummary>> GetPortfolioSummary() {
            API_Response<PortfolioSummary> api_response = new API_Response<PortfolioSummary>();
            api_response = await MarketFlowLibrary.MarketFlowService.GetPortfolioSummary(this.User);
            return api_response;
        }


        //private Device device;

        //public Device Device
        //{
        //    get
        //    {
        //        return device;
        //    }
        //    private set
        //    {
        //        device = value;
        //    }
        //}

        public bool IsLoggedIn
        {
            get
            {
                return user == null || user.WebCliCode == string.Empty ? false : true;
            }
        }

        //public float BundleVersion {
        //    get
        //    {
        //        float result = 0;
        //        float.TryParse(NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString(), out result);
        //        return result;
        //    }
        //}

       

        //private Device GetDeviceInfo()
        //{
        //    Device device = new Device();
        //    NSUuid uid = UIDevice.CurrentDevice.IdentifierForVendor;
        //    device.DeviceType = this.DeviceType.GetEnumDescription();//"iOS";
        //    device.DeviceInfo = new iOSHardware().GetModel(Xamarin.iOS.DeviceHardware.Version);
        //    device.DeviceID = uid.AsString();
        //    device.ReaderVersion = this.Version.ToString();

        //    return device;
        //}


        //public bool DisplayFirstRunController(bool forced = false)
        //{
        //    bool result = forced;
        //    if (!forced)
        //    {
        //        if (this.Version < ReaderApp.Current.BundleVersion)
        //        {
        //            this.Version = ReaderApp.Current.BundleVersion;
        //            result = true;
        //        }
        //    }

        //    return result;
        //}


        public static TimeSpan AppTimer
        {
            get
            {
                return appTimer;
            }

        }


        public bool IsiOS11 {
            get {
                return isiOS11;
            }
        }

        bool isConnected;

        //public bool IsConnected
        //{
        //    get
        //    {
        //        isConnected = MarketFlow.AppDelegate.AppNetworkStatus == NetworkStatus.NotReachable ? false : true;

        //        return isConnected;
        //    }
        //}

        //public static void Collect()
        //{
        //    NSUrlCache.SharedCache.RemoveAllCachedResponses();
        //    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        //}

        #region Insights



        public enum IssueSeverity : int
        {
            Error = 1,
            Warning = 2
        }

        //public void ReportIssue (Exception ex, Dictionary<string, string> info, IssueSeverity issueSeverity) 
        //{
        //    Insights.Severity severity = Insights.Severity.Error;
        //    if (issueSeverity == IssueSeverity.Error)
        //    {
        //        severity = Insights.Severity.Error;
        //    }
        //    else if (issueSeverity == IssueSeverity.Error)
        //    {
        //        severity = Insights.Severity.Warning;
        //    }

        //    Insights.Report (ex, info, severity);
        //    Insights.Save();
        //}
        #endregion
    }
}

