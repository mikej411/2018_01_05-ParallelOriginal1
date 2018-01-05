// I had to comment this class out because it uses C#6.0 syntax (nameof). We are using TFS 2013 for build. The "nameof" is C#6.0 syntax, and TFS 2013 does not build with C#6.0 syntax

//using System;
//using System.Data.Common;
//using System.Data.SqlClient;

//namespace Browser.Core.Framework
//{
//    /// <summary>
//    /// A SQL Server implementation of the DataAccessProvider.
//    /// </summary>
//    public class SqlServerDataAccessProvider : DataAccessProvider
//    {
//        /// <summary>
//        /// Constructor.
//        /// </summary>
//        /// <param name="connectionString">The string used to connect to the data source.</param>
//        public SqlServerDataAccessProvider(string connectionString) : base(connectionString) { }

//        /// <summary>
//        /// Using the connection string, connect to a data source.
//        /// </summary>
//        /// <returns>A connection to the data source.</returns>
//        public override DbConnection CreateConnection()
//        {
//            return new SqlConnection(ConnectionString);
//        }

//        /// <summary>
//        /// Create a DataAdapter for the data source.
//        /// </summary>
//        /// <returns>A DataAdapter for the data source.</returns>
//        public override DbDataAdapter CreateAdapter()
//        {
//            return new SqlDataAdapter();
//        }

//        /// <summary>
//        /// Given a data adapter, create a single table command builder.
//        /// </summary>
//        /// <param name="adapter"></param>
//        /// <returns>A commmand builder.</returns>
//        public override DbCommandBuilder CreateCommandBuilder(DbDataAdapter adapter)
//        {
//            if (adapter == null) throw new ArgumentNullException(nameof(adapter));

//            return new SqlCommandBuilder((SqlDataAdapter)adapter);
//        }
//    }
//}
