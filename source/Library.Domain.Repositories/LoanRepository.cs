
using Library.Domain.Entities;
using Library.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories
{
    public class LoanRepository : RepositoryBase<Loan, System.Int64>, ILoanRepository<Loan, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public LoanRepository()
        {
            MapTable("LOANS");
            MapPrimaryKey("LoanId", "loan_id", true,0);
            MapColumn("BorrowerId", "borrower_id");
            MapColumn("CreatedAt", "created_at");
            MapColumn("LenderId", "lender_id");
            MapColumn("BookId", "book_id");
            MapColumn("LoanStatusId", "loan_status_id");
            MapColumn("DueDate", "due_date");
            MapColumn("ReceivedDate", "received_date");
            MapColumn("ReturnedDate", "returned_date");
            MapColumn("CreatedBy", "created_by");
            MapColumn("UpdateAt", "updated_at");
            MapColumn("UpdatedBy", "updated_by");
            MapRelationshipManyToOne("Borrower", "borrower_id", "USERS", "user_id");
            MapRelationshipManyToOne("Lender", "lender_id", "USERS", "user_id");
            MapRelationshipManyToOne("Book", "book_id", "BOOKS", "book_id");




        }



    #region User Code


}

        #endregion
    
}
