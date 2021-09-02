using System.Net.Http;
using System;
using System.Configuration;
using System.Net;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;
using log4net;
using System.Collections.Generic;

namespace BabilonUSSD
{
    public class UssdService
    {
        private static SmppClient _client;
        private readonly ILog _log = LogManager.GetLogger(typeof(UssdService));
        private Timer _connectionWatcherTimer;
        private string _serviceNumber;
        private Dictionary<string, string> clientd_dict = new Dictionary<string, string>();
        public void Start()
        {


            _connectionWatcherTimer = new Timer(30000, Timer.TimerElapseStartMode.Immediate, Timer.TimerElapseReentranceMode.NonReentrant) { AutoReset = true };
            _connectionWatcherTimer.Elapsed += ConnectionWatcherTimer_Elapsed;
            _connectionWatcherTimer.Start();
        }

        //private HttpSelfHostServer _serv;



        private void ConnectionWatcherTimer_Elapsed(object sender, Timer.ElapsedEventArgs e)
        {

            if (_client != null && _client.Connected && (_client.Status == ConnectionStatus.Bound))
            {
                return;
            }
            ConnectToUssdc();
        }

        public void Stop()
        {
            if (_client != null && _client.Connected)
            {
                _client.Disconnect();
            }
            _client = null;
        }

        private void ConnectToUssdc()
        {
            try
            {
                _log.Error(Configs.Gateway + "Config");
                string gateway = Configs.Gateway;
                int port = Configs.Port;
                string username = Configs.Username;
                string password = Configs.Password;
                _serviceNumber = Configs.Service;


                _client = null;
                _client = new SmppClient
                {
                    EnquireInterval = 60,
                    Timeout = 30000,
                    NeedEnquireLink = true,
                    AddrNpi = (byte)SmppEnums.Npi.Unknown,
                    AddrTon = (byte)SmppEnums.Ton.Unknown,
                    AddressRange = _serviceNumber,
                    WorkerThreads = 99,
                    SystemType = ""
                };
                Console.WriteLine("2");
                _client.WorkerThreads = 99;
                _client.evDeliverSm += SmppClientDeliverSm;
                _client.evDisconnected += SmppClientDisconnected;

                var ipEp = new IPEndPoint(IPAddress.Parse(gateway), port);
                if (ConfigurationManager.AppSettings["LocalPort"] != "")
                {
                    _client.LocalEndPoint = new IPEndPoint(IPAddress.Any, int.Parse(ConfigurationManager.AppSettings["LocalPort"]));
                    _log.DebugFormat("SourcePort : {0}", ConfigurationManager.AppSettings["LocalPort"]);
                }
                Console.WriteLine("3");
                _log.Debug("Trying to connect to USSDC");
                _client.Connect(ipEp);

                var btrp = _client.Bind(username, password);

                switch (btrp.Status)
                {
                    case CommandStatus.ESME_ROK: Console.WriteLine("Ussd center connected"); _log.Debug("USSD center connected"); break;
                    default: _client.Disconnect(); _client = null; break;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                Console.WriteLine(ex);
                Console.WriteLine("Error connect to USSD center", ex);
            }
        }

        private void SmppClientDisconnected(object sender)
        {
            Console.WriteLine("Ussd center disconnected");
        }
        private void SmppClientDeliverSm(object sender, DeliverSm data)
        {
            try
            {
                DataCodings dataCoding1 = DataCodings.Default;
                if (data.DataCoding == (DataCodings)0x48)
                {
                    dataCoding1 = DataCodings.UCS2;
                }
                else if (data.DataCoding == DataCodings.UCS2)
                {
                    dataCoding1 = DataCodings.UCS2;
                }

                string messageText = _client.GetMessageText(data.UserDataPdu.ShortMessage, dataCoding1);


                if (messageText.StartsWith("*") && messageText.Length == 5)
                {
                    messageText = "";
                }

                if (messageText.Length > 5 && messageText.StartsWith("*"))
                {
                    messageText = messageText.Remove(0, 5);
                    messageText = messageText.Remove(messageText.Length - 1);
                }

                string number = data.SourceAddr;
                if (data.SourceAddr.Length > 9)
                {
                    number = data.SourceAddr.Remove(0, 3);
                }


                Console.WriteLine("Request msisdn: " + number + " message: " + messageText);
                _log.Debug("msisdn: " + number.ToString() + " messageText: " + messageText);
                SendMessage(number, messageText);
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _log.Error("Error on USSD request ", ex);
            }
        }

        private async void SendMessage(string number, string message)
        {
            _log.Debug("Replying to ussd request: " + number + " " + message);

            if (!_client.Connected || _client.Status != ConnectionStatus.Bound)
            {
                return;
            }
            var new_message = "Неверный формат запроса";
            byte op_param = 17;
            byte session_param = 0;
            
            if (message == "0")
            {
                Console.WriteLine(clientd_dict.ContainsKey(number));
                if (clientd_dict.ContainsKey(number))
                {

                    clientd_dict.Remove(number);
                    session_param = 0;
                    op_param = 17;
                    new_message = "";
                    Console.WriteLine(new_message,"rr");
                    SendUSSD(op_param, session_param, number, new_message);
                    return;
                }

                //clientdRe_dict.Add(number, message);
                
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync("http://" + Configs.NotifAddress + "/sms/" + number + "/ussd");
                        response.EnsureSuccessStatusCode();
                        //string responseBody = await response.Content.ReadAsStringAsync();
                        // Above three lines can be replaced with new helper method below
                        // string responseBody = await client.GetStringAsync(uri);

                        Console.WriteLine("response ok");
                    }
                    new_message = "Ваш запрос принят, ожидайте ответ по SMS.";
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    _log.Error("error sending to kanel: {0}" + e.Message);
                    new_message = "Ошибка при подключении. Повторите попытку позже";
                }
            }

            else if (message ==""){

                if (clientd_dict.ContainsKey(number))
                {
                    clientd_dict.Remove(number);
                    session_param = 0;
                    op_param = 17;
                    new_message = "";
                    SendUSSD(op_param, session_param, number, new_message);
                    return;
                }

                clientd_dict.Add(number, message);

                new_message = "__________________";
                op_param = 2;
                session_param = 1;
            }

            else if (message == "1"){

                if (clientd_dict.ContainsKey(number))
                {
                    clientd_dict.Remove(number);
                }


                try  
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync("http://" + Configs.NotifAddress + "/sms/+992" + number + "/ussd");
                        response.EnsureSuccessStatusCode();
                        //string responseBody = await response.Content.ReadAsStringAsync();
                        // Above three lines can be replaced with new helper method below
                        // string responseBody = await client.GetStringAsync(uri);

                        Console.WriteLine("response ok");
                    }
                    new_message = "Ваш запрос принят, ожидайте ответ по SMS.";
                }  
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");  
                    Console.WriteLine("Message :{0} ",e.Message);
                    _log.Error("error sending to kanel: {0}" + e.Message);
                    new_message = "Ошибка при подключении. Повторите попытку позже";
                }

            }
            else
            {

                if (clientd_dict.ContainsKey(number))
                {
                    clientd_dict.Remove(number);
                }

            }
            _log.Debug("Replying to ussd request: " + number + " "+ message);

            _client.SubmitAsync(SMS.ForSubmit()
                           .ServiceType("USSD")
                           .Data(_client.GetMessageBytes(new_message, DataCodings.UCS2))
                           .From(_serviceNumber, (byte)SmppEnums.Ton.Unknown, (byte)SmppEnums.Npi.Unknown)
                           .To("992" + number)
                           .Coding((DataCodings)0x48)
                           .AddParameter(OptionalTags.UssdServiceOp, new byte[] { op_param })
                           .AddParameter(OptionalTags.ItsSessionInfo, new byte[] { 0, session_param })
                        );

        }
        private void SendUSSD(byte op_param, byte session_param,string number, string new_message)
        {
            _client.SubmitAsync(SMS.ForSubmit()
                    .ServiceType("USSD")
                    .Data(_client.GetMessageBytes(new_message, DataCodings.UCS2))
                    .From(_serviceNumber, (byte)SmppEnums.Ton.Unknown, (byte)SmppEnums.Npi.Unknown)
                    .To("992" + number)
                    .Coding((DataCodings)0x48)
                    .AddParameter(OptionalTags.UssdServiceOp, new byte[] { op_param })
                    .AddParameter(OptionalTags.ItsSessionInfo, new byte[] { 0, session_param })
                 );
        }
    }

}