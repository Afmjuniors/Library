
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
    public class RatingRepository : RepositoryBase<Rating, System.Int64>, IRatingRepository<Rating, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public RatingRepository()
        {
            MapTable("RATINGS");
            MapPrimaryKey("RatingId", "rating_id", true,0);
            MapColumn("UserId", "user_id");
            MapColumn("CreatedAt", "created_at");
            MapColumn("BookId", "book_id");
            MapColumn("Rate", "rate");
            MapColumn("Comments", "comments");
            MapColumn("UpdatedAt", "updated_at");


        }


        #region User Code


    }

        #endregion
    }
