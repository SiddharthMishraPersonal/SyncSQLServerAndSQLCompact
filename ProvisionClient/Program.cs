﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace ProvisionClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //UpgradeDatabasewithCaseSensitive();

            // create a connection to the SyncCompactDB database
            SqlCeConnection clientConn = new SqlCeConnection(@"Data Source='C:\VisualStudioProjects\SyncSQLServerAndSQLCompact\ClientSQLCompactDB\SyncCompactDB.sdf'");

            // create a connection to the SyncDB server database
            SqlConnection serverConn = new SqlConnection("Data Source=localhost; Initial Catalog=SyncDB; Integrated Security=True");

            // get the description of ProductsScope from the SyncDB server database
            DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("ProductsScope", serverConn);

            // create CE provisioning object based on the ProductsScope
            SqlCeSyncScopeProvisioning clientProvision = new SqlCeSyncScopeProvisioning(clientConn, scopeDesc);

            // starts the provisioning process
            clientProvision.Apply();
        }
    }
}
