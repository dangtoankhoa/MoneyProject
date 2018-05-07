//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.Validator.DataAnnotationsEntityValidator.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-03 11:47:20 PM</Date>
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


using Infrastructure.CrossCutting.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.NetFramework.Validator
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-03 11:47:20 PM</para>
    /// <para>Usage: DataAnnotationsEntityValidator is a concrete class of IEntityValidator using for valid the model base on its property annotation</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public class DataAnnotationsEntityValidator : IEntityValidator
    {
        #region Private Methods

        /// <summary>
        /// Get erros if object implement IValidatableObject
        /// </summary>
        /// <typeparam name="TEntity">The typeof entity</typeparam>
        /// <param name="item">The item to validate</param>
        /// <param name="errors">A collection of current errors</param>
        private Dictionary<string,PropertyValidationResult> SetValidatableObjectErrors<TEntity>(TEntity item) where TEntity : class
        {
            Dictionary<string, PropertyValidationResult> errors = new Dictionary<string, PropertyValidationResult>();
            if (typeof(IValidatableObject).IsAssignableFrom(typeof(TEntity)))
            {
                var validationContext = new ValidationContext(item, null, null);
                var validationResults = ((IValidatableObject)item).Validate(validationContext);                

                foreach(ValidationResult result in validationResults)
                {
                    if(!string.IsNullOrWhiteSpace(result.ErrorMessage) && (result.MemberNames == null || !result.MemberNames.Any()))
                    {
                        if(!errors.ContainsKey("Common"))
                        {
                            errors["Common"] = new PropertyValidationResult() { PropertyName = "Common" };
                        }

                        errors["Common"].ErrorMessages.Add(result.ErrorMessage);
                    }
                    else
                    {
                        foreach(string propertyName in result.MemberNames)
                        {
                            if (!errors.ContainsKey(propertyName))
                            {
                                errors[propertyName] = new PropertyValidationResult() { PropertyName = propertyName };
                            }

                            errors[propertyName].ErrorMessages.Add(result.ErrorMessage);
                        }
                    }
                }                
            }
            return errors;
        }

        /// <summary>
        /// Get errors on ValidationAttribute
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">The entity to validate</param>
        /// <param name="errors">A collection of current errors</param>
        private Dictionary<string, PropertyValidationResult> SetValidationAttributeErrors<TEntity>(TEntity item) where TEntity : class
        {
            var results = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
                         from attribute in property.Attributes.OfType<ValidationAttribute>()
                         where !attribute.IsValid(property.GetValue(item))
                         select new { propetyName = property.Name, errorMessage = attribute.ErrorMessage };
            var errors = new Dictionary<string, PropertyValidationResult>();

            if (results != null && results.Any())
            {
                foreach(var result in results)
                {
                    if(!errors.ContainsKey(result.propetyName))
                    {
                        errors[result.propetyName] = new PropertyValidationResult() { PropertyName = result.propetyName };
                    }
                    errors[result.propetyName].ErrorMessages.Add(result.errorMessage);
                }
            }
            return errors;
        }

        /// <summary>
        /// Get the validation error list of entity
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">The validation object instance</param>
        /// <returns>A list of errors</returns>
        private Dictionary<string, PropertyValidationResult> GetEntityErrors<TEntity>(TEntity item) where TEntity : class
        {
            Dictionary<string, PropertyValidationResult> objectErrors = this.SetValidatableObjectErrors(item);
            Dictionary<string, PropertyValidationResult> attributeErrors = this.SetValidationAttributeErrors(item);
            Dictionary<string, PropertyValidationResult> entityErrors = new Dictionary<string, PropertyValidationResult>();
            foreach (var error in attributeErrors)
            {
                entityErrors[error.Key] = error.Value;
            }

            foreach (var error in objectErrors)
            {
                if (!entityErrors.ContainsKey(error.Key))
                {
                    entityErrors[error.Key] = error.Value;
                }
                else
                {
                    entityErrors[error.Key].ErrorMessages.UnionWith(error.Value.ErrorMessages);
                }
            }

            return entityErrors;
        }
        #endregion

        #region IEntityValidator Members


        /// <summary>
        /// <see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></typeparam>
        /// <param name="item"><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></param>
        /// <returns><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></returns>
        public bool IsValid<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
            {
                return false;
            }
            
            Dictionary<string, PropertyValidationResult> entityErrors = this.GetEntityErrors(item);
            return !entityErrors.Any();
        }

        /// <summary>
        /// <see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></typeparam>
        /// <param name="item"><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></param>
        /// <returns><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></returns>
        public IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return null;

            List<string> validationErrors = new List<string>();
            Dictionary<string, PropertyValidationResult> entityErrors = this.GetEntityErrors(item);

            if (entityErrors != null)
            {
                foreach (var errorMessage in entityErrors.Values)
                {
                    validationErrors.AddRange(errorMessage.ErrorMessages);
                }
            }
            return validationErrors;
        }

        /// <summary>
        /// Validate the Entity's Property
        /// </summary>
        /// <typeparam name="TEntity"><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></typeparam>
        /// <param name="item"><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></param>
        /// <param name="propertyName"><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></param>
        /// <returns><see cref=Infrastructure.CrossCutting.Validator.IEntityValidator"/></returns>
        public PropertyValidationResult ValidateProperty<TEntity>(TEntity item, string propertyName) where TEntity : class
        {
            Dictionary<string, PropertyValidationResult> entityErrors = this.GetEntityErrors(item);

            if (entityErrors.ContainsKey(propertyName))
            {
                return entityErrors[propertyName];
            }
            else
            {
                return new PropertyValidationResult() { PropertyName = propertyName };
            }
        }
        #endregion
    }
}
