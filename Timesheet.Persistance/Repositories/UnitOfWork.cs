using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Timesheet.Core.Interfaces;

namespace Timesheet.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(SqlTransaction transaction)
        {
            _transaction = transaction;
        }

        private SqlTransaction _transaction;     

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (SqlException)
            {
                _transaction.Rollback();
            }
        }
    }
}
