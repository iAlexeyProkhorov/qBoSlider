using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using System;

namespace Nop.Plugin.Widgets.qBoSlider.Domain
{
    /// <summary>
    /// Represents slide entity
    /// </summary>
    public partial class Slide : BaseEntity, ILocalizedEntity, IStoreMappingSupported, IAclSupported
    {
        /// <summary>
        /// Gets or sets slide name.
        /// Using only for administrator needs
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets slide picture id
        /// </summary>
        public int? PictureId { get; set; }

        /// <summary>
        /// Gets or sets slide HTML content
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets slide href
        /// </summary>
        public string HyperlinkAddress { get; set; }

        /// <summary>
        /// Gets or sets start publication date.
        /// Available from version 1.0.5
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets end publication date.
        /// Available from version 1.0.5
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets published parameter
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets slide store limitations
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL.
        /// Available from version 1.0.5
        /// </summary>
        public bool SubjectToAcl { get; set; }
    }
}
