
using NN.Checklist.Domain.Entities;
using NN.Checklist.Domain.Repositories.Specifications;
using TDCore.Data.SqlServer;

#region Cabeçalho

//TECHDRIVEN DIGITAL
//Generated: 10/2021 12:16:49

#endregion

namespace NN.Checklist.Domain.Repositories
{
    public class DatatypeRepository: RepositoryBase<Datatype, System.Int32>, IDatatypeRepository<Datatype, System.Int32>
    {
        /// <summary>
        /// Name: DatatypeRepository
        /// Description: class constructor
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public DatatypeRepository()
        {
            MapTable("DATATYPES");
            MapPrimaryKey("DatatypeId", "datatype_id",false,0);
            MapColumn("Name", "name", 50);
            MapColumn("Typename", "typename", 50);
        }
        #region User Code

        

        #endregion

    }
}