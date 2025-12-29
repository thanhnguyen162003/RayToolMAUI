using System;
using System.Net;
using System.Net.Sockets;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class SubnetCalculatorService : ISubnetCalculator
    {
        public bool IsValidIp(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }

        public SubnetInfo Calculate(string ipAddress, int cidr)
        {
            if (!IPAddress.TryParse(ipAddress, out var ip))
                throw new ArgumentException("Invalid IP address");

            if (cidr < 0 || cidr > 32)
                throw new ArgumentException("CIDR must be between 0 and 32");

            byte[] ipBytes = ip.GetAddressBytes();
            uint ipUint = BitConverter.ToUInt32(ipBytes.Reverse().ToArray(), 0);

            uint maskUint = 0xffffffff << (32 - cidr);
            byte[] maskBytes = BitConverter.GetBytes(maskUint).Reverse().ToArray();
            
            uint networkUint = ipUint & maskUint;
            byte[] networkBytes = BitConverter.GetBytes(networkUint).Reverse().ToArray();

            uint broadcastUint = networkUint | ~maskUint;
            byte[] broadcastBytes = BitConverter.GetBytes(broadcastUint).Reverse().ToArray();

            long totalHosts = (long)Math.Pow(2, 32 - cidr);
            long usableHosts = totalHosts > 2 ? totalHosts - 2 : 0;

            var info = new SubnetInfo
            {
                NetworkAddress = new IPAddress(networkBytes).ToString(),
                BroadcastAddress = new IPAddress(broadcastBytes).ToString(),
                Netmask = new IPAddress(maskBytes).ToString(),
                TotalHosts = totalHosts,
                UsableHosts = usableHosts,
                CidrNotation = $"{ipAddress}/{cidr}",
                BinaryNetmask = Convert.ToString(maskUint, 2).PadLeft(32, '0')
            };

            // First Usable
            if (usableHosts > 0)
            {
                byte[] firstBytes = BitConverter.GetBytes(networkUint + 1).Reverse().ToArray();
                info.FirstUsableHost = new IPAddress(firstBytes).ToString();

                byte[] lastBytes = BitConverter.GetBytes(broadcastUint - 1).Reverse().ToArray();
                info.LastUsableHost = new IPAddress(lastBytes).ToString();
            }
            else
            {
                info.FirstUsableHost = "N/A";
                info.LastUsableHost = "N/A";
            }
            
            // Class
            int firstByte = ipBytes[0];
            if (firstByte < 128) info.IpClass = "A";
            else if (firstByte < 192) info.IpClass = "B";
            else if (firstByte < 224) info.IpClass = "C";
            else if (firstByte < 240) info.IpClass = "D (Multicast)";
            else info.IpClass = "E (Experimental)";

            return info;
        }
    }
}
