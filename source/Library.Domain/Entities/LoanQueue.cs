using Library.Domain.Common;
using Library.Domain.DTO.Response;
using Library.Domain.Entities.Bases;
using Library.Domain.Repositories.Specifications;
using Library.Domain.Services.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TDCore.DependencyInjection;
using TDCore.Domain;
using TDCore.Domain.Exceptions;

namespace Library.Domain.Entities
{
    public class LoanQueue : DomainBase<LoanQueue, ILoanQueueRepository<LoanQueue, System.Int64>, System.Int64>
    {

        #region Constructors

        /// <summary>
        /// Name: Book
        /// Description: Empty constructor method.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public LoanQueue()
        {

        }

        /// <summary>
        /// Name: Book
        /// Description: Constructor method that receives as parameter datetimeDeactivate, deactivated, initials, languageId and does a validation, if true, is inserted into the database.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public LoanQueue(AuthenticatedUserDTO user, long bookId)
        {
            BorrowerId = user.UserId;
            CreatedAt = DateTime.Now;
            BookId = bookId;
            StatusQueueLaonId = EnumLoanQueueStatus.Waiting;

            if (Validate(user, true).Result)
            {
                Insert().Wait();
            }
        }

        #endregion

        #region Attributes

        [AttributeDescriptor("Id", true, EnumValueRanges.Positive)]
        public System.Int64 QueueId { get; internal set; }

        [AttributeDescriptor("Created_at", true)]
        public System.DateTime CreatedAt { get; set; }

        [AttributeDescriptor("StatusQueueLaonId", true)]
        public EnumLoanQueueStatus StatusQueueLaonId { get; set; }

        [AttributeDescriptor("BorrowerId", true)]
        public long BorrowerId { get; set; }
        [AttributeDescriptor("LenderId", true)]
        public long LenderId { get; set; }

        [AttributeDescriptor("BookId", true)]
        public long BookId { get; set; }
        [AttributeDescriptor("PreviousId", false)]
        public long PreviousId { get; set; }

        [AttributeDescriptor("ExpectedLoanDate", false)]
        public DateTime? ExpectedLoanDate { get; set; }

        [AttributeDescriptor("updated_at", false)]
        public DateTime? UpdatedAt { get; set; }
        [AttributeDescriptor("UpdatedBy", true)]
        public long UpdatedBy { get; set; }
        public User Borrower { get => GetManyToOneData<User>().Result; }
        public User Lender { get => GetManyToOneData<User>().Result; }
        public Book Book { get => GetManyToOneData<Book>().Result; }
        public LoanQueue Previues { get => GetManyToOneData<LoanQueue>().Result; }





        #endregion

        #region User Code




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

                if (!newRecord && QueueId <= 0)
                {
                    errors.Add(new DomainError("QueueId", await globalization.GetString(lang, "User001")));
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