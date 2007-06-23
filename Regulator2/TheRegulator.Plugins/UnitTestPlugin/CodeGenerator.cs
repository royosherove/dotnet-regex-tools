using System;
using System.CodeDom;
using System.Collections;
using System.Text.RegularExpressions;

namespace Royo.Plugins.CodeGeneratorPlugin
{

	/// <summary>
	///     A class that outputs Regular expression code
	///     in a specific language (C# or VB.NET).
	///     Courtesy of Omer Van Kloeten, CodeDOM master:
	///     http://weblogs.asp.net/OKloeten/
	/// </summary>
	public class CodeGenerator
	{
		/// <summary>
		/// The specific languages supported.
		/// </summary>
		public enum Language
		{
			CSharp,
			VisualBasic
		}

		/// <summary>
		/// Creates the compile unit containing the code.
		/// </summary>
		private static CodeCompileUnit Create(string expression, RegexOptions options)
		{
			CodeCompileUnit unit = new CodeCompileUnit();
			CodeNamespace nameSpace = new CodeNamespace("Regulator");
			CodeTypeDeclaration type = new CodeTypeDeclaration("RegularExpression");
			CodeMemberMethod method = new CodeMemberMethod();
			method.Name = "Test";

			method.Statements.Add(new CodeVariableDeclarationStatement(typeof(string), "regex", new CodePrimitiveExpression(expression)));
			method.Statements.Add(new CodeVariableDeclarationStatement(typeof(RegexOptions), "options", CreateEnum((int)(options), typeof(RegexOptions))));
			method.Statements.Add(new CodeVariableDeclarationStatement(typeof(Regex), "reg", new CodeObjectCreateExpression(typeof(Regex), new CodeVariableReferenceExpression("regex"), new CodeVariableReferenceExpression("options"))));
			
			type.Members.Add(method);
			nameSpace.Types.Add(type);
			unit.Namespaces.Add(nameSpace);

			return unit;
		}

		/// <summary>
		/// Writes the code to string in the specific language
		/// and returns the string
		/// </summary>
		public static string Create(Language language, string expression, RegexOptions options)
		{
			CodeCompileUnit unit = Create(expression, options);

			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			using (System.IO.StringWriter stringWriter = new System.IO.StringWriter(builder))
			{
				System.CodeDom.Compiler.ICodeGenerator generator;

				switch (language)
				{
					case Language.CSharp:
						System.CodeDom.Compiler.CodeGeneratorOptions genOptions = new System.CodeDom.Compiler.CodeGeneratorOptions();
						genOptions.BracingStyle = "C";
						generator = new Microsoft.CSharp.CSharpCodeProvider().CreateGenerator();
						generator.GenerateCodeFromCompileUnit(unit, stringWriter, genOptions);
						break;
					case Language.VisualBasic:
						generator = new Microsoft.VisualBasic.VBCodeProvider().CreateGenerator();
						generator.GenerateCodeFromCompileUnit(unit, stringWriter, null);
						break;
				}
	
				return builder.ToString();
			}
		}

		#region [ Enums, Courtesy of nGineer ]
		private static CodeExpression CreateEnum(int enumValue, Type enumType)
		{
			CodeExpression expr = null;

			SortedList orderedValues = new SortedList(new EnumValueComparer());

			foreach (int unsortedValue in Enum.GetValues(enumType))
			{
				if (unsortedValue != 0 && orderedValues[unsortedValue] == null)
				{
					orderedValues.Add(unsortedValue, Enum.GetName(enumType, unsortedValue));
				}
			}

			foreach (int sortedValue in orderedValues.Keys)
			{
				if ((enumValue & sortedValue) == sortedValue)
				{
					if (expr != null)
					{
						expr = new CodeBinaryOperatorExpression(expr, CodeBinaryOperatorType.BitwiseOr, new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(enumType), orderedValues[sortedValue].ToString()));
					}
					else
					{
						expr = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(enumType), orderedValues[sortedValue].ToString());
					}

					enumValue -= sortedValue;
				}
			}

			if (expr == null)
			{
				if (Enum.GetName(enumType, 0) == null)
				{
					throw new ArgumentException("enumValue");
				}
				else
				{
					return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(enumType), Enum.GetName(enumType, 0));
				}
			}
			else
			{
				return expr;
			}
		}

		internal class EnumValueComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				return (((int)(y)) - ((int)(x)));
			}
		}
		#endregion
	}
}
