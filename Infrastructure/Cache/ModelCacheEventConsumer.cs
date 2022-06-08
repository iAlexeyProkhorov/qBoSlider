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

using Nop.Core.Caching;
using Nop.Core.Domain.Configuration;
using Nop.Core.Events;
using Nop.Services.Events;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Infrastructure.Cache
{
    /// <summary>
    /// Model cache event consumer (used for caching of presentation layer models)
    /// </summary>
    public partial class ModelCacheEventConsumer: 
        IConsumer<EntityInsertedEvent<Setting>>,
        IConsumer<EntityUpdatedEvent<Setting>>,
        IConsumer<EntityDeletedEvent<Setting>>
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : working language id
        /// {1} : working store id
        /// {2} : short date. Cache key will be actual only one day
        /// {3} : customer roles(coma separated)
        /// </remarks>
        public static CacheKey PICTURE_URL_MODEL_KEY = new CacheKey("qbo-slider-publicinfo-{0}-{1}-{2}-{3}-{4}");
        public const string PICTURE_URL_PATTERN_KEY = "Nop.plugins.widgets.qBoSlider";

        private readonly IStaticCacheManager _staticCacheManager;

        public ModelCacheEventConsumer(IStaticCacheManager staticCacheManager)
        {
            //TODO inject static cache manager using constructor
            this._staticCacheManager = staticCacheManager;
        }

        public async Task HandleEventAsync(EntityInsertedEvent<Setting> eventMessage)
        {
            await _staticCacheManager.RemoveByPrefixAsync(PICTURE_URL_PATTERN_KEY);
        }
        public async Task HandleEventAsync(EntityUpdatedEvent<Setting> eventMessage)
        {
            await _staticCacheManager.RemoveByPrefixAsync(PICTURE_URL_PATTERN_KEY);
        }
        public async Task HandleEventAsync(EntityDeletedEvent<Setting> eventMessage)
        {
            await _staticCacheManager.RemoveByPrefixAsync(PICTURE_URL_PATTERN_KEY);
        }
    }
}
