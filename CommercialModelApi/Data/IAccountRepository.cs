using System;
using System.Collections.Generic;
using CommercialModelApi.Model;

namespace CommercialModelApi.Data
{

    public interface IAccountRepository
    {
        IEnumerable<Account> ListAccounts();
        void AddAccount(Account account);
        void DeleteAllAccounts();
    }
}