﻿using Lucene.Net.Documents;
using Lucene.Net.QueryParsers.Flexible.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucene.Net.QueryParsers.Flexible.Standard.Config
{
    /// <summary>
    /// This listener listens for every field configuration request and assign a
    /// {@link ConfigurationKeys#DATE_RESOLUTION} to the equivalent {@link FieldConfig} based
    /// on a defined map: fieldName -> {@link Resolution} stored in
    /// {@link ConfigurationKeys#FIELD_DATE_RESOLUTION_MAP}.
    /// </summary>
    /// <seealso cref="ConfigurationKeys#DATE_RESOLUTION"/>
    /// <seealso cref="ConfigurationKeys#FIELD_DATE_RESOLUTION_MAP"/>
    /// <seealso cref="FieldConfig"/>
    /// <seealso cref="IFieldConfigListener"/>
    public class FieldDateResolutionFCListener : IFieldConfigListener
    {
        private QueryConfigHandler config = null;

        public FieldDateResolutionFCListener(QueryConfigHandler config)
        {
            this.config = config;
        }

        
        public virtual void BuildFieldConfig(FieldConfig fieldConfig)
        {
            DateTools.Resolution? dateRes = null;
            IDictionary<string, DateTools.Resolution?> dateResMap = this.config.Get(ConfigurationKeys.FIELD_DATE_RESOLUTION_MAP);

            if (dateResMap != null)
            {
                dateResMap.TryGetValue(fieldConfig.GetField(), out dateRes);

                //dateRes = dateResMap.Get(
                //    fieldConfig.GetField());
            }

            if (dateRes == null)
            {
                dateRes = this.config.Get(ConfigurationKeys.DATE_RESOLUTION);
            }

            if (dateRes != null)
            {
                fieldConfig.Set(ConfigurationKeys.DATE_RESOLUTION, dateRes.Value);
            }
        }
    }
}