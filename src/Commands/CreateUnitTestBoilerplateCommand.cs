﻿//------------------------------------------------------------------------------
// <copyright file="CreateUnitTestBoilerplateCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using UnitTestBoilerplate.Utilities;
using UnitTestBoilerplate.View;
using IAsyncServiceProvider = Microsoft.VisualStudio.Shell.IAsyncServiceProvider;
using Task = System.Threading.Tasks.Task;

namespace UnitTestBoilerplate.Commands
{
	/// <summary>
	/// Command handler
	/// </summary>
	internal sealed class CreateUnitTestBoilerplateCommand
	{
		/// <summary>
		/// Command ID.
		/// </summary>
		public const int CommandId = 0x0100;

		/// <summary>
		/// Command menu group (command set GUID).
		/// </summary>
		public static readonly Guid CommandSet = new Guid("542fa460-e966-445e-b2e2-3b82e1a75ca4");

		/// <summary>
		/// VS Package that provides this command, not null.
		/// </summary>
		private readonly AsyncPackage package;

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateUnitTestBoilerplateCommand"/> class.
		/// Adds our command handlers for menu (commands must exist in the command table file)
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		private CreateUnitTestBoilerplateCommand(AsyncPackage package)
		{
			if (package == null)
			{
				throw new ArgumentNullException(nameof(package));
			}

			this.package = package;
		}

		/// <summary>
		/// Gets the instance of the command.
		/// </summary>
		public static CreateUnitTestBoilerplateCommand Instance
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes the singleton instance of the command.
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		public static async Task InitializeAsync(AsyncPackage package)
		{
			Instance = new CreateUnitTestBoilerplateCommand(package);
			await Instance.InitializeAsync();
		}

		private async Task InitializeAsync()
		{
			DTE2 dte = (DTE2)await this.package.GetServiceAsync(typeof(DTE));
			OleMenuCommandService commandService = await this.package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			if (commandService != null)
			{
				var menuCommandId = new CommandID(CommandSet, CommandId);
				var menuItem = new OleMenuCommand(this.MenuItemCallback, menuCommandId);
				menuItem.BeforeQueryStatus += (sender, args) =>
				{
					menuItem.Visible = SolutionUtilities.GetSelectedFiles(dte).Any(file => file != null && file.FilePath != null && file.FilePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
						|| SolutionUtilities.GetSelectedFunction(dte) != null;
				};

				commandService.AddCommand(menuItem);
			}
		}

		/// <summary>
		/// This function is the callback used to execute the command when the menu item is clicked.
		/// See the constructor to see how the menu item is associated with this function using
		/// OleMenuCommandService service and MenuCommand class.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		private void MenuItemCallback(object sender, EventArgs e)
		{
			//var componentModel = (IComponentModel)this.ServiceProvider.GetService(typeof(SComponentModel));
			//var settings = componentModel.DefaultExportProvider.GetExportedValue<IBoilerplateSettings>();

			var dialog = new CreateUnitTestBoilerplateDialog();
			dialog.ShowModal();
		}
	}
}
