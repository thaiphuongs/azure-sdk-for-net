﻿// -----------------------------------------------------------------------------------------
// <copyright file="EntityProperty.cs" company="Microsoft">
//    Copyright 2012 Microsoft Corporation
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// -----------------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Storage.Table
{
    using System;
    using System.Linq;

#if RT
    using System.Runtime.InteropServices.WindowsRuntime;
#endif

    /// <summary>
    /// Class for storing information about a single property in an Azure
    /// Table entity.
    /// </summary>
    public sealed class EntityProperty
    {
        #region Value Storage

        private object propertyAsObject;

        internal object PropertyAsObject
        {
            get
            {
                return this.propertyAsObject;
            }

            set
            {
                this.IsNull = value == null;
                this.propertyAsObject = value;
            }
        }

        #endregion

        #region RT FactoryMethods

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified <see cref="DateTime"/> offset value.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the <see cref="DateTime"/> offset type.</returns>
        public static EntityProperty GeneratePropertyForDateTimeOffset(DateTimeOffset? input)
        {
            return new EntityProperty(input);
        }

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified byte array.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the byte array.</returns>
        public static EntityProperty GeneratePropertyForByteArray(
#if RT
            [ReadOnlyArray]
#endif
byte[] input)
        {
            return new EntityProperty(input);
        }

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the <see cref="Boolean"/> type.</returns>
        public static EntityProperty GeneratePropertyForBool(bool input)
        {
            return new EntityProperty(input);
        }

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified <see cref="Double"/> value.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the <see cref="Double"/> type.</returns>
        public static EntityProperty GeneratePropertyForDouble(double input)
        {
            return new EntityProperty(input);
        }

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified <see cref="Guid"/> value.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the <see cref="Guid"/> type.</returns>
        public static EntityProperty GeneratePropertyForGuid(Guid input)
        {
            return new EntityProperty(input);
        }

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified <see cref="Int32"/> value.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the <see cref="Int32"/> type.</returns>
        public static EntityProperty GeneratePropertyForInt(int input)
        {
            return new EntityProperty(input);
        }

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified <see cref="Int64"/> value.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the <see cref="Int64"/> type.</returns>
        public static EntityProperty GeneratePropertyForLong(long input)
        {
            return new EntityProperty(input);
        }

        /// <summary>
        /// Creates a new <see cref="EntityProperty"/> object that represents the specified <see cref="String"/> value.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        /// <returns>A new <see cref="EntityProperty"/> of the <see cref="String"/> type.</returns>
        public static EntityProperty GeneratePropertyForString(string input)
        {
            return new EntityProperty(input);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// byte array value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(byte[] input)
            : this(EdmType.Binary)
        {
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="Boolean"/> value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(bool input)
            : this(EdmType.Boolean)
        {
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="DateTime"/> offset value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(DateTimeOffset? input)
            : this(EdmType.DateTime)
        {
            if (input.HasValue)
            {
                // Convert to datetime
                this.PropertyAsObject = input.Value.UtcDateTime;
            }
            else
            {
                this.IsNull = true;
                this.PropertyAsObject = new DateTime?();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="DateTime"/> value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(DateTime? input)
            : this(EdmType.DateTime)
        {
            this.IsNull = !input.HasValue;
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="Double"/> value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(double input)
            : this(EdmType.Double)
        {
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="Guid"/> value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(Guid input)
            : this(EdmType.Guid)
        {
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="Int32"/> value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(int input)
            : this(EdmType.Int32)
        {
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="Int64"/> value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
#if RT
        internal
#else
        public
#endif
 EntityProperty(long input)
            : this(EdmType.Int64)
        {
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityProperty"/> class by using the
        /// <see cref="String"/> value of the property.
        /// </summary>
        /// <param name="input">The value for the new <see cref="EntityProperty"/>.</param>
        public EntityProperty(string input)
            : this(EdmType.String)
        {
            this.PropertyAsObject = input;
        }

        /// <summary>
        /// Initializes a new instance of the EntityProperty class given the
        /// EdmType of the property (the value must be set by a public
        /// constructor).
        /// </summary>
        private EntityProperty(EdmType propertyType)
        {
            this.PropertyType = propertyType;
        }

        #endregion

        /// <summary>
        /// Gets the <see cref="EdmType"/> of this <see cref="EntityProperty"/> object.
        /// </summary>
        /// <value>The <see cref="EdmType"/> of this <see cref="EntityProperty"/> object.</value>
        public EdmType PropertyType { get; private set; }

        #region Properties

        /// <summary>
        /// Gets or sets the byte array value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than an byte array.
        /// </summary>
        /// <value>The byte array value of this <see cref="EntityProperty"/> object.</value>
        public byte[] BinaryValue
        {
            get
            {
                this.EnforceType(EdmType.Binary);
                return (byte[])this.PropertyAsObject;
            }

            set
            {
                this.EnforceType(EdmType.Binary);
                this.PropertyAsObject = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Boolean"/> value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than an <see cref="Boolean"/> Object.
        /// </summary>
        /// <value>The <see cref="Boolean"/> value of this <see cref="EntityProperty"/> object.</value>
        public bool BooleanValue
        {
            get
            {
                this.EnforceType(EdmType.Boolean);
                return (bool)this.PropertyAsObject;
            }

            set
            {
                this.EnforceType(EdmType.Boolean);
                this.PropertyAsObject = value;
            }
        }

        internal DateTime? DateTime
        {
            get
            {
                return (DateTime?)this.PropertyAsObject;
            }

            set
            {
                this.PropertyAsObject = value;
                this.IsNull = value.HasValue;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTimeOffset"/> offset value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than a <see cref="DateTimeOffset"/> object.
        /// </summary>
        /// <value>The <see cref="DateTimeOffset"/> offset value of this <see cref="EntityProperty"/> object.</value>
        public DateTimeOffset? DateTimeOffsetValue
        {
            get
            {
                this.EnforceType(EdmType.DateTime);

                return this.PropertyAsObject != null ? new DateTimeOffset((DateTime)this.PropertyAsObject) : (DateTimeOffset?)null;
            }

            set
            {
                this.EnforceType(EdmType.DateTime);

                if (value.HasValue)
                {
                    // Convert to datetime
                    this.PropertyAsObject = value.Value.UtcDateTime;
                }
                else
                {
                    this.IsNull = true;
                    this.PropertyAsObject = (DateTime?)null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Double"/> value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than a <see cref="Double"/> object.
        /// </summary>
        /// <value>The <see cref="Double"/> value of this <see cref="EntityProperty"/> object.</value>
        public double DoubleValue
        {
            get
            {
                this.EnforceType(EdmType.Double);
                return (double)this.PropertyAsObject;
            }

            set
            {
                this.EnforceType(EdmType.Double);
                this.PropertyAsObject = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/> value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than a <see cref="Guid"/> object.
        /// </summary>
        /// <value>The <see cref="Guid"/> value of this <see cref="EntityProperty"/> object.</value>
        public Guid GuidValue
        {
            get
            {
                this.EnforceType(EdmType.Guid);
                return (Guid)this.PropertyAsObject;
            }

            set
            {
                this.EnforceType(EdmType.Guid);
                this.PropertyAsObject = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Int32"/> value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than an <see cref="Int32"/> Object.
        /// </summary>
        /// <value>The <see cref="Int32"/> value of this <see cref="EntityProperty"/> object.</value>
        public int Int32Value
        {
            get
            {
                this.EnforceType(EdmType.Int32);
                return (int)this.PropertyAsObject;
            }

            set
            {
                this.EnforceType(EdmType.Int32);
                this.PropertyAsObject = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Int64"/> value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than an <see cref="Int64"/> Object.
        /// </summary>
        /// <value>The <see cref="Int64"/> value of this <see cref="EntityProperty"/> object.</value>
        public long Int64Value
        {
            get
            {
                this.EnforceType(EdmType.Int64);
                return (long)this.PropertyAsObject;
            }

            set
            {
                this.EnforceType(EdmType.Int64);
                this.PropertyAsObject = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="String"/> value of this <see cref="EntityProperty"/> object.
        /// An exception will be thrown if you attempt to set this property to anything other than a <see cref="String"/> object.
        /// </summary>
        /// <value>The <see cref="String"/> value of this <see cref="EntityProperty"/> object.</value>
        public string StringValue
        {
            get
            {
                this.EnforceType(EdmType.String);
                return (string)this.PropertyAsObject;
            }

            set
            {
                this.EnforceType(EdmType.String);
                this.PropertyAsObject = value;
            }
        }

        #endregion

        /// <summary>
        /// Compares the given object (which is probably an <see cref="EntityProperty"/>)
        /// for equality with this object.
        /// </summary>
        /// <param name="obj">The other object.</param>
        /// <returns><c>true</c> if the objects are equivalent; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            EntityProperty other = obj as EntityProperty;
            if (other == null)
            {
                return false;
            }

            switch (this.PropertyType)
            {
                case EdmType.Binary:
                    return this.BinaryValue.Length == other.BinaryValue.Length
                        && this.BinaryValue.SequenceEqual(other.BinaryValue);
                case EdmType.Boolean:
                    return this.BooleanValue == other.BooleanValue;
                case EdmType.DateTime:
                    return this.DateTime == other.DateTime;
                case EdmType.Double:
                    return this.DoubleValue == other.DoubleValue;
                case EdmType.Guid:
                    return this.GuidValue == other.GuidValue;
                case EdmType.Int32:
                    return this.Int32Value == other.Int32Value;
                case EdmType.Int64:
                    return this.Int64Value == other.Int64Value;
                case EdmType.String:
                    return string.Equals(this.StringValue, other.StringValue);
                default:
                    return this.PropertyAsObject == other.PropertyAsObject;
            }
        }

        /// <summary>
        /// Gets the hash code for this entity property.
        /// </summary>
        /// <returns>The hash code for the entity property.</returns>
        public override int GetHashCode()
        {
            return this.PropertyAsObject.GetHashCode();
        }

        internal bool IsNull { get; set; }

        internal static EntityProperty CreateEntityPropertyFromObject(object value)
        {
            return CreateEntityPropertyFromObject(value, true);
        }

        internal static EntityProperty CreateEntityPropertyFromObject(object value, bool allowUnknownTypes)
        {
            if (value is string)
            {
                return new EntityProperty((string)value);
            }
            else if (value is byte[])
            {
                return new EntityProperty((byte[])value);
            }
            else if (value is bool)
            {
                return new EntityProperty((bool)value);
            }
            else if (value is DateTime)
            {
                return new EntityProperty((DateTime)value);
            }
            else if (value is DateTime?)
            {
                return new EntityProperty((DateTime?)value);
            }
            else if (value is DateTimeOffset)
            {
                return new EntityProperty((DateTimeOffset)value);
            }
            else if (value is DateTimeOffset?)
            {
                return new EntityProperty((DateTimeOffset?)value);
            }
            else if (value is double)
            {
                return new EntityProperty((double)value);
            }
            else if (value is Guid)
            {
                return new EntityProperty((Guid)value);
            }
            else if (value is int)
            {
                return new EntityProperty((int)value);
            }
            else if (value is long)
            {
                return new EntityProperty((long)value);
            }
            else if (value == null)
            {
                return new EntityProperty((string)null);
            }
            else if (allowUnknownTypes)
            {
                return new EntityProperty(value.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ensures that the given type matches the type of this entity
        /// property; throws an exception if the types do not match.
        /// </summary>
        private void EnforceType(EdmType requestedType)
        {
            if (this.PropertyType != requestedType)
            {
                throw new InvalidOperationException(string.Format(
                    "Cannot return {0} type for a {1} typed property.",
                    requestedType,
                    this.PropertyType));
            }
        }
    }
}