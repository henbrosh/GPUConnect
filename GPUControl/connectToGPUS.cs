using MSI.Afterburner;
using MSI.Afterburner.Exceptions;
using MongoDataAccess.UserAccess;
using MongoDataAccess.Models;

namespace GPUControl;

public class connectToGPUS
{

      ControlMemory macm = new ControlMemory();

    private List<GPUModel> getListOfBestSettings()
    {
        List<GPUModel> bestSettingsOfGPU = new List<GPUModel>(macm.GpuEntries.Length);
        return bestSettingsOfGPU;
    }
    public void ConnectToGPUS()
    {
        bool isDirty = false;
        try
        {
            //connect to MACM shared memory
            //  print out current MACM Header values
            Console.WriteLine("***** MSI AFTERTERBURNER CONTROL HEADER *****");
            Console.WriteLine(macm.Header.ToString().Replace(";", "\n"));
            Console.WriteLine();
            //print out current MACM GPU Entry values
            for (int i = 0; i < macm.Header.GpuEntryCount; i++)
            {
                Console.WriteLine("***** MSI AFTERTERBURNER GPU " + i + " *****");
                Console.WriteLine(macm.GpuEntries[i].ToString().Replace(";", "\n"));
                Console.WriteLine();
            
            }
        }
        catch (Exception)
        {
            Console.WriteLine("error");
        } }
    public List<GPUModel> getMonitoring()
    {
        List<GPUModel> a = new List<GPUModel>(4);

        for (int i = 0; i < macm.GpuEntries.Length - 1; i++)
        {
            GPUModel gpu = new GPUModel();
            gpu.BestWatt = macm.GpuEntries[i].PowerLimitCur.ToString();
            gpu.BestCoreClock = macm.GpuEntries[i].CoreClockBoostCur.ToString();
            gpu.BestMemoryClock = macm.GpuEntries[i].MemoryClockBoostCur.ToString();
            a.Add(gpu);
        }
        return a;
    }
    public void returnToBestSettings(){
        int i = whichGPUFall_ReturnTheNumberCellOfTheArray();
        List<GPUModel> bestSettingsOfGPU = getListOfBestSettings();
    
            macm.Connect();
            macm.GpuEntries[i].MemoryClockBoostCur = Int32.Parse(bestSettingsOfGPU[i].BestMemoryClock);
            macm.GpuEntries[i].PowerLimitCur = Int32.Parse(bestSettingsOfGPU[i].BestWatt);
            macm.GpuEntries[i].PowerLimitCur = Int32.Parse(bestSettingsOfGPU[i].BestCoreClock);
            macm.CommitChanges(i);
            macm.Disconnect();
        
    }
    public int whichGPUFall_ReturnTheNumberCellOfTheArray()
    {
        int i = 0;
        for ( i = 0; i < macm.GpuEntries.Length - 1; i++)
        {
            if(macm.GpuEntries[i].PowerLimitCur == 100 &&
               macm.GpuEntries[i].CoreClockBoostCur == 0 &&
               macm.GpuEntries[i].MemoryClockBoostCur == 0)
            {
                return i;
            }
        }
        return i;
    }
    }


