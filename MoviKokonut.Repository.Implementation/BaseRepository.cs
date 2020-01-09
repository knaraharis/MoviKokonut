//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Mongo
{
    using System;
    using System.Globalization;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using System.Configuration;
    

    public class BaseRepository
    {
        private readonly string databaseName;
        private readonly string hostNames;
        private readonly bool setWriteConcernToJournal;
        private readonly bool setWriteConcernToWMajority;
        private static DocumentClient client;
        private static DocumentClient Client
            {
            get
                {
                if (client == null)
                    {
                    string endpoint = ConfigurationManager.AppSettings["endpoint"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    client = new DocumentClient(endpointUri, authKey);
                    }

                return client;
                }
            }

        static BaseRepository()
        {
            //var pack = new ConventionPack();
            //pack.Add(new CamelCaseElementNameConvention());
            //ConventionRegistry.Register("CCC", pack, (t) => true);
            //AutoMapperConfig.SetAutoMapperConfiguration();
        }

        public BaseRepository(string hostNames, string databaseName)
            : this(hostNames, databaseName, false)
        {
        }

        public BaseRepository(string hostNames, string databaseName, bool setWriteConcernToJournal)
            : this(hostNames, databaseName, setWriteConcernToJournal, false)
        {
        }

        public BaseRepository(string hostNames, string databaseName, bool setWriteConcernToJournal, bool setWriteConcernToWMajority)
        {
            if (string.IsNullOrWhiteSpace(hostNames))
            {
                throw new ArgumentNullException(hostNames);
            }

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentNullException("databaseName");
            }

            this.hostNames = hostNames;
            this.databaseName = databaseName;
            this.setWriteConcernToJournal = setWriteConcernToJournal;
            this.setWriteConcernToWMajority = setWriteConcernToWMajority;
        }

        //protected MongoDatabase GetDatabase()
        //{
        //    string connectionString = "mongodb://" + this.hostNames;
        //    MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
        //    if (this.setWriteConcernToJournal)
        //    {
        //        settings.WriteConcern.Journal = true;
        //    }

        //    if (this.setWriteConcernToWMajority)
        //    {
        //        settings.WriteConcern = WriteConcern.WMajority;
        //    }

        //    var mongoClient = new MongoClient(settings);
        //    var mongoServer = mongoClient.GetServer();
        //    if (!mongoServer.DatabaseExists(this.databaseName))
        //    {
        //        throw new MongoException(string.Format(CultureInfo.CurrentCulture, Strings.DatabaseDoesNotExist, this.databaseName));
        //    }

        //    var mongoDb = mongoServer.GetDatabase(this.databaseName); 
        //    return mongoDb;
        //}
    }
}
