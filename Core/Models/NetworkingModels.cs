using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class SubnetInfo
    {
        public string NetworkAddress { get; set; } = string.Empty;
        public string BroadcastAddress { get; set; } = string.Empty;
        public string Netmask { get; set; } = string.Empty;
        public string FirstUsableHost { get; set; } = string.Empty;
        public string LastUsableHost { get; set; } = string.Empty;
        public long TotalHosts { get; set; }
        public long UsableHosts { get; set; }
        public string CidrNotation { get; set; } = string.Empty;
        public string BinaryNetmask { get; set; } = string.Empty;
        public string IpClass { get; set; } = string.Empty;
    }

    public class TraceHop
    {
        public int HopNumber { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string Hostname { get; set; } = string.Empty;
        public long RoundTripTime { get; set; }
        public string Status { get; set; } = "Success"; // Success, TimedOut, Error
        public bool IsSuccess => Status == "Success";
    }

    public class WhoisRecord
    {
        public string Domain { get; set; } = string.Empty;
        public string RawText { get; set; } = string.Empty;
        public string Registrar { get; set; } = string.Empty;
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Error { get; set; } = string.Empty;
    }

    public class MacVendorInfo
    {
        public string MacPrefix { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; // Full address if available
        public string Country { get; set; } = string.Empty;
        public bool Found { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
