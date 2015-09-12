using System;

namespace Core.ModelBinder
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModelBinderTargetAttribute : Attribute
    {
        public Type Target { get; set; }

        public ModelBinderTargetAttribute(Type target)
        {
            Target = target;
        }
    }
}
