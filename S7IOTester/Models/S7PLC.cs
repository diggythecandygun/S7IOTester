using System;
using System.Collections.Generic;
using System.Text;
using S7.Net;
using System.Net.NetworkInformation;

namespace S7IOTester.Models
{
    class S7PLC
    {

        public S7PLC(string CPUType, string IPAddress)
        {
            if (CPUType == "S7-300")
            {
                this.CPUType = CpuType.S7300;
                this.Rack = 0;
                this.Slot = 2;
            }
            else if (CPUType == "S7-400")
            {
                this.CPUType = CpuType.S7400;
                this.Rack = 0;
                this.Slot = 2;
            }
            else if (CPUType == "S7-1200")
            {
                this.CPUType = CpuType.S71200;
                this.Rack = 0;
                this.Slot = 1;
            }
            else if (CPUType == "S7-1500")
            {
                this.CPUType = CpuType.S71500;
                this.Rack = 0;
                this.Slot = 1;
            }

            this.IPAddress = IPAddress;

            plc = new Plc(this.CPUType, this.IPAddress, this.Rack, this.Slot);
            plc.ReadTimeout = 3000;
            plc.WriteTimeout = 3000;
        }

        public CpuType CPUType { get; set; }
        public string IPAddress { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }
        private Plc plc { get; set; }



        public short ConnDisconn()
        {
            if (plc.IsConnected)
            {
                Disconnect();
                return 0;
            }

            else
            {
                if (Connect()) return 1;
                else return 3;
            }
                
        }

        public bool Connect()
        {
            try
            {
                if (PingDevice())
                {
                    plc.Open();
                    return true;
                }
                else return false;

            }
            catch (PlcException e)
            {
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                plc.Close();
            }
            catch (PlcException)
            {

            }
            
        }

        private bool PingDevice()
        {
            int timeout = 100;   //in ms
            Ping p = new Ping();
            try
            {
                PingReply rep = p.Send(IPAddress, timeout);
                if (rep.Status == IPStatus.Success) return true;
                else return false;
            }

            catch (Exception ex)
            {
                return false;
            }


        }

        public byte[] ReadByte(string byteoffsetstr)
        {
            byte[] ByteRead = new byte[] { };

            try
            {
                int byteoffset = Int16.Parse(byteoffsetstr);
                ByteRead = plc.ReadBytes(DataType.Input, 0, byteoffset, 1);
                return ByteRead;
            }
            catch
            {
                return ByteRead;
            }

            
        }

    }
}
