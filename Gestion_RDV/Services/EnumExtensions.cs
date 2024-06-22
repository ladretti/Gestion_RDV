using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Services
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var memberInfo = enumType.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                                   .OfType<DisplayAttribute>()
                                                   .FirstOrDefault();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }
            return enumValue.ToString(); 
        }
    }

}
