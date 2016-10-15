using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITesting.Models
{
    /// <summary>
    /// tblRegister
    /// </summary>
    public class MainRegisterEntry
    {
        /// <summary>
        /// tblRegister.EntryID
        /// </summary>
        public int EntryID { get; set; }

        /// <summary>
        /// tblRegister.EntryDate
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// tblAccounts.AccountID => tblRegister.AccountID
        /// </summary>
        public Int32 AccountID { get; set; }

        /// <summary>
        /// tblPayees.Payee
        /// </summary>
        public string Payee { get; set; }


        private string _entryDesc = "";
        /// <summary>
        /// tblRegister.EntryDesc
        /// If set as null, converts to empty string for easier display
        /// </summary>
        public string EntryDesc
        {
            get { return _entryDesc; }
            set {_entryDesc = value ?? "";}
        }


        private string _checkNumber = "";
        /// <summary>
        /// tblRegister.CheckNumber
        /// Will indicate the type of transaction
        /// ie. DEBIT/CREDIT/ATM/Check Number
        /// </summary>
        public string CheckNumber
        {
            get { return _checkNumber; }
            set { _checkNumber = value ?? ""; }
        }

        /// <summary>
        /// Summed amount of all detail entries
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Entry details from tblReisterDetails
        /// </summary>
        public List<DetailRegisterEntry> EntryDetails;
    }
}