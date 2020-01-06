using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace UnitTestBoilerplate.Model
{
	/// <summary>
	/// Holds all the data required to generate the test file. Includes information about the class to generate tests for, and the unit test project the test will be added to.
	/// </summary>
	public class TestFileContext
	{
		public TestFileContext(
			Document document,
			string className,
			string classNamespace,
			IList<MethodDeclarationSyntax> methodDeclarations,
			IList<string> usingNamespaces)
		{
			this.Document = document;
			this.ClassName = className;
			this.ClassNamespace = classNamespace;
			this.MethodDeclarations = methodDeclarations;
			this.UsingNamespaces = usingNamespaces;
		}

		public Document Document { get; }


		public string ClassName { get; }

		public string ClassNamespace { get; }

		public IList<MethodDeclarationSyntax> MethodDeclarations { get; }

		public IList<string> UsedTestMethodNames { get; } = new List<string>();

		public IList<string> UsingNamespaces { get; } = new List<string>();
	}
}
