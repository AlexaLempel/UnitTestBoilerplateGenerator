using System.Threading.Tasks;
using UnitTestBoilerplate.Model;

namespace UnitTestBoilerplate.Services
{
	public interface ITestGenerationService
	{
		Task<string> GenerateUnitTestFileAsync(
			ProjectItemSummary selectedFile,
			EnvDTE.Project targetProject, 
			TestFramework testFramework,
			MockFramework mockFramework,
			EnvDTE80.CodeFunction2 selectedFunction = null);

		Task GenerateUnitTestFileAsync(
			ProjectItemSummary selectedFile,
			string targetFilePath,
			string targetProjectNamespace,
			TestFramework testFramework,
			MockFramework mockFramework,
			EnvDTE80.CodeFunction2 selectedFunction = null);

		string GetRelativePath(ProjectItemSummary selectedFile);
	}
}