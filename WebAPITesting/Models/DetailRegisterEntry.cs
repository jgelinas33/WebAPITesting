using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITesting.Models
{
    /// <summary>
    /// tblRegisterDetails
    /// </summary>
    public class DetailRegisterEntry
    {
        public Int32 RegisterDetailID { get; set; }
        public Int32 RegisterMainEntryID { get; set; }
        public Int32 RegisterCategoryID { get; set; }
        public Int32? ProjectID { get; set; }
        public decimal EntryAmount { get; set; }
    }
}