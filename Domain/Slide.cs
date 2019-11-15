using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using System;

namespace Nop.Plugin.Widgets.qBoSlider.Domain
{
    public partial class Slide : BaseEntity, ILocalizedEntity, IStoreMappingSupported, IAclSupported
    {
        /// <summary>
        /// Slide picture id
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Slide HTML content
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Slide href
        /// </summary>
        public string HyperlinkAddress { get; set; }

        ///version 1.0.5
        /// <summary>
        /// Start publication date
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        ///version 1.0.5
        /// <summary>
        /// End publication date
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Slide display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets published parameter
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Slide limited to store
        /// </summary>
        public bool LimitedToStores { get; set; }

        ///1.0.5
        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }
    }
}
