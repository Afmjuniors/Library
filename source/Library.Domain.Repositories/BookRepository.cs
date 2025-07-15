
using Library.Domain.Common;
using Library.Domain.DTO;
using Library.Domain.DTO.Paging;
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
            MapColumn("BookStatusId", "book_status_id");
            MapColumn("Author", "author");


            MapRelationshipManyToOne("Owner", "owner_id", "USERS", "user_id");


        }




        #region User Code

        public async Task<PageMessage<BookDTO>> Search(BookPageMessage queryParams)
        {

            try
            {
                var pars = new List<SqlParameter>();
                var select = "SELECT b.* ";
                var from = "from BOOKS b " +
        "left join USERS u on u.user_id = b.owner_id " +
        "left join ORGANIZATIONS_USERS ou on ou.user_id = b.owner_id " +
        "left join LOANS l on l.book_id = b.book_id " +
        "left join organizations o on o.organization_id = ou.organization_id ";

                var where = "WHERE 1=1 ";
                if (queryParams.Filter.UserId.HasValue)
                {
                    where += " AND u.user_id = @userId ";
                    var par = new SqlParameter("userId", System.Data.SqlDbType.BigInt);
                    par.Value = queryParams.Filter.UserId.Value;
                    pars.Add(par);

                }
                if (queryParams.Filter.BookStatus.HasValue)
                {
                    where += " AND b.book_status_id = @bookStarus ";
                    var par = new SqlParameter("bookStarus", System.Data.SqlDbType.Int);
                    par.Value = queryParams.Filter.BookStatus.Value;
                    pars.Add(par);
                }
                if (queryParams.Filter.Genre.HasValue)
                {
                    where += " AND b.genre = @genre ";
                    var par = new SqlParameter("genre", System.Data.SqlDbType.Int);
                    par.Value = queryParams.Filter.Genre.Value;
                    pars.Add(par);
                }
                if (!string.IsNullOrEmpty(queryParams.Filter.Author))
                {
                    where += " AND lower(b.author) like @author ";
                    var par = new SqlParameter("author", System.Data.SqlDbType.VarChar);
                    par.Value = $"%{queryParams.Filter.Author.ToLower()}%";
                    pars.Add(par);
                }
                if (queryParams.Filter.OrganizationId.HasValue && queryParams.Filter.OrganizationId.Value > 0)
                {
                    where += " AND o.organization_id = @organizationId ";
                    var par = new SqlParameter("organizationId", System.Data.SqlDbType.BigInt);
                    par.Value = queryParams.Filter.OrganizationId.Value;
                    pars.Add(par);
                }

                var order = " ORDER BY b.created_at DESC ";

                var res = await Page<BookDTO>(select, from, where, order, pars, queryParams, null);
                return res;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion
    }
}
