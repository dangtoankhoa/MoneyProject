//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.Adapter.AutoMapperTypeAdapterFactory.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>Friday, June 02, 2017 21:00:00 PM</Date>
// Licensed under the Apache License, Version 2.0,
// You may not use this file except in compliance with one of the Licenses.
//
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an 'AS IS' BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </Copyright>
//-----------------------------------------------------------------------

using Infrastructure.CrossCutting.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.NetFramework.Adapter
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-June-02</para>
    /// <para>Usage: AutoMapperTypeAdapterFactory is a concrete class implement contract ITypeAdapterFactory using for create instance of AutoMapperTypeAdapter</para>
    /// <remark>
    ///  This concrete class implement using for AutoMapper tech
    /// </remark>
    /// </summary>
    public class AutoMapperTypeAdapterFactory : ITypeAdapterFactory
    {
        /// <summary>
        /// Create an instance of AutoMapperTypeAdapter
        /// </summary>
        /// <returns>AutoMapperTypeAdapter instance</returns>
        public ITypeAdapter Create()
        {
            ITypeAdapter autoMapperTypeAdapter = new AutoMapperTypeAdapter();
            return autoMapperTypeAdapter;
        }

        private static bool _IsConfigured = false;
        public AutoMapperTypeAdapterFactory()
        {
            if (!AutoMapperTypeAdapterFactory._IsConfigured)
            {
                var profiles = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where
                    (t =>
                         t.BaseType == typeof(AutoMapper.Profile)
                     && t.Assembly != typeof(AutoMapper.Mapper).Assembly
                    );
                AutoMapper.Mapper.Initialize(cfg =>
                {
                    foreach (var item in profiles)
                    {
                        try
                        {
                            cfg.AddProfile(Activator.CreateInstance(item) as AutoMapper.Profile);
                        }
                        catch (Exception error)
                        {
                            Infrastructure.CrossCutting.Logging.LoggerFactory.Get().LogError(error.Message, error);
                        }
                    }
                    //cfg.ForAllMaps((map, exp) => exp.ForAllOtherMembers(opt => opt.Ignore()));
                    cfg.CreateMissingTypeMaps = true;
                });
                AutoMapperTypeAdapterFactory._IsConfigured = true;
            }
        }
    }

}
