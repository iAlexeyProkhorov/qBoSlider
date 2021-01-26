//Copyright 2020 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Microsoft.EntityFrameworkCore;
using Nop.Core;
using Nop.Data;
using Nop.Data.Extensions;
using Nop.Plugin.Widgets.qBoSlider.Mapping.Builders;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider
{
    public class qBoSliderContext : DbContext, IDbContext
    {
        #region Constructor

        public qBoSliderContext(DbContextOptions<qBoSliderContext> options)
            : base(options)
        {

        }

		#endregion

		#region Utilities

		/// <summary>
		/// Further configuration the model
		/// </summary>
		/// <param name="modelBuilder">Model muilder</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfiguration(new WidgetZoneBuilder());
			modelBuilder.ApplyConfiguration(new SlideBuilder());
            modelBuilder.ApplyConfiguration(new WidgetZoneSlideBuilder());
			base.OnModelCreating(modelBuilder);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Creates a DbSet that can be used to query and save instances of entity
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <returns>A set for the given entity type</returns>
		public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
		{
			return base.Set<TEntity>();
		}

		/// <summary>
		/// Generate a script to create all tables for the current model
		/// </summary>
		/// <returns>A SQL script</returns>
		public virtual string GenerateCreateScript()
		{
			return this.Database.GenerateCreateScript();
		}

        /// <summary>
        /// Creates a LINQ query for the query type based on a raw SQL query
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates a LINQ query for the entity based on a raw SQL query
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="sql">The raw SQL query</param>
		/// <param name="parameters">The values to be assigned to parameters</param>
		/// <returns>An IQueryable representing the raw SQL query</returns>
		public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Executes the given SQL against the database
		/// </summary>
		/// <param name="sql">The SQL to execute</param>
		/// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param>
		/// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param>
		/// <param name="parameters">Parameters to use with the SQL</param>
		/// <returns>The number of rows affected</returns>
		public virtual int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
		{
			using (var transaction = this.Database.BeginTransaction())
			{
				var result = this.Database.ExecuteSqlCommand(sql, parameters);
				transaction.Commit();

				return result;
			}
		}

		/// <summary>
		/// Detach an entity from the context
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="entity">Entity</param>
		public virtual void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Install object context
		/// </summary>
		public void Install()
		{
			//create tables
			this.ExecuteSqlScript(this.GenerateCreateScript());
		}

		/// <summary>
		/// Uninstall object context
		/// </summary>
		public void Uninstall()
		{
            //drop the table
            this.DropPluginTable("Baroque_qBoSlider_WidgetZone_Slide_Mapping");
            this.DropPluginTable("Baroque_qBoSlider_WidgetZone");
			this.DropPluginTable("Baroque_qBoSlider_Slide");
        }

		#endregion
	}
}
