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

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Widgets.qBoSlider
{
    public class EfStartUpTask : INopStartup
	{
		/// <summary>
		/// Add and configure any of the middleware
		/// </summary>
		/// <param name="services">Collection of service descriptors</param>
		/// <param name="configuration">Configuration of the application</param>
		public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			//add object context
			services.AddDbContext<qBoSliderContext>(optionsBuilder =>
			{
				optionsBuilder.UseSqlServerWithLazyLoading(services);
			});
		}

		/// <summary>
		/// Configure the using of added middleware
		/// </summary>
		/// <param name="application">Builder for configuring an application's request pipeline</param>
		public void Configure(IApplicationBuilder application)
		{
		}

		/// <summary>
		/// Gets order of this startup configuration implementation
		/// </summary>
		public int Order => 11;
	}
}
