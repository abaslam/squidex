﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschränkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squidex.Domain.Apps.Core.ValidateContent.Validators
{
    public sealed class ReferencesValidator : IValidator
    {
        private readonly Guid schemaId;

        public ReferencesValidator(Guid schemaId)
        {
            this.schemaId = schemaId;
        }

        public async Task ValidateAsync(object value, ValidationContext context, AddError addError)
        {
            if (value is ICollection<Guid> contentIds)
            {
                var invalidIds = await context.GetInvalidContentIdsAsync(contentIds, schemaId);

                foreach (var invalidId in invalidIds)
                {
                    addError(context.Path, $"Contains invalid reference '{invalidId}'.");
                }
            }
        }
    }
}
