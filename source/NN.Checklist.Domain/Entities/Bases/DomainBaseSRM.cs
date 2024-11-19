using System;
using System.Linq;
using TDCore.DependencyInjection;
using TDCore.Domain;

namespace NN.Checklist.Domain.Entities.Bases
{
    public abstract class DomainBaseSRM<T, R, Key> : DomainBase<T,R,Key> where R : class, IRepositoryBase<T, Key>
    {
        public override string GetDifferences(T objNew, T objOld)
        {
            var globalization = ObjectFactory.GetSingleton<Services.Specifications.IGlobalizationService>();
            string description = "";
            string and = "";
            var attributes = ListDatabaseAttributes();

            foreach (var item in attributes)
            {
                var valueNew = item.PropertyInfo.GetValue(objNew);
                var valueOld = item.PropertyInfo.GetValue(objOld);
                string strValueNew = "-";
                string strValueOld = "-";

                if ((valueNew == null && valueOld != null) ||
                    (valueNew != null && valueOld == null) ||
                    (valueNew != null && valueOld != null && !valueNew.Equals(valueOld)))
                {
                    if (valueNew != null)
                    {
                        strValueNew = valueNew.ToString();
                    }

                    if (valueOld != null)
                    {
                        strValueOld = valueOld.ToString();
                    }

                    if (item.PropertyInfo.PropertyType == typeof(DateTime) || item.PropertyInfo.PropertyType == typeof(DateTime?))
                    {
                        string format = "yyyy-MM-dd HH:mm:ss";
                        if (valueNew != null)
                        {
                            strValueNew = ((DateTime)valueNew).ToString(format);
                        }
                        if (valueOld != null)
                        {
                            strValueOld = ((DateTime)valueOld).ToString(format);
                        }
                    }
                    description += and + globalization.GetString(globalization.DefaultLanguage, "ObjectChanged", new string[] { item.FriendlyName, strValueOld, strValueNew });
                    and = " | ";
                }
            }

            if (!string.IsNullOrEmpty(description))
            {
                var attrPKs = attributes.Where(x => x.IsPrimaryKey == true);
                var text = "";
                and = "";
                foreach (var pk in attrPKs)
                {
                    text = and + pk.FriendlyName + ": " + pk.PropertyInfo.GetValue(objNew).ToString();
                    and = " | ";
                }
                description = text + " " + globalization.GetString(globalization.DefaultLanguage, "Changes") + " > " + description;
            }

            return description;
        }
    }
}
