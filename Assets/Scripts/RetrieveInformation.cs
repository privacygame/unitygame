using UnityEngine;
using System.Net;
using UnityEngine.UI;
using System.Collections;
using System.Net.NetworkInformation;
using System.IO;

public class RetrieveInformation : MonoBehaviour {
    
    /*

    Extra mogelijkheden:

    - Bestanden verwijderen?
    - Foto's maken terwijl de game wordt gespeeld
    - Record audio
    - App (contacten, etc)..
    - Download files

        - Met behulp van game, data over gebruiker verzamelen
        - naam van persoon?
        - hoe oud is persoon?
        - e mail van persoon?

    */

    public RawImage rawimage;

    void Start()
    {
        // Project webcam onto plane (todo)
        //WebCamTexture webcamTexture = new WebCamTexture();
        //rawimage.texture = webcamTexture;
        //rawimage.material.mainTexture = webcamTexture;
        //webcamTexture.Play();

        // System information
        PrintSystemInfo();

        // Ip information
        print("Interne IP = " + GetInternalIP());
        WWW www = new WWW("https://ipinfo.io/json");
        StartCoroutine(GetIPInfo(www));

        // MAC-address
        PrintMacAddressInfo();

        // Alternatief voor enkel de externe ip, werkt maar is wel langzamer
        /*WWW www = new WWW("http://checkip.dyndns.org");
        StartCoroutine(GetExternalIP(www));*/

        // Create file on desktop
        CreateTextfile();

        // Print available text files on desktop
        //PrintTextFilesOnDesktop();
    }
   
	// Update is called once per frame
	void Update () {

    }
    
    void PrintSystemInfo(){
        print("Battery Level = "+SystemInfo.batteryLevel);
        print("Battery Status = "+SystemInfo.batteryStatus);
        
        print("Device Model = "+SystemInfo.deviceModel);
        print("Device Name = "+SystemInfo.deviceName);
        print("Device Type = "+SystemInfo.deviceType);
        print("Device Unique Identifier = "+SystemInfo.deviceUniqueIdentifier);
        
        print("Graphics Device ID = "+SystemInfo.graphicsDeviceID);
        print("Graphics Device Name = "+SystemInfo.graphicsDeviceName);
        print("Graphics Device Type = "+SystemInfo.graphicsDeviceType);
        print("Graphics Device Vendor = "+SystemInfo.graphicsDeviceVendor);
        print("Graphics Device Vendor ID = "+SystemInfo.graphicsDeviceVendorID);
        print("Graphics Device Version = "+SystemInfo.graphicsDeviceVersion);
        print("Graphics Memory Size = "+SystemInfo.graphicsMemorySize);
        
        print("OS = "+SystemInfo.operatingSystem);
        print("OS Family = "+SystemInfo.operatingSystemFamily);
        print("Aantal processors = "+SystemInfo.processorCount);
        print("Processorfrequentie = "+SystemInfo.processorFrequency);
        
        print("Geheugen = "+SystemInfo.systemMemorySize);
        print("PC Username = " + System.Environment.UserName);
    }
    private string GetGPS () {
        return "";
    }
    private string GetInternalIP(){
        string strHostName = "";
        strHostName = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        return addr[addr.Length-1].ToString();
    }
    private IEnumerator GetExternalIP(WWW myExtIPWWW) {
        yield return myExtIPWWW;
        if (myExtIPWWW.text != null)
        {
            string myExtIP = myExtIPWWW.text;
            myExtIP = myExtIP.Substring(myExtIP.IndexOf(":") + 1);
            myExtIP = myExtIP.Substring(0, myExtIP.IndexOf("<"));
            print("Externe IP = " + myExtIP);
        }
        else
        {
            print("Eterne IP onbekend");
        }
    }
    private IEnumerator GetIPInfo(WWW myInfoWWW){
        yield return myInfoWWW;
        print(myInfoWWW.text);
        if (myInfoWWW.text != null)
        {
            string myInfo = myInfoWWW.text;
            IpInfoObject ipInfoObject = JsonUtility.FromJson<IpInfoObject>(myInfo);
            print("Externe Ip (object) = " + ipInfoObject.ip);
        }
        else
        {
            print("Eterne info onbekend");
        }
    }
    private void CreateTextfile()
    {
        print("creating text file");
        print(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop));
        System.IO.File.WriteAllText((System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\privacy.txt"), "String one" + ", " + "String two");
    }
    private void PrintMacAddressInfo()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            //if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            if (nic.Name == "Hamachi") continue;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                string macAddress = nic.GetPhysicalAddress().ToString();
                string description = nic.Description;
                string name = nic.Name;

                print("MAC Address = "+macAddress);
                print("Network description = "+description);
                print("Network name = "+name);
                break;
            }
        }
    }
    private void PrintTextFilesOnDesktop()
    {
        foreach (string file in Directory.GetFiles(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "*.txt"))
        {
            Debug.Log(file);
        }
    }
}
