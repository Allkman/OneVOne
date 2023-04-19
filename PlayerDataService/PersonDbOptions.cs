using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerDataService
{
    public class PersonDbOptions
    {
        public string NbaPlayersUrl { get; set; }
        public string PaginationSelectTagTitle { get; set; }
        public string OptionTagName { get; set; }
        public string PlayerNameDivClassName { get; set; }
        public string PlayerFirstNameDivClassName { get; set; }
        public string PlayerLastNameDivClassName { get; set; }
    }
}
