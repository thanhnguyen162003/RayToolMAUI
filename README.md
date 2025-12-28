# RayTools - Enterprise IP & Network Suite

![License](https://img.shields.io/badge/license-MIT-blue.svg) ![Platform](https://img.shields.io/badge/platform-MAUI%20%7C%20Windows%20%7C%20Android%20%7C%20iOS%20%7C%20Mac-purple) ![Status](https://img.shields.io/badge/status-Active%20Development-green)

**RayTools** is a cross-platform network utility suite composed of essential IP tools essential for network administrators, developers, and power users. Built with **.NET MAUI** and **Blazor Hybrid**, it offers a native performance with the beautiful, responsive UI of the web using **MudBlazor**.

---

## 🚀 Key Features

### 🌐 Networking Suite
A dedicated section for core network utilities:
- **🔍 Port Scanner (Optimized):** 
  - High-performance asynchronous scanning.
  - Supports large ranges (1-65535).
  - Real-time progress streaming and live result logs.
- **🌍 IP Lookup:** 
  - Detailed geolocation data `ip-api.com`.
  - Insights including ISP, AS (Autonomous System), Organization, Mobile/Proxy detection, and Zip codes.
- **📶 Ping Tool:** 
  - Measure round-trip time and packet loss statistics.
  - Configurable packet count and TTL.
- **🔗 DNS Lookup:** 
  - Resolve domain names to A (IPv4) and AAAA (IPv6) records.
- **🆔 My Public IP:** 
  - Instantly detect your external facing IP address.

---

## 🛠️ Technology Stack

- **Framework:** [.NET 10 (MAUI Blazor Hybrid)](https://dotnet.microsoft.com/en-us/apps/maui)
- **UI Component Library:** [MudBlazor](https://mudblazor.com/)
- **Architecture:** Clean Architecture (Core, Application, Infrastructure)(Just my setup).
- **Dependency Injection:** Built-in .NET MAUI DI container.
- **Pattern:** Option pattern.
- **Services:**
  - `HttpClient` for API integrations.
  - `System.Net.Sockets` for low-level networking.
  - `System.Net.NetworkInformation` for Ping/DNS.

---

## 📅 Project Roadmap & Timeline


### Phase 1: Foundation (✅ Completed)
- [x] Application Shell & Navigation
- [x] Basic Auth System
- [x] Core 5 IP Tools (Port Scan, Ping, IP, DNS, Public IP)
- [x] MudBlazor UI Integration
- [x] Optimization of Port Scanner (Streaming/Batching)

### Phase 2: Enhanced Networking (🚧 Next Steps)
We will deepen the capabilities of the Networking section:
- [ ] **Subnet Calculator:** Visual VLSM calculator for network planning.
- [ ] **Trace Route:** Visual hop-by-hop analysis with map visualization.
- [ ] **Whois Domain Lookup:** Detailed registrar, expiry, and owner information for domains.
- [ ] **MAC Address Lookup:** OUI Vendor identification database.

### Phase 3: Features (✨ Future)
Tools designed for the modern cloud-native & security-focused market:

#### ☁️ Cloud & DevOps Tools
- **Cloud Latency Checker:** Real-time latency heatmaps to AWS, Azure, and Google Cloud regions (Trending!).
- **SSL/TLS Security Auditor:** Deep scan of website certificates, encryption strength, and vulnerability checks.
- **HTTP Header Analyzer:** Debug security headers (CORS, CSP, HSTS).

#### 🤖 AI-Powered Diagnostics (AI Agent build)
- **"RayAI" Log Analyzer:** Paste a server error log and get an AI-explained root cause and fix suggestion.
- **Smart Network Troubleshooter:** Automated guided workflow to diagnose connection issues.

#### 🔧 Developer Utilities
- **JWT Decoder:** Debug JSON Web Tokens locally and securely.
- **JSON/XML Formatter:** Beautify and validate data structures.
- **Base64 Converter:** Quick encoding/decoding.

---

## 📦 Getting Started

### Prerequisites
- Visual Studio 2022 (17.8+) with .NET MAUI workload.
- .NET 10.0 SDK.

### Default Credentials
- **Username:** `admin`
- **Password:** `admin123`

---
## 📄 License
This project is licensed under the MIT License.
