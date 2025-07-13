
using Library.Domain.Entities;
using Library.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;
using System.Threading.Tasks;
using TDCore.Data.Paging;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Repositories
{
    public class LoanQueueRepository : RepositoryBase<LoanQueue, System.Int64>, ILoanQueueRepository<LoanQueue, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public LoanQueueRepository()
        {
            MapTable("LOANS_QUEUES");
            MapPrimaryKey("QueueId", "queue_id", true, 0);
            MapColumn("BorrowerId", "borrower_id");
            MapColumn("LenderId", "lender_id");
            MapColumn("BookId", "book_id");
            MapColumn("PreviousId", "previous_id");
            MapColumn("ExpectedLoanDate", "expected_loan_date");
            MapColumn("CreatedAt", "created_at");
            MapColumn("UpdateAt", "updated_at");
            MapColumn("UpdatedBy", "updated_by");
            MapColumn("StatusQueueLaonId", "status_queue_laon");
            MapRelationshipManyToOne("Borrower", "borrower_id", "USERS", "user_id");
            MapRelationshipManyToOne("Lender", "lender_id", "USERS", "user_id");
            MapRelationshipManyToOne("Book", "book_id", "BOOKS", "book_id");
            MapRelationshipManyToOne("Previues", "previous_id", "LOANS_QUEUES", "queue_id");



        }

        #region User Code


    }

        #endregion
    }
