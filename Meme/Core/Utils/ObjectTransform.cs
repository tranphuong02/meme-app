using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Core.Utils
{
    public static class ObjectTransform
    {
        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T">Type of source list object</typeparam>
        /// <typeparam name="TR">Type of destination list object will be converted to</typeparam>
        /// <param name="sourceObject">List of source object</param>
        /// <param name="propertyNamePrefix"></param>
        /// <param name="entity2Model"></param>
        /// <returns></returns>
        public static TR TransformObject<T, TR>(T sourceObject, string propertyNamePrefix = "", bool entity2Model = true)
        {
            Type destinationtype = typeof(TR);
            Type sourceType = typeof(T);
            object objectReturn = TransformObject(sourceType, destinationtype, sourceObject, null);
            return (TR)objectReturn;
        }

        /// <summary>
        /// Copy an list object to list destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T">Type of source list object</typeparam>
        /// <typeparam name="TR">Type of destination list object will be converted to</typeparam>
        /// <param name="sourceList">List of source object</param>
        /// <param name="propertyNamePrefix"></param>
        /// <param name="entity2Model"></param>
        /// <returns></returns>
        public static IList<TR> TransformListObject<T, TR>(IList<T> sourceList, string propertyNamePrefix = "", bool entity2Model = true)
        {
            var destinationList = new List<TR>();
            if (sourceList != null)
            {
                destinationList.AddRange(sourceList.Select(x => TransformObject<T, TR>(x, propertyNamePrefix, entity2Model)));
            }
            return destinationList;
        }

        private static object TransformObject(Type sourceType, Type destinationType, object sourceObject, Type parentType, string propertyNamePrefix = "", bool entity2Model = true)
        {
            object objectReturn = Activator.CreateInstance(destinationType);
            PropertyInfo[] propertyInfos = destinationType.GetProperties();
            PropertyInfo[] sourcePropertyInfos = sourceType.GetProperties();
            if (!propertyInfos.Any())
            {
                return sourceObject;
            }
            foreach (PropertyInfo property in propertyInfos)
            {
                var propertyName = property.Name;

                if (!string.IsNullOrEmpty(propertyNamePrefix))
                {
                    propertyName = entity2Model
                        ? propertyName.Substring(propertyNamePrefix.Length)
                        : string.Format("{0}{1}", propertyNamePrefix, propertyName);
                }

                PropertyInfo sourceProp = sourcePropertyInfos.FirstOrDefault(x => x.Name.Equals(propertyName));

                if (sourceProp != null)
                {
                    object sourcePropertyValue;

                    try
                    {
                        sourcePropertyValue = sourceProp.GetValue(sourceObject, null);
                    }
                    catch (TargetInvocationException)    // System.Reflection.TargetInvocationException
                    {
                        continue;
                    }

                    if (sourcePropertyValue == null) continue;

                    // if property is a List of object. check If it implements IList<> ...
                    if (sourceProp.PropertyType.IsGenericType && (sourcePropertyValue.GetType().GetInterface("IList`1") != null || (sourcePropertyValue.GetType().GetInterface("ICollection`1") != null)))
                    {
                        //continue;
                        Type sourcePropertyType = sourceProp.PropertyType.GetGenericArguments()[0];

                        var destPropertyType = property.PropertyType.GetGenericArguments()[0];

                        //Get List data source object's property
                        //IList o = (IList)sourceProp.GetValue(sourceObject, null);
                        var sourceCollection = sourceProp.GetValue(sourceObject, null);

                        // Init new List of generic type in destination object's property
                        var typeArgs = property.PropertyType.GetGenericArguments();
                        Type d1 = typeof(List<>);
                        Type makeme = d1.MakeGenericType(typeArgs);
                        var destList = (IList)Activator.CreateInstance(makeme);

                        //Loop to transform all item in source list to destination list
                        foreach (object obj in ((IEnumerable)sourceCollection))
                        {
                            if (destPropertyType.Namespace != null && destPropertyType.Namespace.Contains("System"))
                            {
                                destList.Add(obj);
                            }
                            else
                            {
                                //prevent infinitive loop in relationship properties
                                if (sourcePropertyType != parentType)
                                {
                                    destList.Add(TransformObject(sourcePropertyType, destPropertyType, obj, sourceType, propertyNamePrefix, entity2Model));
                                }
                            }
                        }
                        //set list to destination property
                        property.SetValue(objectReturn, destList, null);
                    }
                    else if (sourceProp.PropertyType.IsArray)
                    {
                        //Get List data source object's property
                        //IList o = (IList)sourceProp.GetValue(sourceObject, null);
                        var sourceArray = (object[])sourceProp.GetValue(sourceObject, null);

                        //continue;
                        if (sourceArray.Length > 0)
                        {
                            //Type sourcePropertyType = sourceProp.PropertyType.GetGenericArguments()[0];
                            if (!property.PropertyType.IsGenericType)
                            {
                                if (property.PropertyType.IsArray)
                                {
                                    var ns = sourceArray[0].GetType().Namespace;
                                    if (ns != null && ns.Contains("System"))
                                    {
                                        property.SetValue(objectReturn, sourceArray, null);
                                    }
                                }
                            }
                            else
                            {
                                var destPropertyType = property.PropertyType.GetGenericArguments()[0];
                                // Init new List of generic type in destination object's property
                                var typeArgs = property.PropertyType.GetGenericArguments();
                                Type d1 = typeof(List<>);
                                Type makeme = d1.MakeGenericType(typeArgs);
                                var destList = (IList)Activator.CreateInstance(makeme);

                                //Loop to transform all item in source list to destination list
                                foreach (object obj in ((IEnumerable)sourceArray))
                                {
                                    if (destPropertyType.Namespace != null && destPropertyType.Namespace.Contains("System"))
                                    {
                                        destList.Add(obj);
                                    }
                                    else
                                    {
                                        var sourcePropertyType = obj.GetType();
                                        //prevent infinitive loop in relationship properties
                                        if (sourcePropertyType != parentType)
                                        {
                                            destList.Add(TransformObject(sourcePropertyType, destPropertyType, obj, sourceType, propertyNamePrefix, entity2Model));
                                        }
                                    }
                                }
                                //set list to destination property
                                property.SetValue(objectReturn, destList, null);
                            }
                        }

                    }

                    // check if it is a primitive type
                    else if (sourceProp.PropertyType.Namespace != null && sourceProp.PropertyType.Namespace.Contains("System"))
                    {
                        property.SetValue(objectReturn, sourcePropertyValue, null);
                    }
                    // for reference type
                    else
                    {
                        //prevent infinitive loop in relationship properties
                        if (sourceProp.PropertyType != parentType)
                        {
                            Type st = sourcePropertyValue.GetType();
                            Type dt = property.PropertyType;
                            object destinationPropertyValue = TransformObject(st, dt, sourcePropertyValue, sourceType, propertyNamePrefix, entity2Model);
                            property.SetValue(objectReturn, destinationPropertyValue, null);
                        }
                    }
                }
            }
            return objectReturn;
        }

        #region Model - Entity

        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="sourceObject">An object with matching fields of the destination object</param>
        /// <param name="decode"></param>
        public static TR EntityToModel<T, TR>(T sourceObject, bool decode = true) where TR : new()
        {
            TR destObject = new TR();
            //  If either the source return
            if (sourceObject == null)
                return destObject;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = typeof(TR);

            //  Loop through the source properties
            foreach (PropertyInfo sourceProp in sourceType.GetProperties())
            {
                var sourcePropertyValue = sourceProp.GetValue(sourceObject, null);
                if (sourcePropertyValue == null)
                    continue;
                //  if property is a List of object. check If it implements IList<> ...
                if (sourceProp.PropertyType.IsGenericType && (sourcePropertyValue.GetType().GetInterface("IList`1") != null || (sourcePropertyValue.GetType().GetInterface("ICollection`1") != null)))
                    continue;
                if (sourceProp.PropertyType.IsArray)
                    continue;
                //  for reference type => continue
                if (sourceProp.PropertyType.Namespace == null || !sourceProp.PropertyType.Namespace.Contains("System"))
                    continue;
                //  only set value if it is primitive type

                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(sourceProp.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;
                //  Set the value in the destination
                targetObj.SetValue(destObject, sourcePropertyValue, null);
                
                targetObj.SetValue(destObject,
                    decode && sourcePropertyValue is string
                        ? HttpUtility.HtmlDecode(sourcePropertyValue + "")
                        : sourcePropertyValue, null);
            }
            return destObject;
        }

        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="sourceObject">An object with matching fields of the destination object</param>
        /// <param name="encode"></param>
        public static TR ModelToEntity<T, TR>(T sourceObject, bool encode = true) where TR : new()
        {
            TR destObject = new TR();
            //  If either the source return
            if (sourceObject == null)
                return destObject;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = typeof(TR);

            //  Loop through the source properties
            foreach (PropertyInfo sourceProp in sourceType.GetProperties())
            {
                var sourcePropertyValue = sourceProp.GetValue(sourceObject, null);
                if (sourcePropertyValue == null)
                    continue;
                //  if property is a List of object. check If it implements IList<> ...
                if (sourceProp.PropertyType.IsGenericType && (sourcePropertyValue.GetType().GetInterface("IList`1") != null || (sourcePropertyValue.GetType().GetInterface("ICollection`1") != null)))
                    continue;
                if (sourceProp.PropertyType.IsArray)
                    continue;
                //  for reference type => continue
                if (sourceProp.PropertyType.Namespace == null || !sourceProp.PropertyType.Namespace.Contains("System"))
                    continue;
                //  only set value if it is primitive type

                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(sourceProp.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;
                //  Set the value in the destination
                targetObj.SetValue(destObject,
                    encode && sourcePropertyValue is string
                        ? HttpUtility.HtmlEncode(sourcePropertyValue)
                        : sourcePropertyValue, null);
            }
            return destObject;
        }

        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="sourceObject">An object with matching fields of the destination object</param>
        /// <param name="destObject">An destination object from db</param>
        /// <param name="encode"></param>
        public static TR ModelToEntity<T, TR>(T sourceObject, TR destObject, bool encode = true) where TR : new()
        {
            //  If either the source return
            if (sourceObject == null || destObject == null)
                return destObject;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = typeof(TR);

            //  Loop through the source properties
            foreach (PropertyInfo sourceProp in sourceType.GetProperties())
            {
                var sourcePropertyValue = sourceProp.GetValue(sourceObject, null);
                if (sourcePropertyValue == null)
                    continue;
                //  if property is a List of object. check If it implements IList<> ...
                if (sourceProp.PropertyType.IsGenericType && (sourcePropertyValue.GetType().GetInterface("IList`1") != null || (sourcePropertyValue.GetType().GetInterface("ICollection`1") != null)))
                    continue;
                if (sourceProp.PropertyType.IsArray)
                    continue;
                //  for reference type => continue
                if (sourceProp.PropertyType.Namespace == null || !sourceProp.PropertyType.Namespace.Contains("System"))
                    continue;
                //  only set value if it is primitive type

                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(sourceProp.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;
                //  Set the value in the destination
                targetObj.SetValue(destObject,
                    encode && sourcePropertyValue is string
                        ? HttpUtility.HtmlEncode(sourcePropertyValue)
                        : sourcePropertyValue, null);
            }
            return destObject;
        }

        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="listSourceObject">List object with matching fields of the destination object</param>
        /// <param name="decode"></param>
        public static IEnumerable<TR> EntitiesToModels<T, TR>(List<T> listSourceObject, bool decode = true) where TR : new()
        {
            var listDestObject = new List<TR>();
            //  If either the source return
            if (listSourceObject == null || !listSourceObject.Any())
                return listDestObject;

            //  Get the type of each object
            var firstOrDefault = listSourceObject.FirstOrDefault();
            if (firstOrDefault != null)
            {
                Type sourceType = firstOrDefault.GetType();
                Type targetType = typeof(TR);

                foreach (var obj in listSourceObject)
                {
                    var destObject = new TR();
                    //  Loop through the source properties
                    foreach (PropertyInfo sourceProp in sourceType.GetProperties())
                    {
                        var sourcePropertyValue = sourceProp.GetValue(obj, null);
                        if (sourcePropertyValue == null)
                            continue;
                        //  if property is a List of object. check If it implements IList<> ...
                        if (sourceProp.PropertyType.IsGenericType && (sourcePropertyValue.GetType().GetInterface("IList`1") != null || (sourcePropertyValue.GetType().GetInterface("ICollection`1") != null)))
                            continue;
                        if (sourceProp.PropertyType.IsArray)
                            continue;
                        //  for reference type => continue
                        if (sourceProp.PropertyType.Namespace == null || !sourceProp.PropertyType.Namespace.Contains("System"))
                            continue;
                        //  only set value if it is primitive type

                        //  Get the matching property in the destination object
                        PropertyInfo targetObj = targetType.GetProperty(sourceProp.Name);
                        //  If there is none, skip
                        if (targetObj == null)
                            continue;

                        //  Set the value in the destination
                        targetObj.SetValue(destObject, sourcePropertyValue, null);

                        targetObj.SetValue(destObject,
                            decode && sourcePropertyValue is string
                                ? HttpUtility.HtmlDecode(sourcePropertyValue + "")
                                : sourcePropertyValue, null);
                    }
                    listDestObject.Add(destObject);
                }
            }
            return listDestObject;
        }

        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listSourceObject">List object with matching fields of the destination object</param>
        /// <param name="encode"></param>
        public static IEnumerable<TR> ModelsToEntities<T, TR>(List<T> listSourceObject, bool encode = true) where TR : new()
        {
            var listDestObject = new List<TR>();
            //  If either the source return
            if (listSourceObject == null || !listSourceObject.Any())
                return listDestObject;

            //  Get the type of each object
            var firstOrDefault = listSourceObject.FirstOrDefault();
            if (firstOrDefault != null)
            {
                Type sourceType = firstOrDefault.GetType();
                Type targetType = typeof(TR);

                foreach (var obj in listSourceObject)
                {
                    var destObject = new TR();
                    //  Loop through the source properties
                    foreach (PropertyInfo sourceProp in sourceType.GetProperties())
                    {
                        var sourcePropertyValue = sourceProp.GetValue(obj, null);
                        if (sourcePropertyValue == null)
                            continue;
                        //  if property is a List of object. check If it implements IList<> ...
                        if (sourceProp.PropertyType.IsGenericType && (sourcePropertyValue.GetType().GetInterface("IList`1") != null || (sourcePropertyValue.GetType().GetInterface("ICollection`1") != null)))
                            continue;
                        if (sourceProp.PropertyType.IsArray)
                            continue;
                        //  for reference type => continue
                        if (sourceProp.PropertyType.Namespace == null || !sourceProp.PropertyType.Namespace.Contains("System"))
                            continue;
                        //  only set value if it is primitive type

                        //  Get the matching property in the destination object
                        PropertyInfo targetObj = targetType.GetProperty(sourceProp.Name);
                        //  If there is none, skip
                        if (targetObj == null)
                            continue;
                        //  Set the value in the destination
                        targetObj.SetValue(destObject, sourcePropertyValue, null);

                        targetObj.SetValue(destObject,
                            encode && sourcePropertyValue is string
                                ? HttpUtility.HtmlEncode(sourcePropertyValue)
                                : sourcePropertyValue, null);
                    }
                    listDestObject.Add(destObject);
                }
            }
            return listDestObject;
        }

        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listSourceObject">List object with matching fields of the destination object</param>
        /// <param name="listDestObject">List destination object from db</param>
        /// <param name="encode"></param>
        public static IEnumerable<TR> ModelsToEntities<T, TR>(List<T> listSourceObject, List<TR> listDestObject, bool encode = true) where TR : new()
        {
            //  If either the source return
            if (listSourceObject == null || !listSourceObject.Any() || listDestObject == null || !listDestObject.Any())
                return listDestObject;

            //  Get the type of each object
            var firstOrDefault = listSourceObject.FirstOrDefault();
            if (firstOrDefault != null)
            {
                Type sourceType = firstOrDefault.GetType();
                Type targetType = typeof(TR);

                foreach (var obj in listSourceObject)
                {
                    var destObject = new TR();
                    //  Loop through the source properties
                    foreach (PropertyInfo sourceProp in sourceType.GetProperties())
                    {
                        var sourcePropertyValue = sourceProp.GetValue(obj, null);
                        if (sourcePropertyValue == null)
                            continue;
                        //  if property is a List of object. check If it implements IList<> ...
                        if (sourceProp.PropertyType.IsGenericType && (sourcePropertyValue.GetType().GetInterface("IList`1") != null || (sourcePropertyValue.GetType().GetInterface("ICollection`1") != null)))
                            continue;
                        if (sourceProp.PropertyType.IsArray)
                            continue;
                        //  for reference type => continue
                        if (sourceProp.PropertyType.Namespace == null || !sourceProp.PropertyType.Namespace.Contains("System"))
                            continue;
                        //  only set value if it is primitive type

                        //  Get the matching property in the destination object
                        PropertyInfo targetObj = targetType.GetProperty(sourceProp.Name);
                        //  If there is none, skip
                        if (targetObj == null)
                            continue;
                        //  Set the value in the destination
                        targetObj.SetValue(destObject, sourcePropertyValue, null);

                        targetObj.SetValue(destObject,
                            encode && sourcePropertyValue is string
                                ? HttpUtility.HtmlEncode(sourcePropertyValue)
                                : sourcePropertyValue, null);
                    }
                    listDestObject.Add(destObject);
                }
            }
            return listDestObject;
        }

        #endregion
    }
}
