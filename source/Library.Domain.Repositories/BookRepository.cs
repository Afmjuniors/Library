
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
    public class BookRepository : RepositoryBase<Book, System.Int64>, IBookRepository<Book, System.Int64>
    {
        /// <summary>
        /// Name: UserRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public BookRepository()
        {
            MapTable("BOOKS");
            MapPrimaryKey("BookId", "book_id", true, 0);
            MapColumn("Name", "name");
            MapColumn("CreatedAt", "created_at");
            MapColumn("OwnerId", "owner_id");
            MapColumn("Image", "image");
            MapColumn("Genre", "genre");
            MapColumn("Description", "description");
            MapColumn("Url", "url");
            MapColumn("UpdatedAt", "updated_at");
            MapRelationshipManyToOne("Owner", "owner_id", "USERS", "user_id");


        }




        #region User Code




        #endregion
    }
}
