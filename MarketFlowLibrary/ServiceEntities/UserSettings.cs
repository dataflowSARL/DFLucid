using System;
using SQLite;

namespace MarketFlowLibrary
{
    public class UserSettings : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        [SQLite.Ignore]
        public string Extra { get; set; }
    }
}
