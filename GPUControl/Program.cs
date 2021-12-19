using MongoDataAccess.UserAccess;
using MongoDataAccess.Models;
using MongoDB.Driver;
using GPUControl;
using System;
using System.Diagnostics;
using System.IO;





helpedMethod helpedmethod = new helpedMethod();
helpedmethod.runTheMsiAfterborner();


FallDataAccess db = new FallDataAccess();



string macAddress = helpedmethod.GetMACAddress();

Console.WriteLine(macAddress);

List<MonitoringFromNodeModel> listOfallMonitoring = await db.GetAllJS();


MonitoringFromNodeModel m = helpedmethod.returnsameMAC(macAddress, listOfallMonitoring);
if (true == m.fall)
{
    
};
connectToGPUS c= new connectToGPUS();
c.ConnectToGPUS();
List<GPUModel> l = c.getMonitoring();
Console.WriteLine(l[0].BestWatt);