using Library.Domain.Common;
using Library.Domain.DTO.Response;
using Library.Domain.Entities.Bases;
using Library.Domain.Repositories.Specifications;
using iTextSharp.text;
using Library.Domain.DTO.Response;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;
using TDCore.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 14/2021 15:09:37

#endregion

namespace Library.Domain.Entities
{
    public class Book : DomainBase<Book, IBookRepository<Book, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: Book
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Book()
        {

        }

        /// <summary>
        /// Name: Book
        /// Description: Constructor method that receives as parameter datetimeDeactivate, deactivated, initials, languageId and does a validation, if true, is inserted into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Book(AuthenticatedUserDTO user, string name, string description, string author, EnumGenre? genre, string utl, string image)
        {
            OwnerId = user.UserId;
            Name = name;
            Description = description;
            Author = author;
            Genre = genre;
            Url = utl;
            Image = image;
            CreatedAt = DateTime.Now;
            BookStatusId = EnumBookStatus.Available;
            try
            {

                if (Validate(user, true).Result)
                {
                    Insert().Wait();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 BookId { get; internal set; }

        [AttributeDescriptor("Created_at", true)]
        public System.DateTime CreatedAt { get; set; }

        [AttributeDescriptor("book_status_id", true)]
        public EnumBookStatus BookStatusId { get; set; }
        [AttributeDescriptor("name", true)]
        public string Name { get; set; }
        [AttributeDescriptor("ownerId", true)]
        public long OwnerId { get; set; }
        [AttributeDescriptor("genre", false)]
        public EnumGenre? Genre { get; set; }
        [AttributeDescriptor("description", false)]
        public string Description { get; set; }
        [AttributeDescriptor("Author", false)]
        public string Author { get; set; }

        [AttributeDescriptor("Url", false)]
        public string Url { get; set; }
        [AttributeDescriptor("Image", false)]
        public string Image { get; set; }
        [AttributeDescriptor("updated_at", false)]
        public DateTime? UpdatedAt { get; set; }
        public User? Owner { get => GetManyToOneData<User>().Result; }




        #endregion

        #region User Code

        public async Task Update(string name, EnumGenre? genre, string author, string description, EnumBookStatus? bookStatus, string url, string image)
        {
            try
            {
                var edited = string.Empty;
                if (bookStatus.HasValue && bookStatus != BookStatusId)
                {
                    edited += $"Book Status: {BookStatusId} - {bookStatus.Value}";
                    BookStatusId = bookStatus.Value;

                }
                if (!string.IsNullOrEmpty(name) && name != Name)
                {
                    edited += $"Name: {Name} - {name}";
                    Name = name;

                }
                if (!string.IsNullOrEmpty(description) && name != Description)
                {
                    edited += $"Description: {Description} - {description}";
                    Description = description;

                }
                if (!string.IsNullOrEmpty(image) && image != Image)
                {
                    edited += $"Image changed";
                    Image = image;

                }
                if (!string.IsNullOrEmpty(url) && url != Url)
                {
                    edited += $"Url: {Url} - {url}";
                    Url = url;

                }
                if (!string.IsNullOrEmpty(author) && author != Author)
                {
                    edited += $"Author: {Author} - {author}";
                    Author = author;

                }
                if (genre.HasValue && genre != Genre)
                {
                    edited += $"Genre: {Genre} - {genre}";
                    Genre = genre;

                }
                if (!string.IsNullOrEmpty(edited))
                {
                    UpdatedAt = DateTime.Now;
                    await Update();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("BookNotUpdated", ex);
            }


        }


        /// <summary>
        /// Name: Validate
        /// Description: Method that receives as parameter user, newRecord and validates if user is different from null, if yes, lang receives user.CultureInfo.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public async Task<bool> Validate(AuthenticatedUserDTO user, bool newRecord)
        {
            try
            {
                base.Validate(newRecord);

                var globalization = ObjectFactory.GetSingleton<IGlobalizationService>();
                string lang = null;

                if (user != null)
                {
                    lang = user.CultureInfo;
                }

                List<DomainError> errors = new List<DomainError>();

                if (!newRecord && BookId <= 0)
                {
                    errors.Add(new DomainError("BookId", await globalization.GetString(lang, "User001")));
                }

                if (errors.Count > 0)
                {
                    throw new DomainException(await globalization.GetString(lang, "DataDomainError"), errors);
                }

                return true;
            }
            catch (DomainException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion
    }
}