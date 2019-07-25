﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls;
using Internal.Windows.Calls;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Devices.Haptics;

namespace WoADialer.Model
{
    internal static class MainEntities
    {
        public static bool Initialized { get; private set; }
        public static CallManager CallManager { get; private set; }
        public static PhoneLine DefaultLine { get; private set; }
        public static PhoneCallStore CallStore { get; private set; }
        public static PhoneCallHistoryStore CallHistoryStore { get; private set; }
        public static ProximitySensor ProximitySensor { get; private set; }
        
        public static async Task Initialize()
        {
            DeviceInformationCollection devices;
            CallManager = await CallManager.GetSystemPhoneCallManagerAsync();
            CallStore = await PhoneCallManager.RequestStoreAsync();
            CallHistoryStore = await PhoneCallHistoryManager.RequestStoreAsync(PhoneCallHistoryStoreAccessType.AllEntriesReadWrite);
            devices = await DeviceInformation.FindAllAsync(ProximitySensor.GetDeviceSelector());
            ProximitySensor = devices.Count > 0 ? ProximitySensor.FromId(devices.First().Id) : null;
            try
            {
                DefaultLine = await PhoneLine.FromIdAsync(await CallStore.GetDefaultLineAsync());
            }
            catch
            {

            }
            Initialized = true;
        }
    }
}
