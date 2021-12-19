using MongoDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MongoDataAccess.UserAccess;

using System.Diagnostics;
using System.IO;

namespace GPUControl;

public class helpedMethod
{
    public MonitoringFromNodeModel returnsameMAC(string macFromNODEJS, List<MonitoringFromNodeModel> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].MAC == macFromNODEJS)
            {
                return list[i];
            }
        }
        return list[0];
    }


    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        }
        for (int i = 2; i <= sMacAddress.Length - 1; i = i + 3)
        {
            sMacAddress = sMacAddress.Insert(i, ":");

        }
        return sMacAddress.ToLower();
    }
    public void runTheMsiAfterborner()
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.Verb = "runas";
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        cmd.StandardInput.WriteLine(@"cd\");
        cmd.StandardInput.WriteLine(@"cd \Program Files (x86)\MSI Afterburner");
        cmd.StandardInput.WriteLine("MSIAfterburner.exe");

        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();
    }
}
