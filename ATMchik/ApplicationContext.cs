using ATMchik.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATMchik
{
    public class ApplicationContext : DbContext
    {
        public DbSet<BankAccount> BankAccounts { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public void AddBankAccountAsync(BankAccount bankAccount)
        {
            BankAccounts.Add(bankAccount);
            SaveChangesAsync();
        }

        public void ChangeValue(BankAccount bankAccount, decimal value)
        {
            decimal preOperationResult = bankAccount.Value + value;

            if (preOperationResult < 0)
            {
                throw new Exception("Not enough money in the account");
            }
            else
            {
                bankAccount.Value += value;
                SaveChangesAsync();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=atmchik;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
