using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbBuddy
{
    public class ConnectionStrings
    {
        /// <summary>
        /// User's complete connection string to their Local Db.
        /// </summary>
        public string LocalDbPath { get; set; }

        /// <summary>
        /// User's complete connection string to their Remote Db.
        /// </summary>
        public string RemoteDbPath { get; set; }

        /// <summary>
        /// Path to the User's web.config file.
        /// </summary>
        public string ConfigPath { get; set; }

        /// <summary>
        /// String indicating the current active Db.
        /// </summary>
        public string CurrentDb { get; set; }
    }
}
