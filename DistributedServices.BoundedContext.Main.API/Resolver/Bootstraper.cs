//-----------------------------------------------------------------------
// <Copyright file="DistributedServices.BoundedContext.Main.API.Resolver.Bootstraper.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-Oct-20 12:23:08 AM</Date>
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


using Domain.Seedwork;
using Infrastructure.CrossCutting.Adapter;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Adapter;
using Infrastructure.Data.Seedwork;
using Infrastructure.Data.Seedwork.EntityFramework;
using ezRich.Infrastructure.Data.BoundedContext.Main.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace DistributedServices.BoundedContext.Main.API.Resolver
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-Oct-20 12:23:08 AM</para>
    /// <para>Usage: Bootstraper is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class Bootstraper
    {
        #region Private fields
        protected IUnityContainer Container;
        #endregion

        #region Property Members
        #endregion

        #region Constructors
        public Bootstraper(IUnityContainer container)
        {
            this.Container = container;
        }
        #endregion

        #region Public operation logical methods
        public void Initialize()
        {
            this.RegisterServices();
        }
        #endregion

        #region Private utilities methods
        /// <summary>
        /// Register services
        /// </summary>
        private void RegisterServices()
        {
            // Register application common services
            this.Container.RegisterInstance<ILogger>(Infrastructure.CrossCutting.Logging.LoggerFactory.Get());

            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["MainBoundContext"].ToString();
            this.Container.RegisterType<IQueryableUnitOfWork, MainBoundContext>(new InjectionConstructor(conStr));
            this.Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            // Register TypAdapterFactory
            this.Container.RegisterType<ITypeAdapterFactory, AutoMapperTypeAdapterFactory>(new ContainerControlledLifetimeManager());
            var typeAdapterFactory = this.Container.Resolve<ITypeAdapterFactory>();
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);
        }
        #endregion
    }
}
