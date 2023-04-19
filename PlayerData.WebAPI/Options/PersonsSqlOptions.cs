﻿namespace PlayerData.WebAPI.Options
{
    public class PersonsSqlOptions
    {
        public string SelectQuery { get; set; }
        public string InsertQuery { get; set; }
        public string IdParameter { get; set; }
        public string FirstNameParameter { get; set; }
        public string LastNameParameter { get; set; }
    }
}
