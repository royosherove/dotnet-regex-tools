namespace TeamAgile.RegexKit.Common
{
    using System;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class Reflect
    {
        public static string GetInputFromMatch(Match match)
        {
            return GetPrivateFieldValue<string, Match>("_text", match);
        }

        public static string GetInputFromMatches(MatchCollection matches)
        {
            return GetPrivateFieldValue<string, MatchCollection>("_input", matches);
        }

        public static string GetPatternFromMatches(MatchCollection matches)
        {
            Regex reflectedObject = GetRegexObjectFromMatches(matches);
            return GetPrivateFieldValue<string, Regex>("pattern", reflectedObject);
        }

        public static string GetPatternFromRegex(Regex instance)
        {
            return GetPrivateFieldValue<string, Regex>("pattern", instance);
        }

        public static FieldType GetPrivateFieldValue<FieldType, ReflectedType>(string fieldName, ReflectedType reflectedObject)
        {
            Type type = typeof(ReflectedType);
            return safeCastOrDefaultValue<FieldType>(type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(reflectedObject));
        }

        public static Regex GetRegexObjectFromMatch(Match match)
        {
            return GetPrivateFieldValue<Regex, Match>("_regex", match);
        }

        public static Regex GetRegexObjectFromMatches(MatchCollection matches)
        {
            return GetPrivateFieldValue<Regex, MatchCollection>("_regex", matches);
        }

        public static T safeCastOrDefaultValue<T>(object obj)
        {
            if ((obj != null) && (obj is T))
            {
                return (T) obj;
            }
            return default(T);
        }
    }
}

