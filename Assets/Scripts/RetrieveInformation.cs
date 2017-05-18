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

    // Information to store
    private string internalIp;
    private IpInfoObject ipInfoObject;
    
    private string batteryLevel;
    private string batteryStatus;
    private string deviceModel;
    private string deviceName;
    private string deviceType;
    private string deviceUniqueIdentifier;
    
    private string graphicsDeviceID;
    private string graphicsDeviceName;
    private string graphicsDeviceType;
    private string graphicsDeviceVendor;
    private string graphicsDeviceVendorID;
    private string graphicsDeviceVersion;
    private string graphicsMemorySize;
    
    private string operatingSystem;
    private string operatingSystemFamily;
    private string processorCount;
    private string processorFrequency;
    private string systemMemorySize;
    private string systemUsername;

    private string macAddress;
    private string networkAdapterDescription;
    private string networkAdapterName;
    
    void Start()
    {
        // System information
        SetSystemInfo();

        // MAC-address
        SetMacAddressInfo();

        // Alternatief voor enkel de externe ip, werkt maar is wel langzamer
        /*WWW www = new WWW("http://checkip.dyndns.org");
        StartCoroutine(GetExternalIP(www));*/

        // Ip information
        SetInternalIP();
        WWW www = new WWW("https://ipinfo.io/json");
        StartCoroutine(GetIPInfo(www));
        
        // Create file on desktop
        //CreateTextfile();

        // Print available text files on desktop
        //PrintTextFilesOnDesktop();
    }
    
    void SetSystemInfo(){
        batteryLevel = ""+SystemInfo.batteryLevel;
        batteryStatus = ""+SystemInfo.batteryStatus;
        deviceModel = ""+SystemInfo.deviceModel;
        deviceName = ""+SystemInfo.deviceName;
        deviceType = ""+SystemInfo.deviceType;
        deviceUniqueIdentifier = ""+SystemInfo.deviceUniqueIdentifier;
        graphicsDeviceID = ""+SystemInfo.graphicsDeviceID;
        graphicsDeviceName = ""+SystemInfo.graphicsDeviceName;
        graphicsDeviceType = ""+SystemInfo.graphicsDeviceType;
        graphicsDeviceVendor = ""+SystemInfo.graphicsDeviceVendor;
        graphicsDeviceVendorID = ""+SystemInfo.graphicsDeviceVendorID;
        graphicsDeviceVersion = ""+SystemInfo.graphicsDeviceVersion;
        graphicsMemorySize = ""+SystemInfo.graphicsMemorySize;
        operatingSystem = ""+SystemInfo.operatingSystem;
        operatingSystemFamily = ""+SystemInfo.operatingSystemFamily;
        processorCount = ""+SystemInfo.processorCount;
        processorFrequency = ""+SystemInfo.processorFrequency;
        systemMemorySize = ""+SystemInfo.systemMemorySize;
        systemUsername = ""+System.Environment.UserName;
    }
    private string GetGPS () {
        return "";
    }
    private void SetInternalIP(){
        string strHostName = "";
        strHostName = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        internalIp = addr[addr.Length-1].ToString();
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
            ipInfoObject = JsonUtility.FromJson<IpInfoObject>(myInfo);
            print("Externe Ip (object) = " + ipInfoObject.ip);
        }
        else
        {
            print("Externe info onbekend");
        }
        CreateWebResult();
    }
    private void CreateWebResult()
    {
        string webContent = "<!DOCTYPE html><html lang='en'> <head> <meta charset='utf-8'> <title>The Privacy Game </title> <meta name='description' content='Your information has been leaked!'> <meta name='author' content='PrivacyGame'> </head> <style> body {font-family: Arial;} </style> <body style='background-color:lightblue; '> <div> <h1>Thanks for playing the privacy game!</h1> <h3 style='color:red'>The goal of this game is to show you that data can be gathered without your knowledge!</h3> <h3>The following data has been gathered while you were playing: </h3> <img src='photo.png'><table style='width:25%' border=1> <tr> <td>Interne IP</td> <td>"+internalIp+"</td> </tr> <tr> <td>Externe IP</td> <td>"+ipInfoObject.ip+"</td> </tr> <tr> <td>Hostnaam</td> <td>"+ipInfoObject.hostname+"</td> </tr> <tr> <td>Stad</td> <td>"+ipInfoObject.city+"</td> </tr> <tr> <td>Regio</td> <td>"+ipInfoObject.region+"</td> </tr> <tr> <td>Land</td> <td>"+ipInfoObject.country+"</td> </tr> <tr> <td>Locatie</td> <td>"+ipInfoObject.loc+"</td> </tr> <tr> <td>Provider</td> <td>"+ipInfoObject.org+"</td> </tr> <tr> <td>Postcode</td> <td>"+ipInfoObject.postal+"</td> </tr> <tr> <td>Batterijduur</td> <td>"+batteryLevel+"</td> </tr> <tr> <td>Batterij status</td> <td>"+batteryStatus+"</td> </tr> <tr> <td>Apparaat</td> <td>"+deviceModel+"</td> </tr> <tr> <td>Naam apparaat</td> <td>"+deviceName+"</td> </tr> <tr> <td>Type apparaat</td> <td>"+deviceType+"</td> </tr> <tr> <td>Unieke code van apparaat</td> <td>"+deviceUniqueIdentifier+"</td> </tr> <tr> <td>Grafische kaart id</td> <td>"+graphicsDeviceID+"</td> </tr> <tr> <td>Grafische kaart naam</td> <td>"+graphicsDeviceName+"</td> </tr> <tr> <td>Grafische kaart type</td> <td>"+graphicsDeviceType+"</td> </tr> <tr> <td>Grafische kaart fabrikant</td> <td>"+graphicsDeviceVendor+"</td> </tr> <tr> <td>Grafische kaart fabrikant id</td> <td>"+graphicsDeviceVendorID+"</td> </tr> <tr> <td>Grafische kaart versie</td> <td>"+graphicsDeviceVersion+"</td> </tr> <tr> <td>Grafisch geheugen</td> <td>"+graphicsMemorySize+"</td> </tr> <tr> <td>Besturingssysteem</td> <td>"+operatingSystem+"</td> </tr> <tr> <td>Besturingssysteem familie</td> <td>"+operatingSystemFamily+"</td> </tr> <tr> <td>Processors</td> <td>"+processorCount+"</td> </tr> <tr> <td>Processor frequentie</td> <td>"+processorFrequency+"</td> </tr> <tr> <td>Systeemgeheugen</td> <td>"+systemMemorySize+"</td> </tr> <tr> <td>Systeemgebruiker</td> <td>"+systemUsername+"</td> </tr> <tr> <td>MAC Adres</td> <td>"+macAddress+"</td> </tr> <tr> <td>Netwerkadapter omschrijving</td> <td>"+networkAdapterDescription+"</td> </tr> <tr> <td>Netwerkadapter naam</td> <td>"+networkAdapterName+"</td> </tr> </table> </div> </body></html>";
         System.IO.File.WriteAllText("privacy.html", webContent);
    }
    private void CreateTextfile()
    {
        print("creating text file");
        print(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop));
        System.IO.File.WriteAllText((System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\privacy.txt"), "String one" + ", " + "String two");
    }
    private void SetMacAddressInfo()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            //if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            if (nic.Name == "Hamachi") continue;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddress = nic.GetPhysicalAddress().ToString();
                networkAdapterDescription = nic.Description;
                networkAdapterName = nic.Name;
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
