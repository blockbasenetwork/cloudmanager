using System;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class DeploymentConfiguration
    {
        public String Account { get; set; }
        public String PrivateKey { get; set; }
        public String PublicKey { get; set; }

        //ConnectionString
        public String MongoCS { get; set; }

        public String PostgresHost { get; set; }
        public String PostgresUser { get; set; }
        public String PostgresPort { get; set; }
        public String PostgresPassword { get; set; }

        public String DBPrefix{ get; set; }
    }
}
