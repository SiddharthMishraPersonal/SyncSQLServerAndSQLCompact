using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace ProvisionServerWithFilteredScope
{
    class Program
    {
        static void Main(string[] args)
        {
            //create connection to the server DB
            SqlConnection serverConn = new SqlConnection("Data Source=localhost; Initial Catalog=SyncDB; Integrated Security=True");

            // define the OrdersScope-NC filtered scope 
            // this scope filters records in the Orders table with OriginState set to NC"
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("OrdersScope-NC");

            // get the description of the Orders table and add it to the scope
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable("Orders", serverConn);
            scopeDesc.Tables.Add(tableDesc);

            // create server provisioning object
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);

            // no need to create the Orders table since it already exists, 
            // so use the Skip parameter
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            // set the filter column on the Orders table to OriginState
            serverProvision.Tables["Orders"].AddFilterColumn("OriginState");

            // set the filter value to NC
            serverProvision.Tables["Orders"].FilterClause = "[side].[OriginState] = 'NC'";

            // start the provisioning process
            serverProvision.Apply();
        }
    }
}
