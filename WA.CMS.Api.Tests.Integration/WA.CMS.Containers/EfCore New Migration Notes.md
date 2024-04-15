### Ef Core notes about setting up mock environments

### Tech reference

- Entity Framework Code-First : https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database

### Standard operating procedure

1. In PackageManager window, select .Data project as "Default project" with dropdown.
1. EntityFrameworkCore\add-migration initial_OutboundStaging -o Migrations/OutboundStaging -Context OutboundContext
1. EntityFrameworkCore\add-migration initial_SubscriberContext -o Migrations/Subscribers -Context SubscriberContext

### Example docker connection string

- Server=172.20.192.1,1433;User Id=sa;Password=ayeCiEs2k24!;TrustServerCertificate=True;

You will need to replace `172.20.192.1` with your own ip address.  From Docker's perspective, this is to be Host's Ip4 number.

1. Open a `cmd` terminal
2. Run `ipconfig`

Example Result:

```
Ethernet adapter vEthernet (WSL):

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe80::2b5:4b64:2a75:3de8%64
   IPv4 Address. . . . . . . . . . . : 172.20.192.1
   Subnet Mask . . . . . . . . . . . : 255.255.240.0
   Default Gateway . . . . . . . . . :
```

3. In this case, it was labelled as the "Ethernet adapter vEthernet (WSL)" adapter and the value is `172.20.192.1`
- WSL stands for "Windows Subsystem for Linux" and is the preferred tech to use Docker on Windows.  
- You may be using the Hyper-V alternative, so the label for the adapter could be different.
