using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabilonUSSD
{
    class SmppEnums
    {
        public enum ConnectionStatus
        {
            Open,
            Bound,
            Outbound,
            Closed,
        }

        public enum ConnectionMode
        {
            Transmitter,
            Receiver,
            Transceiver,
        }

        public enum CommandStatus : uint
        {
            EsmeRok = 0U,
            EsmeRinvmsglen = 1U,
            EsmeRinvcmdlen = 2U,
            EsmeRinvcmdid = 3U,
            EsmeRinvbndsts = 4U,
            EsmeRalybnd = 5U,
            EsmeRinvprtflg = 6U,
            EsmeRinvregdlvflg = 7U,
            EsmeRsyserr = 8U,
            EsmeRinvsrcadr = 10U,
            EsmeRinvdstadr = 11U,
            EsmeRinvmsgid = 12U,
            EsmeRbindfail = 13U,
            EsmeRinvpaswd = 14U,
            EsmeRinvsysid = 15U,
            EsmeRcancelfail = 17U,
            EsmeRreplacefail = 19U,
            EsmeRmsgqful = 20U,
            EsmeRinvsertyp = 21U,
            EsmeRinvnumdests = 51U,
            EsmeRinvdlname = 52U,
            EsmeRinvdestflag = 64U,
            EsmeRinvsubrep = 66U,
            EsmeRinvesmclass = 67U,
            EsmeRcntsubdl = 68U,
            EsmeRsubmitfail = 69U,
            EsmeRinvsrcton = 72U,
            EsmeRinvsrcnpi = 73U,
            EsmeRinvdstton = 80U,
            EsmeRinvdstnpi = 81U,
            EsmeRinvsystyp = 83U,
            EsmeRinvrepflag = 84U,
            EsmeRinvnummsgs = 85U,
            EsmeRthrottled = 88U,
            EsmeRinvsched = 97U,
            EsmeRinvexpiry = 98U,
            EsmeRinvdftmsgid = 99U,
            EsmeRxTAppn = 100U,
            EsmeRxPAppn = 101U,
            EsmeRxRAppn = 102U,
            EsmeRqueryfail = 103U,
            EsmeRinvoptparstream = 192U,
            EsmeRoptparnotallwd = 193U,
            EsmeRinvparlen = 194U,
            EsmeRmissingoptparam = 195U,
            EsmeRinvoptparamval = 196U,
            EsmeRdeliveryfailure = 254U,
            EsmeRunknownerr = 255U,
            SmppclientUnexpresp = 4097U,
            SmppclientRcvtimeout = 4098U,
            SmppclientNoconn = 4099U,
            SmppclientUnbound = 4100U,
            SmppclientGenericNack = 8190U,
            SmppclientUnknownerror = 8191U,
        }

        public enum CommandSet : uint
        {
            UnknownPacket = 0U,
            BindReceiver = 1U,
            BindTransmitter = 2U,
            QuerySm = 3U,
            SubmitSm = 4U,
            DeliverSm = 5U,
            Unbind = 6U,
            ReplaceSm = 7U,
            CancelSm = 8U,
            BindTransceiver = 9U,
            Outbind = 11U,
            EnquireLink = 21U,
            SubmitMulti = 33U,
            AlertNotification = 258U,
            DataSm = 259U,
            GenericNack = 2147483648U,
            BindReceiverResp = 2147483649U,
            BindTransmitterResp = 2147483650U,
            QuerySmResp = 2147483651U,
            SubmitSmResp = 2147483652U,
            DeliverSmResp = 2147483653U,
            UnbindResp = 2147483654U,
            ReplaceSmResp = 2147483655U,
            CancelSmResp = 2147483656U,
            BindTransceiverResp = 2147483657U,
            EnquireLinkResp = 2147483669U,
            SubmitMultiResp = 2147483681U,
            DataSmResp = 2147483907U,
        }

        public enum DataCodings : byte
        {
            Default = (byte)0,
            Ascii = (byte)1,
            Octets = (byte)2,
            Latin1 = (byte)3,
            OctetUnspecified = (byte)4,
            Cyrllic = (byte)6,
            LatinHebrew = (byte)7,
            Ucs2 = (byte)8,
            DefaultFlashSms = (byte)16,
            UnicodeFlashSms = (byte)24,
            Class0 = (byte)240,
            Class1 = (byte)241,
            Class2 = (byte)242,
            Class3 = (byte)243,
            Class0Alert8Bit = (byte)244,
            Class1Me8Bit = (byte)245,
        }

        public enum DestinationAddressType : byte
        {
            SmeAddress = (byte)1,
            DistributionListName = (byte)2,
        }

        public enum GsmSpecificFeatures : byte
        {
            No = (byte)0,
            Udhi = (byte)64,
            ReplyPath = (byte)128,
            UdhIandReplyPath = (byte)192,
        }

        public enum InformationElementIdentifiers : byte
        {
            ConcatenatedShortMessages8Bit = (byte)0,
            SpecialSmsMessageIndication = (byte)1,
            ApplicationPortAddressingScheme8Bit = (byte)4,
            ApplicationPortAddressingScheme16Bit = (byte)5,
            SmscControlParameters = (byte)6,
            UdhSourceIndicator = (byte)7,
            ConcatenatedShortMessage16Bit = (byte)8,
            WirelessControlMessageProtocol = (byte)9,
            TextFormatting = (byte)10,
            PredefinedSound = (byte)11,
            UserDefinedSound = (byte)12,
            PredefinedAnimation = (byte)13,
            LargeAnimation = (byte)14,
            SmallAnimation = (byte)15,
            LargePicture = (byte)16,
            SmallPicture = (byte)17,
            VariablePicture = (byte)18,
            UserPromptIndicator = (byte)19,
            ExtendedObject = (byte)20,
            ReusedExtendedObject = (byte)21,
            CompressionControl = (byte)22,
            ObjectDistributionIndicator = (byte)23,
            StandardWvgObject = (byte)24,
            CharacterSizeWvgObject = (byte)25,
            ExtendedObjectDataRequestCommand = (byte)26,
            EMailHeader = (byte)32,
            HyperlinkFormatElement = (byte)33,
            ReplyAddressElement = (byte)34,
            EnhancedVoiceMailInformation = (byte)35,
            Unknown = (byte)255,
        }

        public enum IntermediateNotification : byte
        {
            NotRequested = (byte)0,
            Requested = (byte)16,
        }

        public enum MessageModes : byte
        {
            Default,
            Datagram,
            Forward,
            StoreForward,
        }

        public enum MessageState : byte
        {
            None,
            Enroute,
            Delivered,
            Expired,
            Deleted,
            Undeliverable,
            Accepted,
            Unknown,
            Rejected,
        }

        public enum MessageTypes : byte
        {
            Default = (byte)0,
            SmscDeliveryReceipt = (byte)4,
            SmeDeliveryAcknowledgement = (byte)8,
            SmeManualAcknowledgement = (byte)16,
            ConversationAbort = (byte)24,
            IntermediateDeliveryNotification = (byte)32,
        }

        public enum Npi : byte
        {
            Unknown = (byte)0,
            Isdn = (byte)1,
            DataX121 = (byte)3,
            TelexF69 = (byte)4,
            LandMobileE212 = (byte)6,
            National = (byte)8,
            Private = (byte)9,
            Ermes = (byte)10,
            Internet = (byte)14,
            Wap = (byte)18,
        }

        public enum OptionalTags : ushort
        {
            DestAddrSubunit = (ushort)5,
            DestNetworkType = (ushort)6,
            DestBearerType = (ushort)7,
            DestTelematicsId = (ushort)8,
            SourceAddrSubunit = (ushort)13,
            SourceNetworkType = (ushort)14,
            SourceBearerType = (ushort)15,
            SourceTelematicsId = (ushort)16,
            QosTimeToLive = (ushort)23,
            PayloadType = (ushort)25,
            AdditionalStatusInfoText = (ushort)29,
            ReceiptedMessageId = (ushort)30,
            MsMsgWaitFacilities = (ushort)48,
            PrivacyIndicator = (ushort)513,
            SourceSubaddress = (ushort)514,
            DestSubaddress = (ushort)515,
            UserMessageReference = (ushort)516,
            UserResponseCode = (ushort)517,
            SourcePort = (ushort)522,
            DestinationPort = (ushort)523,
            SarReferenceNumber = (ushort)524,
            LanguageIndicator = (ushort)525,
            SarTotalSegments = (ushort)526,
            SarSequenceNumber = (ushort)527,
            ScInterfaceVersion = (ushort)528,
            CallbackNumPresInd = (ushort)770,
            CallbackNumAtag = (ushort)771,
            NumberOfMessages = (ushort)772,
            CallbackNum = (ushort)897,
            DpfResult = (ushort)1056,
            SetDpf = (ushort)1057,
            MsAvailabilityStatus = (ushort)1058,
            NetworkErrorCode = (ushort)1059,
            MessagePayload = (ushort)1060,
            DeliveryFailureReason = (ushort)1061,
            MoreMessagesToSend = (ushort)1062,
            MessageState = (ushort)1063,
            UssdServiceOp = (ushort)1281,
            DisplayTime = (ushort)4609,
            SmsSignal = (ushort)4611,
            MsValidity = (ushort)4612,
            AlertOnMessageDelivery = (ushort)4876,
            ItsReplyType = (ushort)4992,
            ItsSessionInfo = (ushort)4995,
        }

        public enum SmeAcknowledgement : byte
        {
            NotRequested = (byte)0,
            Delivery = (byte)4,
            Manual = (byte)8,
            DeliveryManual = (byte)12,
        }

        public enum SmscDeliveryReceipt : byte
        {
            NotRequested,
            SuccessFailure,
            Failure,
        }

        public enum SubmitMode
        {
            ShortMessage = 1,
            Payload = 2,
            ShortMessageWithSar = 3,
        }

        public enum Ton : byte
        {
            Unknown = 0,
            International = 1,
            National = 2,
            NetworkSpecific = 3,
            SubscriberNumber = 4,
            Alfanumeric = 5,
            Abreviated = 6
        }
    }
}
