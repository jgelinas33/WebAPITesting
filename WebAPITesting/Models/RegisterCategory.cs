using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPITesting.Models
{
    /// <summary>
    /// Register categories
    /// Main categories may or may not have sub categories
    /// Main categories do have an entry in sub categories table
    /// with the CategoryID the same as the MainCategoryID
    /// </summary>
    public class RegisterCategory
    {

        /// <summary>
        /// Corresponds to tblRegisterMainCategories.MainCategoryID
        /// </summary>
        public int MainCategoryID { get; set; }

        /// <summary>
        /// Corresponds to tblRegisterSubCategories.CategoryID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Contantenation of tblRegisterMainCategories.Category with
        /// tblRegisterSubCategories.SubCategory.
        /// If this is a main category, will just have the main category name.
        /// If this is a sub category, will have the main category name + : + sub category name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Link from tblRegisterMainCategories to tblCategoryTypes.
        /// Only main categories have a category type.  All sub categories will
        /// have the same category type and react the same
        /// 1 - Income - entries are credits to the account
        /// 2 - Expense - entries are debits to the account
        /// </summary>
        public int CategoryTypeID { get; set; }

        /// <summary>
        /// Corresponds to tblCategoryTypes.CategoryType
        /// </summary>
        public string CategoryType { get; set; }
    }
}